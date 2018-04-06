using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Awale.Models
{
    public class Serveur
    {
        private SocketPermission permission;
        private string port;
        private Socket sListener;
        Socket handler;

        public Serveur(string port)
        {
            this.port = port;
            permission = new SocketPermission(NetworkAccess.Accept,
                   TransportType.Tcp, "", SocketPermission.AllPorts);
        }

        private bool Run()
        {
            int portNumber =0;
            if(int.TryParse(port, out portNumber))
            {
                IPHostEntry ipHost = Dns.GetHostEntry("");
                IPAddress ipAddr = ipHost.AddressList[0];
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, portNumber);
                sListener = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                sListener.Bind(ipEndPoint);
                sListener.Listen(1);
                AsyncCallback aCallback = new AsyncCallback(AcceptCallback);
                sListener.BeginAccept(aCallback, sListener);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket listener = (Socket)ar.AsyncState;
            handler = listener.EndAccept(ar);
            byte[] buffernew = new byte[1024];
            object[] obj = new object[2];
            obj[0] = buffernew;
            obj[1] = handler;
            handler.BeginReceive(buffernew, 0, buffernew.Length,
                    SocketFlags.None, new AsyncCallback(ReceiveCallback), obj);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            
        }
    }
}
