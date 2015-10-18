using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public class TransformComponent : Component
    {
        private Vector2 position, scale;
        private float rotation;

        public TransformComponent parent;
        private byte dirtyFlags;

        public Vector2 Position
        {
            get { return position; }
            set
            {
                dirtyFlags |= 1 << 0;
                position = value;
            }
        }
        public Vector2 TotalPosition
        {
            get
            {
                return parent != null ?
                  parent.TotalPosition + position : position;
            }
        }

        public Vector2 Scale
        {
            get { return scale; }
            set
            {
                dirtyFlags |= 1 << 1;
                scale = value;
            }
        }
        public Vector2 TotalScale
        {
            get
            {
                return parent != null ?
                  parent.TotalScale + Scale : Scale;
            }
        }

        public float Rotation
        {
            get { return rotation; }
            set
            {
                dirtyFlags |= 1 << 2;
                rotation = value;
            }
        }
        public float TotalRotation
        {
            get
            {
                return parent != null ?
                  parent.TotalRotation + Rotation : Rotation;
            }
        }

        internal bool IsDirty(byte flag)
        {
            return (dirtyFlags & flag) == flag;
        }
        internal void ResetDirtyFlag(byte flag)
        {
            dirtyFlags ^= flag;
        }

        protected override void OnCreate()
        {
            scale = Vector2.One;
            base.OnCreate();
        }
    }
}
