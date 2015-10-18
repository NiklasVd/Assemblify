using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace Assemblify.Core
{
    public class ButtonUI : UIElement
    {
        public delegate void OnClickButtonHandler(Vector2 relativePosition);
        public event OnClickButtonHandler OnClickButton;

        public LabelUI textLabel { get; private set; }

        protected override void OnCreate()
        {
            textLabel = GameCenter.Scene.CreateActor<LabelUI>();
            textLabel.Transform.parent = Transform;

            base.OnCreate();
        }
        protected override void OnClick(Vector2 relativePosition)
        {
            if (OnClickButton != null)
            {
                OnClickButton(relativePosition);
            }

            base.OnClick(relativePosition);
        }
    }
}
