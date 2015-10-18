using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Network.Utility
{
    public class DirtyState // Really needed?
    {
        private byte flags;

        public bool this[byte flag]
        {
            get { return (flags & flag) == flag; }
        }

        public DirtyState()
        {
        }

        public void Clear()
        {
            flags = 0;
        }

        public static DirtyState operator +(DirtyState me, byte flag)
        {
            me.flags |= flag;
            return me;
        }
        public static DirtyState operator -(DirtyState me, byte flag)
        {
            me.flags = (byte)~flag;
            return me;
        }
    }
}
