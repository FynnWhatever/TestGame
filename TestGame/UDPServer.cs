using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace TestGame
{
    class UDPServer
    {
        UdpClient receivingUdpClient;
        IPEndPoint RemoteIpEndPoint;
        private Boolean _isRunning;
        private Game1 game;
        int port;

        public UDPServer(int port, Game1 g1)
        {
            game = g1;
            receivingUdpClient = receivingUdpClient = new UdpClient(port);
            RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, port);
            _isRunning = true;
            this.port = port;
        }

        public void LoopClients()
        {
            
                receivingUdpClient.BeginReceive(new AsyncCallback(HandlePacket), null);
            
        }

        public void HandlePacket(IAsyncResult ar)
        {
            PlayerInput input = null;
            
            Byte[] rcvBytes = receivingUdpClient.EndReceive(ar,ref RemoteIpEndPoint);
            receivingUdpClient.BeginReceive(new AsyncCallback(HandlePacket), null);
            if (rcvBytes == null || rcvBytes.Length == 0)
                return;
            String rcv = System.Text.Encoding.ASCII.GetString(rcvBytes);
            if (rcv.Equals(""))
                return;
            input = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<PlayerInput>(rcv);

            if (input == null)
                return;

            game.MovePlayer(input.id, input.x, input.y);
        }
    }
}
