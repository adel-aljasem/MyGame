using AdilGame.Logic.Weapons.bullet;
using AdilGame.Network.Data;
using Microsoft.Xna.Framework;
using PandaGameLibrary.Components;
using PandaGameLibrary.System;

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
        public override void Awake()
        {
            base.Awake();
            Name = "Bow";
            WeaponTypeenum = WeaponTypeEnum.bow;
            var textrue = Render2D.LoadTexture("Weapon/Full Sheet", 16, 16);
            Render2D.AddAnimation("Attacking", new Animation(textrue, 105, 107, 0.5f));
            Render2D.AddAnimation("Idle", new Animation(textrue, 106, 106, 100));
            Render2D.Origin = new Vector2(6, 10);
            Render2D.DrawSprite(106);
        }



        public override void Update(GameTime gameTime)
        {
            WeaponPositionRight = gameObject.Transform.Position + new Vector2(6, -2);
            WeaponPositionLeft = gameObject.Transform.Position + new Vector2(-5, -2);
            base.Update(gameTime);

        }


    }
}
