using AdilGame.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Components
{
    public class Render2D : Component
    {
        private AnimationManagerComponent AnimationManagerComponent { get; } = new AnimationManagerComponent();
        private bool isThereAnimation;
        public Texture2D Texture { get; set; }
        public Vector2 Position { get; set; } 
        public Rectangle SourceRectangle { get; set; }
        public Color Color { get; set; } = Color.White;
        public float Rotation { get; set; } = 0f;
        public Vector2 Origin { get; set; } = Vector2.Zero;
        public float Scale { get; set; } = 1f;
        public float LayerDepth { get; set; } = 1f;
        private SpriteEffects spriteEffect = SpriteEffects.None;
        public Texture2D LoadTexture(string path,int width , int height)
        {
            Texture = Game1.Instance.Content.Load<Texture2D>(path);
            AnimationManagerComponent.SetFrameSizeAnimation(width, height);

            //Origin = new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);

            return Texture;
        }
       
        public void PlayAnimation(string name)
        {
            AnimationManagerComponent.PlayAnimation(name);
        }

        public void AddAnimation(string AnimationName,Animation animation)
        {
            AnimationManagerComponent.AddAnimation(AnimationName,animation);
            isThereAnimation = true;
        }
       

        public void FlipAnimation(bool shouldFaceLeft)
        {
            spriteEffect = shouldFaceLeft ? SpriteEffects.FlipHorizontally : SpriteEffects.None;

        }
        public void DrawSprite( int index)
        {
            int spriteWidth = 16;
            int spriteHeight = 16;

            // Calculate the number of columns in the texture
            int columns = Texture.Width / spriteWidth;

            // Calculate the row and column for the sprite
            int row = index / columns;
            int column = index % columns;

            // Calculate the x and y position of the sprite in the texture
            int x = column * spriteWidth;
            int y = row * spriteHeight;

            // Define the source rectangle for the sprite
            SourceRectangle = new Rectangle(x, y, spriteWidth, spriteHeight);

            //// Create a new texture for the sprite (if needed)
            //Texture2D spriteTexture = new Texture2D(Game1.Instance.GraphicsDevice, spriteWidth, spriteHeight);
            //Color[] data = new Color[spriteWidth * spriteHeight];
            //texture.GetData(0, sourceRectangle, data, 0, data.Length);
            //spriteTexture.SetData(data);

        }


        internal override void Awake()
        {
            Position = gameObject.Transform.Position;
            Origin = gameObject.Transform.Position;
            Rotation = gameObject.Transform.Rotation;  
        }



        internal override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Texture != null && !isThereAnimation)
            {
                spriteBatch.Draw(Texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, spriteEffect, LayerDepth);
            }
            else
            {
                AnimationManagerComponent.Draw(spriteBatch,Position,Origin, Rotation,LayerDepth,spriteEffect);
            }
        }


        internal override void Update(GameTime gameTime)
        {
            AnimationManagerComponent.Update(gameTime);

        }
    }
}
