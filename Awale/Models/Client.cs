using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Awale.Models
{
    public class Client
    {

        // ManualResetEvent instances signal completion.  
        private static ManualResetEvent connectDone;
        private static ManualResetEvent sendDone;
        private static ManualResetEvent receiveDone;

        // The response from the remote device.  
        private static String response = String.Empty;
        private static Socket client;

        public static Task<string> StartClient(string ip, string port, string nameplayer)
        {
            return Task.Run(() =>
            {
                connectDone =
            new ManualResetEvent(false);
                sendDone =
            new ManualResetEvent(false);
                receiveDone =
            new ManualResetEvent(false);
                // Connect to a remote device.  
                try
                {
                    // Establish the remote endpoint for the socket.  
                    // The name of the   
                    // remote device is "host.contoso.com".  
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(ip);
                    IPAddress ipAddress = ipHostInfo.AddressList[0];
                    foreach (IPAddress ipa in ipHostInfo.AddressList)
                    {
                        AddressFamily af = ipa.AddressFamily;
                        if (af == AddressFamily.InterNetwork)
                        {
                            ipAddress = ipa;
                            break;
                        }
                    }
                    IPEndPoint remoteEP = new IPEndPoint(ipAddress, int.Parse(port));

                    // Create a TCP/IP socket.  
                    client = new Socket(ipAddress.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);

                    // Connect to the remote endpoint.  
                    client.BeginConnect(remoteEP,
                        new AsyncCallback(ConnectCallback), client);
                    connectDone.WaitOne();

                    // Send test data to the remote device.  
                    Send("client:test/player:" + nameplayer + "<EOF>");
                    sendDone.WaitOne();

                    // Receive the response from the remote device.  
                    Receive();
                    receiveDone.WaitOne();

                    // Write the response to the console.  
                    Console.WriteLine("Response received : {0}", response);

                    // Release the socket.  
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();
                    return response;

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                    return "";
                }
            });
        }

        public static void Stop()
        {
            if(connectDone != null)
            {
                connectDone.Close();
            }
            if (sendDone != null)
            {
                sendDone.Close();
            }
            if (receiveDone != null)
            {
                receiveDone.Close();
            }
            if (client != null && client.Connected)
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
            }
        }

        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                client = (Socket)ar.AsyncState;

                // Complete the connection.  
                client.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());

                // Signal that the connection has been made.  
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive()
        {
            try
            {
                // Create the state object.  
                StateObject state = new StateObject();
                state.workSocket = client;

                // Begin receiving the data from the remote device.  
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the state object and the client socket   
                // from the asynchronous state object.  
                StateObject state = (StateObject)ar.AsyncState;
                client = state.workSocket;

                // Read data from the remote device.  
                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    // There might be more data, so store the data received so far.  
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    // Get the rest of the data.  
                    client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    // All the data has arrived; put it in response.  
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    // Signal that all bytes have been received.  
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public static void Send(String data)
        {
            // Convert the string data to byte data using ASCII encoding.  
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            // Begin sending the data to the remote device.  
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                // Retrieve the socket from the state object.  
                client = (Socket)ar.AsyncState;

                // Complete sending the data to the remote device.  
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                // Signal that all bytes have been sent.  
                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
