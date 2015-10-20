using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Gameplay
{
    [Serializable]
    public class Player
    {
        public readonly Vector3 color;
        public readonly User userInfo;

        public Team team;

        public Player(Vector3 color, User userInfo, Team team)
        {
            this.color = color;
            this.userInfo = userInfo;
            this.team = team;
        }
    }
}
