using Assemblify.Gameplay;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Network
{
    [Serializable]
    public struct ClientPlayHandshakePacket : IClientPlayPacket, IPacketRequestable
    {
        public readonly User user;
        public readonly string password;

        public ClientPlayPacketType Type
        {
            get { return ClientPlayPacketType.Handshake; }
        }

        public ClientPlayHandshakePacket(User user, string password)
        {
            this.user = user;
            this.password = password;
        }
    }

    [Serializable]
    public struct ClientPlayDisconnectPacket : IClientPlayPacket
    {
        public ClientPlayPacketType Type
        {
            get { return ClientPlayPacketType.Disconnect; }
        }
    }

    [Serializable]
    public struct ClientPlayRequestPacket : IClientPlayPacket
    {
        public readonly sbyte horizontalMovement;
        public readonly bool jump;
        public readonly int enterAssemblyInstanceId;

        public ClientPlayPacketType Type
        {
            get { return ClientPlayPacketType.Request; }
        }

        public ClientPlayRequestPacket(sbyte horizontalMovement = 0, bool jump = false, int enterAssemblyInstanceId = 0)
        {
            this.horizontalMovement = horizontalMovement;
            this.jump = jump;
            this.enterAssemblyInstanceId = enterAssemblyInstanceId;
        }
    }

    [Serializable]
    public struct ClientPlayUploadConstructionPacket : IClientPlayPacket
    {
        public ClientPlayPacketType Type
        {
            get { return ClientPlayPacketType.UploadConstruction; }
        }
    }
}
