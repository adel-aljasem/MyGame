using AdilGame.Logic.Weapons.bullet;
using AdilGame.Network.Data;
using AdilGame.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Weapons
{
    public class Bow : Weapon
    {
        protected override Vector2 WeaponPositionRight { get; set; }
        protected override Vector2 WeaponPositionLeft { get; set; }

        public override void Fire(Vector2 mousePosition)
        {
            if (fireCooldownTimer <= 0)
            {
                this.BulletSpeed = BulletSpeed;
                GameObject NewgameObject = new GameObject();
                NewgameObject.GameObjectId = gameObject.GameObjectId;

                var bullet = NewgameObject.AddComponent<Arrow>();
                bullet.Initialize(gameObject.Transform.Position, mousePosition, BulletSpeed, Damage, LifeTime);
                Core.Instance.GameObjectSystem.AddGameObject(NewgameObject);

                weaponState = WeaponState.Attacking;
                fireCooldownTimer = FireCooldownDuration;

            }

        }
        internal override void Awake()
        {
            base.Awake();
            Name = "Bow";
            WeaponTypeenum = WeaponTypeEnum.bow;
            var textrue = Render2D.LoadTexture("Weapon/Full Sheet", 16, 16);
            ItemTexture = Render2D.DrawSprite(106);
            Render2D.AddAnimation("Attacking", new Animation(textrue, 105, 107, 0.5f));
            Render2D.AddAnimation("Idle", new Animation(textrue, 106, 106, 100));
            Render2D.Origin = new Vector2(6, 10);
        }



        internal override void Update(GameTime gameTime)
        {
            WeaponPositionRight = gameObject.Transform.Position + new Vector2(6, -2);
            WeaponPositionLeft = gameObject.Transform.Position + new Vector2(-5, -2);
            base.Update(gameTime);

        }


    }
}
