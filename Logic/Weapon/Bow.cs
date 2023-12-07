using AdilGame.Logic.Weapon.bullet;
using AdilGame.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Weapon
{
    public class Bow : Weapon
    {
        public override int Id { get; set; } = 100;
        protected override Vector2 WeaponPositionRight { get; set; }
        protected override Vector2 WeaponPositionLeft { get; set; }

        public override void Fire()
        {
            if (fireCooldownTimer <= 0)
            {
                this.BulletSpeed = BulletSpeed;
                var currentMouseState = Mouse.GetState();
                var mouseInWorld = Game1.Instance.map._camera.ScreenToWorld(currentMouseState.X, currentMouseState.Y);
                GameObject NewgameObject = new GameObject();
                NewgameObject.GameObjectId = gameObject.GameObjectId;

                var bullet = NewgameObject.AddComponent<Arrow>();
                bullet.Initialize(gameObject.Transform.Position, mouseInWorld, BulletSpeed, Damage, LifeTime);
                Core.Instance.GameObjectSystem.AddGameObject(NewgameObject);

                weaponState = WeaponState.Attacking;
                fireCooldownTimer = FireCooldownDuration;

            }

        }
        public override void Awake()
        {
            base.Awake();

            var textrue = Render2D.LoadTexture("Weapon/Full Sheet", 16, 16);
            Render2D.AddAnimation("Attacking", new Animation(textrue, 105, 107, 0.5f));
            Render2D.AddAnimation("Idle", new Animation(textrue, 106, 106, 100));
            Render2D.Origin = new Vector2(6, 10);
            Collider.ShowCollider = false;
        }



        public override void Update(GameTime gameTime)
        {
            WeaponPositionRight = gameObject.Transform.Position + new Vector2(6, -2);
            WeaponPositionLeft = gameObject.Transform.Position + new Vector2(-5, -2);

            base.Update(gameTime);
        }


    }
}
