using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Assemblify.Core
{
    public class LabelUI : UIElement
    {
        public string text;
        public Color textColor;

        private SpriteFont font;
        public SpriteFont Font
        {
            get { return font; }
            set { font = value; }
        }

        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, Vector2.Zero, textColor);
            base.OnDraw(gameTime, spriteBatch);
        }
    }
}
