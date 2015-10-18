using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assemblify.Core;

namespace Assemblify.Gameplay
{
    public class UserPlayerActor : PlayerActor
    {
        private UserPlayerControlComponent userPlayerControl;
        public UserPlayerControlComponent UserPlayerControl
        {
            get { return userPlayerControl; }
        }

        protected override void OnCreate()
        {
            userPlayerControl = AddComponent<UserPlayerControlComponent>();
            base.OnCreate();
        }

        protected override void OnLoadResources(ResourceDatabase resourceDatabase)
        {
            Renderer.SetTexture(resourceDatabase.Textures["Textures/Player 1"]);
            base.OnLoadResources(resourceDatabase);
        }
    }
}
