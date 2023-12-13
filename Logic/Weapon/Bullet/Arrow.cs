using AdilGame.Components;
using AdilGame.Interfaces;
using AdilGame.System;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Weapons.bullet
{
    public class Arrow : Bullet
    {
        public override void OnHit(GameObject gameObject)
        {
            var status = gameObject.GetComponentByInterface<IStatus>();
            if (status != null)
            {
                Console.WriteLine("Arrow hit object");

                status.DmgTaken(Damage);
                IsActive = false;
                IsEnabled = false;
                Core.Instance.GameObjectSystem.RemoveGameObject(this.gameObject);
            }

        }

        internal override void Awake()
        {
            base.Awake();
            var arrowTexture = Render2D.LoadTexture("Weapon/Full Sheet",16,16);
            Render2D.DrawSprite(108);

        }

        internal override void Update(GameTime gameTime)
        {
            Render2D.Origin = new Vector2(Render2D.SourceRectangle.Width /2 , Render2D.SourceRectangle.Height /2);
            base.Update(gameTime);
        }

    }
}
