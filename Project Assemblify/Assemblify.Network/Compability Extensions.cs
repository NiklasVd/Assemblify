using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Network
{
    public static class CompabilityExtensions
    {
        public static Vector2 ToVector2(this float[] array)
        {
            return new Vector2(array[0], array[1]);
        }

        public static float[] ToArray(this Vector2 vector)
        {
            return new[] { vector.X, vector.Y };
        }
    }
}
