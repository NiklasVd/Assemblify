using Assemblify.Gameplay;
using Assemblify.Network;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Assemblify.Gameplay
{
    public class GameServerMultiplayer
    {
        public const int serverPort = 36003;

        private readonly GameServer<IServerPlayPacket, IClientPlayPacket> server;
        private readonly GameServerSettings settings;
        private readonly Dictionary<int, Player> connectedPlayers;

        private readonly Stack<Color> newPlayerColors =
            new Stack<Color>(new[] { Color.Blue, Color.Red, Color.Green, Color.Yellow,
            Color.Azure, Color.Purple, Color.Brown, Color.DarkGray, Color.HotPink, Color.LightBlue, Color.LightGreen, Color.Lime }); // More colors!

        public GameServerMultiplayer(GameServerSettings settings)
        {
            server = new GameServer<IServerPlayPacket, IClientPlayPacket>(
                serverPort, IPAddress.Parse(GameClientMultiplayer.multicastAddress));
            server.OnReceiveClientPacket += OnReceiveClientPacket;

            this.settings = settings;
            connectedPlayers = new Dictionary<int, Player>();
        }

        public void Host()
        {
            server.Host();
        }
        public void Stop()
        {
            server.Close();
        }

        private void OnReceiveClientPacket(int connectionId, IClientPlayPacket packet)
        {
            switch (packet.Type)
            {
                case ClientPlayPacketType.Handshake:
                    var handshakePacket = (ClientPlayHandshakePacket)packet;
                    HandleHandshake(connectionId, handshakePacket);
                    break;
                case ClientPlayPacketType.Disconnect:
                    var disconnectPacket = (ClientPlayDisconnectPacket)packet;
                    HandleDisconnect(connectionId, disconnectPacket);
                    break;
                case ClientPlayPacketType.Request:
                    break;
                case ClientPlayPacketType.UploadConstruction:
                    break;
            }
        }

        private void HandleHandshake(int connectionId, ClientPlayHandshakePacket packet)
        {
            var acceptMode = ServerPlayAnswerHandshakePacket.AcceptMode.Success;

            if (settings.password != packet.password)
            {
                acceptMode = ServerPlayAnswerHandshakePacket.AcceptMode.WrongPassword;
            }
            else if (settings.maxPlayers < connectedPlayers.Count + 1)
            {
                acceptMode = ServerPlayAnswerHandshakePacket.AcceptMode.ServerFull;
            }
            else
            {
                server.SendPacket(new ServerPlayAnswerHandshakePacket(acceptMode, connectedPlayers.Values.ToArray()));

                var player = new Player(newPlayerColors.Pop().ToVector3(), packet.user, FindUnderpoweredTeam());
                connectedPlayers.Add(connectionId, player);

                server.SendPacket(new ServerPlayPlayerChangePacket(ServerPlayPlayerChangePacket.ChangeMode.Add, added: player));
                return;
            }

            server.SendPacket(new ServerPlayAnswerHandshakePacket(acceptMode));
        }
        private void HandleDisconnect(int connectionId, ClientPlayDisconnectPacket packet)
        {
            var disconnectedPlayerId = connectedPlayers[connectionId].userInfo.globalId;
            if (connectedPlayers.Remove(connectionId))
            {
                server.SendPacket(new ServerPlayPlayerChangePacket(ServerPlayPlayerChangePacket.ChangeMode.Remove, removedId: disconnectedPlayerId));
            }
        }

        // Utility
        private Color GetNewPlayerColor()
        {
            return newPlayerColors.Pop();
        }
        private Team FindUnderpoweredTeam()
        {
            var blueTeamCount = 0;
            var redTeamCount = 0;
            foreach (var player in connectedPlayers.Values)
            {
                if (player.team == Team.Blue)
                {
                    blueTeamCount++;
                }
                else if (player.team == Team.Red)
                {
                    redTeamCount++;
                }
            }

            if (blueTeamCount > redTeamCount)
            {
                return Team.Red;
            }
            else
            {
                return Team.Blue;
            }
        }
    }
}
