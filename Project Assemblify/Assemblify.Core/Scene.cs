using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assemblify.Core
{
    public class Scene
    {
        private static int actorInstanceIdGenerationNumber = 1;

        public readonly string name;
        public readonly SceneWorldSettings worldSettings;

        private List<Actor> actors;

        public Scene(string name)
        {
            this.name = name;
            worldSettings = new SceneWorldSettings();

            actors = new List<Actor>(500);
        }
        public Scene() : this("Unnamed scene")
        {
        }

        public T CreateActor<T>() where T : Actor
        {
            return CreateActor<T>(actorInstanceIdGenerationNumber++);
        }
        public T CreateActor<T>(int actorInstanceId) where T : Actor
        {
            return (T)CreateActor(typeof(T), actorInstanceId);
        }
        public Actor CreateActor(Type actorType, int actorInstanceId)
        {
            if (!actorType.IsAssignableFrom(typeof(Actor)))
            {
                return null;
            }
            else
            {
                var newActor = (Actor)Activator.CreateInstance(actorType, actorInstanceId);
                actors.Add(newActor);
                newActor.Initialize();

                newActor.Create();
                newActor.LoadResources(GameCenter.resourceDatabase);

                return newActor;
            }
        }

        public void DestroyActor(Actor actor)
        {
            if (actors.Remove(actor))
            {
                actor.Destroy();
            }
            else
            {
                Debug.Log("Destroying the actor " + actor.instanceId + " failed. Actor not found.");
            }
        }
        public void DestroyAllActors(Predicate<Actor> destroyCriteria)
        {
            actors.ForEach(a => DestroyActor(a));
        }
        public void DestroyAllActors()
        {
            DestroyAllActors((a) => true);
        }

        public Actor FindActor(int instanceId)
        {
            return actors.Find(a => a.instanceId == instanceId);
        }

        public void Update(GameTime gameTime)
        {
            var currentActors = actors.ToArray();
            for (int i = 0; i < actors.Count; i++)
            {
                currentActors[i].Update(gameTime);
            }
        }
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var currentActors = actors.ToArray();
            for (int i = 0; i < actors.Count; i++)
            {
                currentActors[i].Draw(gameTime, spriteBatch);
            }
        }
    }

    public class SceneWorldSettings
    {
        public Vector2 gravity = new Vector2(0, -1);
    }
}
