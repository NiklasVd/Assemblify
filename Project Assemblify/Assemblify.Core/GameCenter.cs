using Assemblify.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public static class GameCenter
    {
        internal static readonly ResourceDatabase resourceDatabase;

        private static Scene scene;
        public static Scene Scene
        {
            get { return scene; }
        }

        static GameCenter()
        {
            resourceDatabase = new ResourceDatabase();
        }

        public static void Initialize()
        {
        }
        public static void LoadContent(ContentManager content, string[] texturePaths, string[] fontPaths, string[] scenePaths)
        {
            resourceDatabase.LoadContent(content,
                texturePaths, fontPaths, scenePaths);
        }

        public static void Update(GameTime gameTime)
        {
            scene.Update(gameTime);
        }
        public static void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.AlphaBlend, SamplerState.PointWrap); // Front to back or deferred?
            scene.Draw(gameTime, spriteBatch);
        }

        public static void SetScene(Scene newScene)
        {
            scene = newScene;
        }

        //public static T CreateActor<T>() where T : Actor
        //{
        //    return scene.CreateActor<T>();
        //}
        //public static T CreateActor<T>(int actorInstanceId) where T : Actor
        //{
        //    return scene.CreateActor<T>(actorInstanceId);
        //}

        //public static void DestroyActor(Actor actor)
        //{
        //    scene.DestroyActor(actor);
        //}
        //public static void DestroyActor(int actorInstanceId)
        //{
        //    scene.DestroyActor(
        //        FindActor(actorInstanceId));
        //}
        //public static Actor FindActor(int instanceId)
        //{
        //    return scene.FindActor(instanceId);
        //}
    }
}
