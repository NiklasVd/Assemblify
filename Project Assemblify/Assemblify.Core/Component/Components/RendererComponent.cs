using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Assemblify.Core
{
    public class RendererComponent : Component
    {
        public float layerDepth;

        private Texture2D selectedTexture;
        public Texture2D SelectedTexture
        {
            get { return selectedTexture; }
        }

        // Implement different origins?
        private Vector2 origin;
        public Vector2 Origin
        {
            get { return origin; }
        }

        public void SetTexture(Texture2D texture)
        {
            selectedTexture = texture;
            origin = texture.Bounds.Size.ToVector2() / 2;
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (selectedTexture != null)
            {
                spriteBatch.Draw(selectedTexture, parentActor.Transform.TotalPosition, null, null,
                    Origin, parentActor.Transform.TotalRotation, parentActor.Transform.TotalScale, Color.White, SpriteEffects.None, layerDepth);
            }

            base.OnDraw(gameTime, spriteBatch);
        }
    }
}
