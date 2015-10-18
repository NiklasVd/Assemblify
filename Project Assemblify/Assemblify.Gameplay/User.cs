using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Game
{
    [Serializable]
    public class User
    {
        public string name;
        public Guid globalId;

        public User() : this("")
        {
        }
        public User(string name)
        {
            this.name = name;
            globalId = Guid.NewGuid();
        }
    }
}
