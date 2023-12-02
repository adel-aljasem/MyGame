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

        public Texture2D LoadTexture(string path)
        {
            Texture = Game1.Instance.Content.Load<Texture2D>(path);
            Origin = new Vector2( SourceRectangle.Width / 2, SourceRectangle.Height / 2);

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


        public override void Awake()
        {
        }



        public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (Texture != null && !isThereAnimation)
            {
                spriteBatch.Draw(Texture, Position, SourceRectangle, Color, Rotation, Origin, Scale, spriteEffect, LayerDepth);
            }
            else
            {
                AnimationManagerComponent.Draw(spriteBatch,Position,LayerDepth,spriteEffect);
            }
        }


        public override void Update(GameTime gameTime)
        {
            AnimationManagerComponent.Update(gameTime);

        }
    }
}
