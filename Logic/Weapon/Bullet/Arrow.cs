using AdilGame.Interfaces;
using Microsoft.Xna.Framework;
using PandaGameLibrary.Components;
using PandaGameLibrary.System;
using System;

namespace AdilGame.Logic.Weapons.bullet
{
    public class Arrow : Bullet
    {
        public override void OnHit(GameObject gameObject)
        {
            if (gameObject != null)
            {
                var status = gameObject.GetComponentByInterface<IDamageable>();
                if (status != null)
                {
                    Console.WriteLine("Arrow hit object");

                    status.DmgTaken(Damage);
                    IsActive = false;
                    IsEnabled = false;
                    Core.Instance.GameObjectSystem.RemoveGameObject(this.gameObject);
                }
            }
        }

        public override void Awake()
        {
            base.Awake();
            var arrowTexture = Render2D.LoadTexture("Weapon/Full Sheet", 16, 16);
            Render2D.DrawSprite(108);

        }

        public override void Update(GameTime gameTime)
        {
            Render2D.Origin = new Vector2(Render2D.SourceRectangle.Width / 2, Render2D.SourceRectangle.Height / 2);
            base.Update(gameTime);
        }

    }
}
