using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public class ContentResourceLoader<T> : IResourceLoader<T>
    {
        private readonly ContentManager contentManager;

        public ContentResourceLoader(ContentManager contentManager)
        {
            this.contentManager = contentManager;
        }

        public T Load(string path)
        {
            return contentManager.Load<T>(path);
        }
    }
}
