using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Core
{
    public class ColliderComponent : Component
    {
        private static readonly List<ColliderComponent> registeredColliders = new List<ColliderComponent>();

        public int layer;
        public Vector2 size;
        private BoundingBox boundingBox;

        protected override void OnCreate()
        {
            registeredColliders.Add(this);

            GenerateBoundingBox();
            base.OnCreate();
        }
        protected override void OnUpdate(GameTime gameTime)
        {
            CheckForCollisions();
            base.OnUpdate(gameTime);
        }
        protected override void OnDestroy()
        {
            registeredColliders.Remove(this);
            base.OnDestroy();
        }

        private void GenerateBoundingBox()
        {
            boundingBox = new BoundingBox(new Vector3(parentActor.Transform.TotalPosition, 0),
                new Vector3(parentActor.Transform.TotalPosition + size, 0));
        }
        private void CheckForCollisions()
        {
            if ((parentActor.Transform.IsDirty(1)))
            {
                var intersectingColliders = registeredColliders.FindAll(c => c.boundingBox.Intersects(boundingBox) && c.layer == layer); // This doesnt work, performance...
                intersectingColliders.ForEach(c =>
                {
                    var rigidbody = c.parentActor.GetComponent<RigidbodyComponent>();
                    if (rigidbody != null)
                    {
                        rigidbody.AddForce(parentActor.Transform.TotalPosition - c.parentActor.Transform.TotalPosition);
                    }

                    // Put this object back so that its not colliding anymore
                });

                parentActor.Transform.ResetDirtyFlag(1);
            }
        }
    }
}
