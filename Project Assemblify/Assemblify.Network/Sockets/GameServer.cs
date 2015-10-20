using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Assemblify.Network
{
    public class GameServer<TServerPacket, TClientPacket> : Server<TServerPacket, TClientPacket>
        where TServerPacket : IServerPacket where TClientPacket : IClientPacket
    {
        private readonly UdpClient udpClient;
        private readonly IPEndPoint multicastEndPoint;

        public GameServer(int port, IPAddress multicastAddress)
            : base(port)
        {
            multicastEndPoint = new IPEndPoint(multicastAddress, 0);

            udpClient = new UdpClient(new IPEndPoint(IPAddress.Any, multicastEndPoint.Port));
            udpClient.JoinMulticastGroup(multicastEndPoint.Address);
        }
        ~GameServer()
        {
            udpClient.DropMulticastGroup(multicastEndPoint.Address); // Is this really needed?
            udpClient.Close();
        }

        public void SendMulticastPacket(TServerPacket packet)
        {
            var packetBytes = PacketConverter.ToBytes(packet);
            var sentBytes = udpClient.Send(packetBytes, packetBytes.Length, multicastEndPoint);

            if (sentBytes == 0)
            { /* What happens then? */ }
        }
    }
}
