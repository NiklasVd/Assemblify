using Assemblify.Gameplay;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Network
{
    [Serializable]
    public struct ServerPlayAnswerHandshakePacket : IServerPlayPacket
    {
        public readonly AcceptMode acceptMode;
        public readonly Player[] players;
        public readonly GameServerSettings settings;

        public ServerPlayPacketType Type
        {
            get { return ServerPlayPacketType.AnswerHandshake; }
        }

        public ServerPlayAnswerHandshakePacket(AcceptMode acceptMode, Player[] players = null, GameServerSettings settings = null)
        {
            this.acceptMode = acceptMode;
            this.players = players;
            this.settings = settings;
        }

        [Serializable]
        public enum AcceptMode : byte
        {
            Success,
            WrongPassword,
            ServerFull,
            InternalError
        }
    }

    [Serializable]
    public class ServerPlayPlayerChangePacket : IServerPlayPacket
    {
        public readonly ChangeMode mode;

        public readonly Player added;
        public readonly Player updated;
        public readonly Guid removedId;

        public ServerPlayPacketType Type
        {
            get { return ServerPlayPacketType.PlayerChange; }
        }

        public ServerPlayPlayerChangePacket(ChangeMode mode, Player added = null, Player updated = null, Guid removedId = default(Guid))
        {
            this.mode = mode;
            this.added = added;
            this.updated = updated;
            this.removedId = removedId;
        }

        [Serializable]
        public enum ChangeMode : byte
        {
            Add,
            Update,
            Remove
        }
    }

    [Serializable]
    public class ServerPlayWorldSnapshotPacket : IServerPlayPacket
    {
        public readonly List<SnapshotItem> snapshots;

        public ServerPlayPacketType Type
        {
            get { return ServerPlayPacketType.WorldSnapshot; }
        }

        public ServerPlayWorldSnapshotPacket(List<SnapshotItem> snapshots)
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
    public class ServerPlayCommandPacket : IServerPlayPacket
    {
        public readonly List<Command> commands;

        public ServerPlayPacketType Type
        {
            get { return ServerPlayPacketType.Command; }
        }

        public ServerPlayCommandPacket(List<Command> commands)
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
    public class ServerPlayStatePacket : IServerPlayPacket
    {
        public readonly List<StateItem> states;

        public ServerPlayPacketType Type
        {
            get { return ServerPlayPacketType.State; }
        }

        public ServerPlayStatePacket(List<StateItem> states)
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
