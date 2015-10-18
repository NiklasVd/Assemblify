using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public abstract class Entity
    {
        public bool enabled;

        protected Entity()
        {
            enabled = true;
        }
        
        internal void Create()
        {
            OnCreate();
        }
        internal void LoadResources(ResourceDatabase resourceDatabase)
        {
            OnLoadResources(resourceDatabase);
        }
        internal void Update(GameTime gameTime)
        {
            if (enabled)
            {
                OnUpdate(gameTime);
            }
        }
        internal void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            OnDraw(gameTime, spriteBatch);
        }
        internal void Destroy()
        {
            OnDestroy();
        }

        protected virtual void OnCreate()
        {
        }
        protected virtual void OnLoadResources(ResourceDatabase resourceDatabase)
        {
        }
        protected virtual void OnUpdate(GameTime gameTime)
        {
        }
        protected virtual void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
        }
        protected virtual void OnDestroy()
        {
        }
    }
}
