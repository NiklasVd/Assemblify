using Assemblify.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Assemblify.Gameplay
{
    public class UserPlayerControlComponent : Component
    {
        public Keys moveLeftKey = Keys.A,
            moveRightKey = Keys.D;

        private UserPlayerActor userPlayer;

        protected override void OnCreate()
        {
            userPlayer = (UserPlayerActor)parentActor;
            base.OnCreate();
        }
        protected override void OnUpdate(GameTime gameTime)
        {
            var currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(moveLeftKey))
            {
                userPlayer.PlayerMovement.Move(FacingDirection.Left, (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
            else if (currentKeyboardState.IsKeyDown(moveRightKey))
            {
                userPlayer.PlayerMovement.Move(FacingDirection.Right, (float)gameTime.ElapsedGameTime.TotalSeconds);
            }

            base.OnUpdate(gameTime);
        }
    }
}
