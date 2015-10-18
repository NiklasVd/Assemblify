using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Game
{
    [Serializable]
    public class Player
    {
        public readonly Color color;
        public readonly Guid localId;
        public readonly User userInfo;

        public Team team;

        public Player(Color color, Guid localId, User userInfo, Team team)
        {
            this.color = color;
            this.localId = localId;
            this.userInfo = userInfo;
            this.team = team;
        }
    }
}
