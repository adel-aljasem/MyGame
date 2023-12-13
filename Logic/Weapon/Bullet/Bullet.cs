using AdilGame.Components;
using AdilGame.Interfaces;
using AdilGame.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Weapons.bullet
{
    public abstract class Bullet : Component 
    {
        public int Id { get; set; }
        public float Lifetime { get; set; }
        public int Damage { get; set; }
        public bool IsActive { get; set; } = true;
        public int Speed { get; set; }
        public ColliderComponent ColliderComponent { get; set; }
        public Render2D Render2D { get; set; }
        protected Action _childCallback;

        public abstract void OnHit(GameObject gameObject);





        internal override void Awake()
        {
            ColliderComponent = gameObject.AddComponent<ColliderComponent>();
            ColliderComponent.ShowCollider = false;
            Render2D = gameObject.AddComponent<Render2D>();
            ColliderComponent.OnCollision += OnHit;
            ColliderComponent.Radius = 5;
            base.Awake();
        }

        public void Initialize(Vector2 startPosition, Vector2 mousePosition, int speed, int damage, float lifetime)
        {
            Speed = speed;
            Damage = damage;
            Lifetime = lifetime;
            Vector2 direction = mousePosition - startPosition;
            direction.Normalize();
            gameObject.Transform.Position = startPosition;
            ColliderComponent.Velocity = direction * speed;
            Render2D.Position = gameObject.Transform.Position;
            UpdateBulletRotation();
            IsActive = true;
        }

        protected void UpdateBulletRotation()
        {
            // Get the current mouse state and weapon's position
            var currentMouseState = Mouse.GetState();
            var mouseInWorld = Game1.Instance.map._camera.ScreenToWorld(currentMouseState.X, currentMouseState.Y);
            Vector2 weaponPosition = new Vector2(Render2D.Position.X, Render2D.Position.Y); // Assuming Render2D has a Position 
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


        internal override void Update(GameTime gameTime)
        {

            if (!IsActive) return;
            float deltaTime = (float)gameTime.ElapsedGameTime.TotalSeconds;
            Render2D.Position = gameObject.Transform.Position;
            Lifetime -= deltaTime;

            if (Lifetime <= 0)
            {
                IsActive = false;
                Core.Instance.GameObjectSystem.RemoveGameObject(this.gameObject);
            }
            base.Update(gameTime);
        }

      
    }

}
