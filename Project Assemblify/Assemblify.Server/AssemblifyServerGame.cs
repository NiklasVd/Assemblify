using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Assemblify.Gameplay
{
    public class AssemblifyServerGame : Game
    {
        public AssemblifyServerGameCenter gameCenter;

        private SpriteBatch spriteBatch;
        private GraphicsDeviceManager graphicsDeviceManager;

        protected override void Initialize()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            gameCenter.Initialize();

            base.Initialize();
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            gameCenter.Exit();
            base.OnExiting(sender, args);
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();
        }

        protected override void Update(GameTime gameTime)
        {
            gameCenter.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            gameCenter.Draw(gameTime, spriteBatch);
            base.Draw(gameTime);
        }
    }
}
