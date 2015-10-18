using NetSerializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Reaction.Core.Network.Utility
{
    internal class PacketTypeSerializer : ITypeSerializer
    {
        public IEnumerable<Type> GetSubtypes(Type type)
        {
            throw new NotImplementedException();
        }

        public bool Handles(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
