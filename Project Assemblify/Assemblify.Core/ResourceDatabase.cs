using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public class ResourceDatabase
    {
        private ResourceDictionary<Texture2D> textures;
        public ResourceDictionary<Texture2D> Textures
        {
            get { return textures; }
        }

        private ResourceDictionary<SpriteFont> fonts;
        public ResourceDictionary<SpriteFont> Fonts
        {
            get { return fonts; }
        }

        private ResourceDictionary<Scene> scenes;
        public ResourceDictionary<Scene> Scenes
        {
            get { return scenes; }
        }

        public ResourceDatabase()
        {
        }

        internal void LoadContent(ContentManager content, string[] texturePaths, string[] fontPaths, string[] scenePaths)
        {
            textures = new ResourceDictionary<Texture2D>(new ContentResourceLoader<Texture2D>(content));
            fonts = new ResourceDictionary<SpriteFont>(new ContentResourceLoader<SpriteFont>(content));
            scenes = new ResourceDictionary<Scene>(new SceneResourceLoader());

            for (int i = 0; i < texturePaths.Length; i++)
                textures.Load(texturePaths[i]);
            for (int i = 0; i < fontPaths.Length; i++)
                fonts.Load(fontPaths[i]);
            for (int i = 0; i < scenePaths.Length; i++)
                scenes.Load(scenePaths[i]);
        }
    }
}
