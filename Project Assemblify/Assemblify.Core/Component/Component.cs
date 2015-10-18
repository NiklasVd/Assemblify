using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public abstract class Component : Entity
    {
        public Actor parentActor;

        protected Component()
        {
        }
    }
}
