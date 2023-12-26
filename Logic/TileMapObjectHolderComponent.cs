using AdilGame.Interfaces;
using AdilGame.Logic.Status;
using Microsoft.Xna.Framework;
using MonoGame.Extended.Tiled;
using PandaGameLibrary.Components;
using System;

namespace AdilGame.Logic
{
    public class TileMapObjectHolderComponent : Component, IDamageable
    {
        public int RankLevel { get; set; } = 3;
        public TiledMapTileObject TiledMapObject { get; set; }
        public Render2D render2D { get; set; }
        public CharcaterStatu State { get; set ; }
        public int Speed { get; set; }
        public IMainStatus MainStatus { get ; set ; }

        Rectangle rectangle;

        private float rotationAngle = 0f;
        private const float rotationSpeed = 0.05f; // Adjust this for faster/slower rotation
        private bool isRotating = false;
        private const float maxRotation = MathHelper.PiOver4; // 45 degrees, adjust as needed
        private bool rotateToLeft = true;



        public override void Awake()
        {
            StatusSystem.instance.ApplyStatusBasedOnRank(this);
            render2D = gameObject.AddComponent<Render2D>();

        }

        public void LoadData()
        {
            rectangle = new Rectangle(
        Convert.ToInt32(TiledMapObject.Tile.Properties["ImageRectX"]),
        Convert.ToInt32(TiledMapObject.Tile.Properties["ImageRectY"]),
        64, 64 // Assuming all tiles are 64x64
    );

            render2D.Position = TiledMapObject.Position;
            render2D.SourceRectangle = rectangle;
            render2D.Origin = new Vector2(render2D.SourceRectangle.Width / 2, render2D.SourceRectangle.Height / 2);
            render2D.LoadTexture("map/ob_0", 64, 64);

        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            if (isRotating)
            {
                // Update rotation angle
                float rotationChange = rotateToLeft ? rotationSpeed : -rotationSpeed;
                rotationAngle += rotationChange;

                // Apply rotation to Render2D component
                render2D.Rotation = rotationAngle;

                // Check if rotation has reached its limit and reverse or stop it
                if (Math.Abs(rotationAngle) >= maxRotation)
                {
                    rotateToLeft = !rotateToLeft;
                    rotationAngle = maxRotation * (rotateToLeft ? 1 : -1);
                    isRotating = false; // Stop rotation after reaching the limit
                }
            }
        }


        public void DmgTaken(int dmg)
        {
            MainStatus.Health -= dmg; // Reduce health
                           // Start rotation effect
            if (!isRotating)
            {
                isRotating = true;
                rotateToLeft = !rotateToLeft; // Alternate rotation direction
            }
        }

        public void HealthIncress(int healthAmount)
        {
            throw new NotImplementedException();
        }
    }
}
