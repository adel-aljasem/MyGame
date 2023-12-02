using AdilGame.Components;
using AdilGame.Interfaces;
using AdilGame.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic
{
    public class TileMapObjectHolderComponent : Component , IStatus
    {
        public TiledMapTileObject TiledMapObject { get; set; }
        public Render2D render2D { get; set; }
        public int Health { get; set; }
        public CharcaterStatu State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        Rectangle rectangle;

        private float rotationAngle = 0f;
        private const float rotationSpeed = 0.05f; // Adjust this for faster/slower rotation
        private bool isRotating = false;
        private const float maxRotation = MathHelper.PiOver4; // 45 degrees, adjust as needed
        private bool rotateToLeft = true;



        public override void Awake()
        {
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
            render2D.LoadTexture("map/ob_0");

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

        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            base.Draw(spriteBatch, gameTime);
        }

        public void DmgTaken(int dmg)
        {
            Health -= dmg; // Reduce health
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
