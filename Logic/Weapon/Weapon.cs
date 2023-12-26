using AdilGame.Logic.inventory.Items;
using AdilGame.Logic.Status;
using AdilGame.Network.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PandaGameLibrary.Components;
using System;
using System.Collections.Generic;

namespace AdilGame.Logic.Weapons
{
    public abstract class Weapon : Component , Iitem 
    {
        public int Id { get; set; }
        public WeaponTypeEnum WeaponTypeenum { get;  set; }
        public bool IsDropped { get; set; }
        public string PlayerId { get; set; }
        public string Description { get; set; }
        public bool CanBeDropped { get; set; } = true;
        public int Tier { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; } = 5;
        public int BulletSpeed { get; set; } = 5;
        public int LifeTime { get; set; } = 100;
        public Render2D Render2D { get; set; }
        public ColliderComponent Collider { get; set; }
        public float FireCooldownDuration { get; set; } = 0.00001f; // Duration of the cooldown in seconds
        protected float fireCooldownTimer = 0; // Current cooldown timer
        protected abstract Vector2 WeaponPositionRight { get; set; }
        protected abstract Vector2 WeaponPositionLeft { get; set; }
        public Dictionary<WeaponStatusTypeEnum, int> AdditionalStatuses { get; set; }
        public Dictionary<WeaponStatusTypeEnum, int> MainStatus { get; set; }

        public WeaponState weaponState { get; set; }
        protected GameObject ObjectToHit { get; set; } = new GameObject();
        public abstract void Fire(Vector2 mousePostion);
        
        public virtual void Hit(GameObject gameObject)
        {
            if(IsDropped) return;
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


        public void FlipWeaponBasedOnMouse(bool shouldfaceleft)
        {
            if (IsDropped) return;
            //var currentMouseState = Mouse.GetState();
            //var mouseInWorld = Game1.Instance.map._camera.ScreenToWorld(currentMouseState.X, currentMouseState.Y);

            //bool shouldFaceLeft = mouseInWorld.X < gameObject.Transform.Position.X;
            if (shouldfaceleft)
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
        public void UpdateWeaponRotation(float mouseX,float mouseY)
        {
            // Get the current mouse state and weapon's position
            var currentMouseState = Mouse.GetState();
            var mouseInWorld = new Vector2(mouseX,mouseY);
            
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
            Guid guid = Guid.NewGuid();
            Id = guid.GetHashCode();
            AdditionalStatuses = new Dictionary<WeaponStatusTypeEnum, int>();
            AddRandomStatuses();
            Render2D = gameObject.AddComponent<Render2D>();
            Collider = gameObject.AddComponent<ColliderComponent>();
            Collider.TileMapOptomaiztion = false;
            Collider.ShowCollider = false;
            Collider.ColliderPointAtBottom = false;
            Collider.IsDynamic = true;
            Collider.OnCollision += Hit;
            base.Awake();
        }


        public override void Update(GameTime gameTime)
        {
            if (fireCooldownTimer > 0)
            {
                fireCooldownTimer -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            var animation = weaponState switch
            {
                WeaponState.Attacking => "Attacking",
                WeaponState.Dropped => "Dropped",
                _ => "Idle"
            };
            Render2D.PlayAnimation(animation);
            base.Update(gameTime);
            if (IsDropped)
            {
                Render2D.Position = gameObject.Transform.Position;
            }
        }

    

    }

}
