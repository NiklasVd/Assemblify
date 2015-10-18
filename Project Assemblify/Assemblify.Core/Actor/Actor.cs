using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public class Actor : Entity
    {
        public readonly int instanceId;
        private List<Component> components;
        
        private TransformComponent transform;
        public TransformComponent Transform
        {
            get { return transform; }
        }

        public ActorTypeTag tag;

        internal Actor(int instanceId) : this()
        {
            this.instanceId = instanceId;
        }
        protected Actor()
        {
            components = new List<Component>(10);
        }

        public T AddComponent<T>() where T : Component
        {
            if (GetComponent<T>() == null)
            {
                var newComponent = Activator.CreateInstance<T>();
                newComponent.parentActor = this;

                components.Add(newComponent);

                newComponent.Create();
                return newComponent;
            }
            else
            {
                Debug.Log("You cannot add a component of the same type twice.");
                return null;
            }
        }
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Count; i++)
            {
                var componentT = components[i] as T;
                if (componentT != null)
                {
                    return componentT;
                }
            }
            
            return null;
        }
        public void DestroyComponent<T>() where T : Component
        {
            var component = GetComponent<T>();
            if (components.Remove(component))
            {
                component.Destroy();
            }
            else
                Debug.Log("The component of type " + typeof(T).Name + " is not attached to this actor.");

        }

        internal void Initialize()
        {
            transform = AddComponent<TransformComponent>();
        }

        protected override void OnCreate()
        {
            base.OnCreate();
        }
        protected override void OnLoadResources(ResourceDatabase resourceDatabase)
        {
            for (int i = 0; i < components.Count; i++)
                components[i].LoadResources(resourceDatabase);
            base.OnLoadResources(resourceDatabase);
        }
        protected override void OnUpdate(GameTime gameTime)
        {
            for (int i = 0; i < components.Count; i++)
                components[i].Update(gameTime);
            base.OnUpdate(gameTime);
        }
        protected override void OnDraw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            if (enabled)
            {
                for (int i = 0; i < components.Count; i++)
                    components[i].Draw(gameTime, spriteBatch);
            }

            base.OnDraw(gameTime, spriteBatch);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }

    [Flags]
    public enum ActorTypeTag
    {
        None,
        Content,
        NetworkInstantiated
    }
}
