using Assemblify.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assemblify.Gameplay
{
    public static class GameNetworkResources
    {
        private static int actorIdGenerationNumber = 1;
        private static Dictionary<int, Type> actors;

        static GameNetworkResources()
        {
            actors = new Dictionary<int, Type>();
            RegisterActorType(typeof(UserPlayerActor));
        }

        public static Type GetActorTypeByID(int id)
        {
            return actors[id];
        }

        private static void RegisterActorType(Type type)
        {
            actors.Add(actorIdGenerationNumber++, type);
        }
    }
}
