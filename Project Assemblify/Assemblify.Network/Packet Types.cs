using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Network
{
    [Serializable]
    public enum ClientPlayPacketType : byte
    {
        Handshake,
        Disconnect,
        Request,
        UploadConstruction
    }
    [Serializable]
    public enum ServerPlayPacketType : byte
    {
        AnswerHandshake,
        WorldSnapshot,
        Command,
        State
    }
}
