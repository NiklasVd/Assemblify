using Assemblify.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Gameplay
{
    public class PlayerMovementComponent : Component
    {
        public float movementSpeed;

        private FacingDirection currentFacingDirection;
        public FacingDirection CurrentFacingDirection
        {
            get { return currentFacingDirection; }
        }

        public void Move(float deltaTime)
        {
            Move(currentFacingDirection, deltaTime);
        }
        public void Move(FacingDirection f, float deltaTime)
        {
            if (currentFacingDirection != f)
            {
                parentActor.Transform.Scale = new Vector2(((int)f) * parentActor.Transform.Scale.X, parentActor.Transform.Scale.Y);
            }
            currentFacingDirection = f;

            var currentPos = parentActor.Transform.Position;
            if (currentFacingDirection == FacingDirection.Left)
            {
                currentPos.X -= movementSpeed * deltaTime;
                parentActor.Transform.Position = currentPos;
            }
            else
            {
                currentPos.X += movementSpeed * deltaTime;
                parentActor.Transform.Position = currentPos;
            }
        }
    }

    public enum FacingDirection // Every player sprite needs to face to the left
    {
        Left = 1,
        Right = -1
    }
}
