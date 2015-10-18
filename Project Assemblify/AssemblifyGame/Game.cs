using Assemblify.Core;
using Assemblify.Game;
using Assemblify.Gameplay;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AssemblifyGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        public AssemblifyGameCenter gameCenter;
        public SpriteFont debugFont; // Courier New

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public Game()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.PreferredBackBufferWidth = 1240;
            graphics.PreferredBackBufferHeight = 840;
            graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content. Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            gameCenter.Initialize();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            gameCenter.LoadContent(Content);
            debugFont = Content.Load<SpriteFont>("Fonts/Courier New");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Content.Unload(); call?
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            gameCenter.Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            gameCenter.Draw(gameTime, spriteBatch);
            DrawLogMessages();

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private void DrawLogMessages()
        {
            if (Program.IsDebugMode)
            {
                var logMessages = Debug.LogMessages;
                var position = new Vector2(10, 10);
                const float yAdditive = 20;

                for (int i = 0; i < 5; i++)
                {
                    if (logMessages.Count <= i)
                        break;
                    var logMessage = logMessages[i];

                    spriteBatch.DrawString(debugFont, logMessage.ToString(), position, Color.White);
                    position.Y += yAdditive;
                }
            }
        }
    }
}
