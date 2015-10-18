using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Assemblify.Core
{
    public class UIElement : Actor
    {
        private RendererComponent renderer;
        public RendererComponent Renderer
        {
            get { return renderer; }
        }

        protected virtual void OnClick(Vector2 relativePosition)
        {
        }

        protected override void OnCreate()
        {
            renderer = AddComponent<RendererComponent>();
            base.OnCreate();
        }
        protected override void OnUpdate(GameTime gameTime)
        {
            var currentMouseState = Mouse.GetState();
            if (currentMouseState.LeftButton == ButtonState.Pressed &&
                renderer.SelectedTexture.Bounds.Contains(currentMouseState.Position))
            {
                OnClick(currentMouseState.Position.ToVector2() - Transform.TotalPosition); // is this the relative position?
            }

            base.OnUpdate(gameTime);
        }
    }
}
