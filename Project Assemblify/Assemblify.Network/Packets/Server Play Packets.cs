using Assemblify.Game;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Network
{
    [Serializable]
    public struct ServerAnswerHandshakePacket : IServerPlayPacket
    {
        public readonly bool accepted;
        public readonly Player[] players;

        public ServerPlayPacketType Type
        {
            get { return ServerPlayPacketType.AnswerHandshake; }
        }

        public ServerAnswerHandshakePacket(bool accepted, Player[] players = null)
        {
            this.accepted = accepted;
            this.players = players;
        }
    }

    [Serializable]
    public class ServerWorldSnapshotPacket : IServerPlayPacket
    {
        public readonly List<SnapshotItem> snapshots;

        public ServerPlayPacketType Type
        {
            get { return ServerPlayPacketType.WorldSnapshot; }
        }

        public ServerWorldSnapshotPacket(List<SnapshotItem> snapshots)
        {
            this.snapshots = snapshots;
        }

        [Serializable]
        public class SnapshotItem
        {
            public readonly int actorResourceId,
                actorInstanceId;

            public SnapshotItem(int actorResourceId, int actorInstanceId)
            {
                this.actorResourceId = actorResourceId;
                this.actorInstanceId = actorInstanceId;
            }
        }
    }

    [Serializable]
    public class ServerCommandPacket : IServerPlayPacket
    {
        public readonly List<Command> commands;

        public ServerPlayPacketType Type
        {
            get { return ServerPlayPacketType.Command; }
        }

        public ServerCommandPacket(List<Command> commands)
        {
            this.commands = commands;
        }

        [Serializable]
        public class Command
        {
            public int actorInstanceId;
        }
    }

    [Serializable]
    public class ServerStatePacket : IServerPlayPacket
    {
        public readonly List<StateItem> states;

        public ServerPlayPacketType Type
        {
            get { return ServerPlayPacketType.State; }
        }

        public ServerStatePacket(List<StateItem> states)
        {
            this.states = states;
        }

        [Serializable]
        public class StateItem
        {
            public readonly float[] position;
            public readonly float rotation;

            public StateItem(float[] position, float rotation)
            {
                this.position = position;
                this.rotation = rotation;
            }
        }
    }
}
