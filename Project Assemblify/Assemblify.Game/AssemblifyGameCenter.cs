using Assemblify.Core;
using Assemblify.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Assemblify.Gameplay
{
    public class AssemblifyGameCenter
    {
        public readonly User localUser;
        private readonly GameClientMultiplayer multiplayer;

        public AssemblifyGameCenter()
        {
            localUser = new User("Test Player " + new Random().Next(0, 100));
            multiplayer = new GameClientMultiplayer(localUser);
        }

        public void Initialize()
        {
            GameCenter.Initialize();
            GameCenter.SetScene(new Scene("World"));
        }

        public void Exit()
        {
            multiplayer.Disconnect();
        }

        public void LoadContent(ContentManager content)
        {
            var texturePaths = new[]
            {
                "Textures/Player 1"
            };
            var fontPaths = new[]
            {
                "Fonts/Courier New"
            };
            //var scenePaths = new[]
            //{
            //    ""
            //};

            GameCenter.LoadContent(content, texturePaths, fontPaths, new string[0]);

            multiplayer.Connect(new IPEndPoint(IPAddress.Loopback, 36003), "");
            Debug.Log("Connected to server");
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
