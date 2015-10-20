using Assemblify.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Gameplay
{
    public class AssemblifyServerGameCenter
    {
        public readonly GameServerSettings settings;
        private readonly GameServerMultiplayer multiplayer;
        private readonly Scene worldScene;

        public AssemblifyServerGameCenter()
        {
            settings = new GameServerSettings();
            multiplayer = new GameServerMultiplayer(new GameServerSettings());

            worldScene = new Scene("World");
        }

        public void Initialize()
        {
            GameCenter.Initialize();
            GameCenter.SetScene(worldScene);

            multiplayer.Host();
            Debug.Log("Hosting server");
        }

        public void Exit()
        {
            multiplayer.Stop();
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
