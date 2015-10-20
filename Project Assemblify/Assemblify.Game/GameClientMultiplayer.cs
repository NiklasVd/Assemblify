using Assemblify.Core;
using Assemblify.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Assemblify.Gameplay
{
    public class GameClientMultiplayer
    {
        public const int clientPort = 36005;
        public const string multicastAddress = "224.5.6.7";

        private Dictionary<Guid, Player> connectedPlayers;

        private GameServerSettings currentServerSettings;
        public GameServerSettings CurrentServerSettings
        {
            get { return currentServerSettings; }
        }
        
        private readonly GameClient<IClientPlayPacket, IServerPlayPacket> client;
        private User localUser;

        public GameClientMultiplayer(User localUser)
        {
            connectedPlayers = new Dictionary<Guid, Player>();

            client = new GameClient<IClientPlayPacket, IServerPlayPacket>(new IPEndPoint(IPAddress.Any, clientPort),
                IPAddress.Parse(multicastAddress));

            client.OnReceivePacket += OnReceivePacket;
            client.OnReceiveUdpPacket += OnReceiveUdpPacket;

            this.localUser = localUser;
        }

        public void Connect(IPEndPoint endPoint, string password)
        {
            client.Connect(endPoint);
            HandleAnswerHandshake(client.SendRequest<ClientPlayHandshakePacket, ServerPlayAnswerHandshakePacket>(
                new ClientPlayHandshakePacket(localUser, password)));
        }
        public void Disconnect()
        {
            client.SendPacket(new ClientPlayDisconnectPacket());
            client.Disconnect();
        }

        public Actor InstantiateActor(int resourceId, int instanceId)
        {
            var actor = GameCenter.Scene.CreateActor(
                GameNetworkResources.GetActorTypeByID(resourceId), instanceId);

            actor.AddComponent<NetworkComponent>();
            actor.tag |= ActorTypeTag.NetworkInstantiated;

            return actor;
        }

        private void OnReceivePacket(IServerPlayPacket packet)
        {
            switch (packet.Type)
            {
                case ServerPlayPacketType.WorldSnapshot:
                    var worldSnapshotPacket = (ServerPlayWorldSnapshotPacket)packet;
                    HandleWorldSnapshot(worldSnapshotPacket);
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

        private void HandleWorldSnapshot(ServerPlayWorldSnapshotPacket packet)
        {
            GameCenter.Scene.DestroyAllActors((a) => a.tag.HasFlag(ActorTypeTag.Content)); // Destroy everything thats content
            packet.snapshots.ForEach(s =>
            {
                var actor = InstantiateActor(s.actorResourceId, s.actorInstanceId);
            });
        }
        private void HandleAnswerHandshake(ServerPlayAnswerHandshakePacket packet)
        {
            var answerHandshake = client.SendRequest<ClientPlayHandshakePacket, ServerPlayAnswerHandshakePacket>(new ClientPlayHandshakePacket());
            if (answerHandshake.acceptMode == ServerPlayAnswerHandshakePacket.AcceptMode.Success)
            {
                currentServerSettings = answerHandshake.settings;

                Debug.Log("Accepted by the server");
                Debug.Log("Server settings:" +
                    "\n  Name: " + currentServerSettings.name +
                    "\n  Max. players: " + currentServerSettings.maxPlayers);
            }
            else
            {
                Debug.Log("Rejected by server, reason: " + answerHandshake.acceptMode);
                client.Disconnect(); // Do not use Disconnect() because it sends a disconnect packet to the server (but practically we are not even connected)
            }
        }
        private void HandlePlayerChangePacket(ServerPlayPlayerChangePacket packet)
        {
            switch (packet.mode)
            {
                case ServerPlayPlayerChangePacket.ChangeMode.Add:
                    connectedPlayers.Add(packet.added.userInfo.globalId, packet.added);
                    Debug.Log("The player \"" + packet.added.userInfo.name + "\" connected");
                    break;
                case ServerPlayPlayerChangePacket.ChangeMode.Update:
                    connectedPlayers[packet.updated.userInfo.globalId] = packet.updated;
                    break;
                case ServerPlayPlayerChangePacket.ChangeMode.Remove:
                    var playerRemove = connectedPlayers[packet.removedId];
                    connectedPlayers.Remove(packet.removedId);

                    Debug.Log("The player \"" + playerRemove.userInfo.name + "\" disconnected");
                    break;
            }
        }
    }
}
