using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Network
{
    public interface IPacket
    {
    }

    public interface IPacketRequestable
    {

    }

    #region Client
    public interface IClientPacket : IPacket
    {
    }

    public interface IClientPlayPacket : IClientPacket
    {
        ClientPlayPacketType Type { get; }
    }
    #endregion

    #region Server
    public interface IServerPacket : IPacket
    {
    }

    public interface IServerPlayPacket : IServerPacket
    {
        ServerPlayPacketType Type { get; }
    }
    #endregion
}
