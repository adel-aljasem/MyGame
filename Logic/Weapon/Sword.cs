using AdilGame.Interfaces;
using AdilGame.Network.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Weapons
{
    public class Sword : Weapon
    {
        protected override Vector2 WeaponPositionRight { get; set; }
        protected override Vector2 WeaponPositionLeft { get; set; }
        private float rotationDuration = 0.2f; // Duration of the rotation in seconds
        private float rotationTimer;
        private bool isRotating = false;

        public override void Fire()
        {
            var currentMouseState = Mouse.GetState();
            var status = ObjectToHit.GetComponentByInterface<IStatus>();
            StartRotation();

            if (status != null && fireCooldownTimer <= 0)
            {
                status.DmgTaken(6);
                fireCooldownTimer = FireCooldownDuration;
                Console.WriteLine("damged");
                ObjectToHit = new GameObject();

            }
        }

        public override void Hit(GameObject gameObject)
        {
            ObjectToHit = gameObject;
    
        }
        private void StartRotation()
        {
            // Set the rotation timer and flag
            rotationTimer = rotationDuration;
            isRotating = true;
        }
        private void HandleRotation(GameTime gameTime)
        {
            if (isRotating)
            {
                float elapsed = (float)gameTime.ElapsedGameTime.TotalSeconds;
                rotationTimer -= elapsed;

                if (rotationTimer <= 0)
                {
                    // Reset rotation and flag
                    Render2D.Rotation=0;
                    isRotating = false;
                }
                else
                {
                    // Calculate the current rotation angle
                    float rotationAngle = MathHelper.Lerp(0, MathHelper.PiOver2, 1 - (rotationTimer / rotationDuration));
                    Render2D.Rotation = rotationAngle;
                }
            }
        }


        internal override void Awake()
        {
            base.Awake();
            WeaponTypeenum = WeaponTypeEnum.sword;
            var textrue = Render2D.LoadTexture("Weapon/Full Sheet", 16, 16);
            Render2D.DrawSprite(2);
            Render2D.Origin = new Vector2(6, 10);
            Collider.ShowCollider = true;

        }

        internal override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Collider.Radius = 15;

            WeaponPositionRight = gameObject.Transform.Position + new Vector2(6, -2);
            WeaponPositionLeft = gameObject.Transform.Position + new Vector2(-7, -2);
            HandleRotation(gameTime);

        }
    }
}
