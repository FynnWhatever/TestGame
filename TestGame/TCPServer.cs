using System;
using System.Net.Sockets;
using System.IO;
using System.Text;
using System.Threading;
using System.Net;
using System.Web;

namespace TestGame
{
    class TCPServer
    {
        private TcpListener _server;
        private Boolean _isRunning;
        private Game1 game;
        int port;

        public TCPServer(int port, Game1 g1)
        {
            game = g1;
            _server = new TcpListener(IPAddress.Any, port);
            _server.Start();
            _isRunning = true;
            this.port = port;
        }

        public void LoopClients()
        {
            while (_isRunning)
            {
                TcpClient newClient = _server.AcceptTcpClient();
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newClient);
            }
        }

        public void HandleClient(object obj)
        {

            TcpClient client = (TcpClient)obj;
            NetworkStream nStream = client.GetStream();
            string ip = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
            Boolean bClientConnected = true;


            byte[] rcvLenBytesID = new byte[4];
            nStream.Read(rcvLenBytesID, 0, 4);

            if (BitConverter.IsLittleEndian) //Java ist BigEndian, falls hier LittleEndian gilt wird getauscht
                Array.Reverse(rcvLenBytesID);

            int rcvLenID = System.BitConverter.ToInt32(rcvLenBytesID, 0);
            byte[] rcvBytesID = new byte[rcvLenID];
            nStream.Read(rcvBytesID, 0, rcvLenID);
            String id = System.Text.Encoding.ASCII.GetString(rcvBytesID);

            ThreadStart starter = delegate { RecieveUDP(id, ip); };
            Thread thread = new Thread(starter);
            thread.Start();

            game.CreatePlayer(id);

            while (bClientConnected)
            {
                /*
                 *Do some TCP HANDLING here 
                */

                /*
                byte[] rcvLenBytes = new byte[4];
                nStream.Read(rcvLenBytes,0,4);
                
                if (BitConverter.IsLittleEndian) //Java ist BigEndian, falls hier LittleEndian gilt wird getauscht
                    Array.Reverse(rcvLenBytes);
                    
                int rcvLen = System.BitConverter.ToInt32(rcvLenBytes, 0);
                byte[] rcvBytes = new byte[rcvLen];
                nStream.Read(rcvBytes,0,rcvLen);
                String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);
                if (rcv.Equals(""))
                    break;
                PlayerInput input = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PlayerInput>(rcv);
                if (input == null)
                    break;
                

                game.MovePlayer(id,input.x,input.y);
                input = null;

                */

            }
            game.DestroyPlayer(id);
            nStream.Close();
            client.Close();
        }

        void RecieveUDP(string id, string ip)
        {
            UdpClient receivingUdpClient = new UdpClient(port);
            IPAddress udpAddress = IPAddress.Parse(ip);
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(udpAddress, port);

            Boolean bClientConnected = true;

            while (bClientConnected)
            {
                Byte[] rcvBytes = receivingUdpClient.Receive(ref RemoteIpEndPoint);
                String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);
                if (rcv.Equals(""))
                    break;
                PlayerInput input = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PlayerInput>(rcv);
                if (input == null)
                    break;

                game.MovePlayer(id, input.x, input.y);
                input = null;
            }
        }
    }
}