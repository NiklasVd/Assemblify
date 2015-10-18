using Assemblify.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Gameplay
{
    public class PlayerActor : Actor
    {
        private RendererComponent renderer;
        public RendererComponent Renderer
        {
            get { return renderer; }
        }

        private ColliderComponent collider;
        public ColliderComponent Collider
        {
            get { return collider; }
        }

        private PlayerMovementComponent playerMovement;
        public PlayerMovementComponent PlayerMovement
        {
            get { return playerMovement; }
        }

        protected override void OnCreate()
        {
            renderer = AddComponent<RendererComponent>();
            collider = AddComponent<ColliderComponent>();

            playerMovement = AddComponent<PlayerMovementComponent>();
            playerMovement.movementSpeed = 8;

            base.OnCreate();
        }
    }
}
