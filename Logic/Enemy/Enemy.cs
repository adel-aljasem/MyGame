using AdilGame.Interfaces;
using AdilGame.Logic.Status;
using Microsoft.Xna.Framework;
using PandaGameLibrary.Components;
using System;

namespace AdilGame.Logic.Enemy
{
    public abstract class Enemy : Component , IDamageable
    {
        public int Id { get; set; }
        public int RankLevel { get; set; }
        public float DetectionRange { get; set; }
        public ColliderComponent colliderRange { get; set; }
        public ColliderComponent colliderEnemy { get; set; }
        public Render2D Render2d { get; set; }
        private Vector2 previousPosition;

        public EnemyState State { get; set; }
        public AIComponent AI { get; set; }
        CharcaterStatu IDamageable.State { get ; set ; }
        public IMainStatus MainStatus { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void OnEnterRange(GameObject gameObject)
        {

        }

        public void OnCollied(GameObject gameObject) 
        {
        
        }

        public override void Awake()
        {
            Render2d = gameObject.AddComponent<Render2D>();
            colliderRange.OnCollision += OnEnterRange;
            colliderEnemy.OnCollision += OnCollied;
        }

        public void FlipCharacterBasedOnMovingPosition()
        {
            var currentPosition = gameObject.Transform.Position;

            // Check if there's significant movement in the X-axis
            if (Math.Abs(currentPosition.X - previousPosition.X) > float.Epsilon)
            {
                bool shouldFaceLeft = currentPosition.X < previousPosition.X;
                gameObject.GetComponent<Render2D>()?.FlipAnimation(shouldFaceLeft);
            }
        }


        public void DmgTaken(int dmg)
        {
        }

        public void HealthIncress(int healthAmount)
        {
        }

        public override void Update(GameTime gameTime)
        {
            previousPosition = gameObject.Transform.Position;
            FlipCharacterBasedOnMovingPosition();
            base.Update(gameTime);
        }

   

    }
}
