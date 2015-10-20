using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Gameplay
{
    [Serializable]
    public class GameServerSettings
    {
        public string name;
        public string password;
        public int maxPlayers;

        public GameServerSettings(string name = "Assemblify Server", string password = "", int maxPlayers = 30)
        {
            this.name = name;
            this.password = password;
            this.maxPlayers = maxPlayers;
        }
    }
}
