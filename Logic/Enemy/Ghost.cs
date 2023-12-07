using AdilGame.Components;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Enemy
{
    public class Ghost : Enemy
    {
        public override void Awake()
        {
            base.Awake();
            var Arthax = Render2d.LoadTexture("Character/girl", 32, 32);

            Render2d.AddAnimation("attack", new Animation(Arthax, 64, 71, 0.4f));
            Render2d.AddAnimation("Walk", new Animation(Arthax, 24, 31, 0.2f));
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
