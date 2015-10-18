using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Assemblify.Core
{
    public class RigidbodyComponent : Component
    {
        public float mass;
        public Vector2 velocity;

        public void AddForce(Vector2 force)
        {
        }

        protected override void OnUpdate(GameTime gameTime)
        {
            ApplyVelocity((float)gameTime.ElapsedGameTime.TotalSeconds);
            base.OnUpdate(gameTime);
        }

        private void ApplyVelocity(float deltaTime)
        {
            velocity += (GameCenter.Scene.worldSettings.gravity * mass) * deltaTime;
            parentActor.Transform.Position += velocity;
        }
    }
}
