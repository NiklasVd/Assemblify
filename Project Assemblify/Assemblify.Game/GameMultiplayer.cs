using Assemblify.Core;
using Assemblify.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Assemblify.Game
{
    internal class GameMultiplayer
    {
        public const int clientPort = 36005;
        public const string multicastAddress = "224.5.6.7";

        private GameClient<IClientPlayPacket, IServerPlayPacket> client;

        public GameMultiplayer()
        {
            client = new GameClient<IClientPlayPacket, IServerPlayPacket>(new IPEndPoint(IPAddress.Any, clientPort),
                IPAddress.Parse(multicastAddress));
            client.OnReceivePacket += OnReceivePacket;
            client.OnReceiveUdpPacket += OnReceiveUdpPacket;
        }

        public Actor InstantiateActor(int resourceId, int instanceId)
        {
            var actor = GameCenter.Scene.CreateActor(
                GameNetworkResources.GetActorTypeByID(resourceId), instanceId);
            actor.AddComponent<NetworkComponent>();
            actor.tag |= ActorTypeTag.NetworkInstantiated;
        }

        private void OnReceivePacket(IServerPlayPacket packet)
        {
            switch (packet.Type)
            {
                case ServerPlayPacketType.WorldSnapshot:
                    break;
                case ServerPlayPacketType.Command:
                    break;
            }
        }
        private void OnReceiveUdpPacket(IServerPlayPacket packet)
        {
            switch (packet.Type)
            {
                case ServerPlayPacketType.State:
                    break;
            }
        }

        private void HandleWorldSnapshot(ServerWorldSnapshotPacket packet)
        {
            GameCenter.Scene.DestroyAllActors((a) => a.tag.HasFlag(ActorTypeTag.Content)); // Destroy everything thats not UI
            packet.snapshots.ForEach(s =>
            {
                var actor = InstantiateActor(s.actorResourceId, s.actorInstanceId);
            });
        }
    }
}
