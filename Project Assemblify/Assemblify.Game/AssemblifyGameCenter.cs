using Assemblify.Core;
using Assemblify.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Game
{
    public class AssemblifyGameCenter
    {
        private GameMultiplayer multiplayer;

        public void Initialize()
        {
            GameCenter.Initialize();
        }
        public void LoadContent(ContentManager content)
        {
            var texturePaths = new[]
            {
                "Textures/Player 1"
            };
            var fontPaths = new[]
            {
                "Courier New"
            };
            var scenePaths = new[]
            {
                ""
            };

            GameCenter.LoadContent(content, texturePaths, fontPaths, scenePaths);
        }
        public void Update(GameTime gameTime)
        {
            GameCenter.Update(gameTime);
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GameCenter.Draw(gameTime, spriteBatch);
        }
    }
}
