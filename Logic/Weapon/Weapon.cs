using AdilGame.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdilGame.Logic.Weapon.bullet;
using Cyotek.Drawing.BitmapFont;
using AdilGame.System;

namespace AdilGame.Logic.Weapon
{
    public abstract class Weapon : Component
    {
        public abstract int Id { get; set; }
        public string IdPlayer { get; set; }
        public int Level { get; set; }
        public int PlusHealth { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; } = 10;
        public int BulletSpeed { get; set; } = 5;
        public int LifeTime { get; set; } = 100;
        public Render2D Render2D { get; set; }
        public ColliderComponent Collider { get; set; }
        public float FireCooldownDuration { get; set; } = 0.00001f; // Duration of the cooldown in seconds
        protected float fireCooldownTimer = 0; // Current cooldown timer
        protected abstract Vector2 WeaponPositionRight { get; set; }
        protected abstract Vector2 WeaponPositionLeft { get; set; }
        public Dictionary<WeaponStatusTypeEnum, int> AdditionalStatuses { get; set; }
        public WeaponState weaponState { get; set; }
        protected GameObject ObjectToHit { get; set; } = new GameObject();
        public abstract void Fire();
        
        public virtual void Hit(GameObject gameObject)
        {

        }

        private void AddRandomStatuses()
        {
            Random random = new Random();
            var statusValues = Enum.GetValues(typeof(WeaponStatusTypeEnum));
            while (AdditionalStatuses.Count < 2)
            {
                var randomStatus = (WeaponStatusTypeEnum)statusValues.GetValue(random.Next(statusValues.Length));
                if (!AdditionalStatuses.ContainsKey(randomStatus))
                {
                    AdditionalStatuses.Add(randomStatus, random.Next(1, 10)); // Random value for status
                }
            }
        }

        public void WeaponLevel()
        {

        }


        private void FlipWeaponBasedOnMouse()
        {
            var currentMouseState = Mouse.GetState();
            var mouseInWorld = Game1.Instance.map._camera.ScreenToWorld(currentMouseState.X, currentMouseState.Y);

            bool shouldFaceLeft = mouseInWorld.X < gameObject.Transform.Position.X;
            if (shouldFaceLeft)
            {
                Render2D.Position = WeaponPositionLeft;
                Collider.Center = WeaponPositionLeft;
            }
            else
            {
                Render2D.Position = WeaponPositionRight;
                Collider.Center = WeaponPositionRight + new Vector2(5, 0);
            }

        }
        private void UpdateWeaponRotation()
        {
            // Get the current mouse state and weapon's position
            var currentMouseState = Mouse.GetState();
            var mouseInWorld = Game1.Instance.map._camera.ScreenToWorld(currentMouseState.X, currentMouseState.Y);

            Vector2 weaponPosition = new Vector2(Render2D.Position.X, Render2D.Position.Y); // Assuming Render2D has a Position property

            // Calculate the direction vector
            Vector2 direction = mouseInWorld - weaponPosition;

            // Calculate the angle in radians
            float angle = (float)Math.Atan2(direction.Y, direction.X);
            // Adjust the angle based on the sprite's default orientation
            // Assuming the sprite is oriented at a 45-degree angle (π/4 radians) from the right
            float offsetAngle = (float)(Math.PI / 4);
            angle += offsetAngle;

            // Set the rotation of the Render2D component
            Render2D.Rotation = angle;
        }

        public override void Awake()
        {
            AdditionalStatuses = new Dictionary<WeaponStatusTypeEnum, int>();
            AddRandomStatuses();
            Render2D = gameObject.AddComponent<Render2D>();
            Collider = gameObject.AddComponent<ColliderComponent>();
            Collider.TileMapOptomaiztion = false;
            Collider.ColliderPointAtBottom = false;
            Collider.OnCollision += Hit;
            base.Awake();
        }


        public override void Update(GameTime gameTime)
        {
            if (fireCooldownTimer > 0)
            {
                fireCooldownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            FlipWeaponBasedOnMouse();
            var animation = weaponState switch
            {
                WeaponState.Attacking => "Attacking",
                WeaponState.Dropped => "Dropped",
                _ => "Idle"
            };
            Render2D.PlayAnimation(animation);
            Render2D.Update(gameTime);

            UpdateWeaponRotation();

            base.Update(gameTime);
        }

    

    }

}
