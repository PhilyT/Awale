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
    public class Serveur
    {
        private event EventHandler reception;
        private int port;
        private TcpListener server;
        private TcpClient client;
        private NetworkStream stream;
        private bool running;
        private string nomPlayer;

        public EventHandler Reception
        {
            get
            {
                return reception;
            }
            set
            {
                reception = value;
            }
        }
        
        public string NomPlayer { get => nomPlayer; set => nomPlayer = value; }

        public bool Start(string port, string nomPlayer)
        {
            this.nomPlayer = nomPlayer;
            if(int.TryParse(port, out this.port))
            {
                IPAddress localadresse = IPAddress.Parse("127.0.0.14");
                server = new TcpListener(localadresse, this.port);
                server.Start();
                running = true;
                return true;
            }
            return false;
        }

        public void Stop()
        {
            if (client != null && client.Connected)
            {
                client.Close();
            }
            server.Stop();
            running = false;
        }

        public Task Accept()
        {
            return Task.Run(() =>
            {
                try
                {
                    // Buffer for reading data
                    Byte[] bytes = new Byte[256];
                    String data = null;

                    // Enter the listening loop.
                    while (running)
                    {
                        Console.Write("Waiting for a connection... ");

                        // Perform a blocking call to accept requests.
                        // You could also user server.AcceptSocket() here.
                        client = server.AcceptTcpClient();
                        Console.WriteLine("Connected!");

                        data = null;

                        // Get a stream object for reading and writing
                        stream = client.GetStream();

                        int i;

                        // Loop to receive all the data sent by the client.
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            data = Encoding.ASCII.GetString(bytes, 0, i);
                            Console.WriteLine("Received: {0}", data);
                        }

                        // Shutdown and end connection
                        client.Close();
                    }
                }
                catch (SocketException e)
                {
                    Console.WriteLine("SocketException: {0}", e);
                }
                finally
                {
                    // Stop listening for new clients.
                    server.Stop();
                }
            });
        }

        public Task SendData(string data)
        {
            return Task.Run(() =>
            {
                // Process the data sent by the client.
                data = data.ToUpper();

                byte[] msg = Encoding.ASCII.GetBytes(data);

                // Send back a response.
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine("Sent: {0}", data);
            });
        }
    }
}
