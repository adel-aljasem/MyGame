using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using AdilGame.Interfaces;
using AdilGame.System;
using AdilGame;
using MonoGame.Extended;
using AdilGame.Components;

public class ColliderComponent : Component
{
    public event Action<GameObject> OnCollision;
    public event EventHandler Disposed;
    public bool ShowCollider { get; set; } = true;
    public bool ColliderShapeCircle { get; set; } = true;
    public Rectangle Bounds { get; private set; }
    public Vector2 Center { get; private set; }
    public float Radius { get; set; } = 10;
    public bool ColliderPointAtBottom { get; set; }
    public bool LetMeDraw { get; set; }
    private float X { get; set; }
    private float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public bool IsDynamic { get; set; } = false;
    public Vector2 Velocity { get; set; }
    CircleData circle = new CircleData();


    Texture2D pixel;

    public ColliderComponent()
    {
        pixel = new Texture2D(Game1.Instance.GraphicsDevice, 1, 1);
        pixel.SetData(new[] { Color.White }); // Set the pixel to white

    }


    public void OnCollide(GameObject e)
    {
        OnCollision?.Invoke(e);
    }

    public override void Awake()
    {



    }

    public void DrawColliderFromTiled(float x, float y)
    {
        X = x;
        Y = y;
    }

    public override void Update(GameTime gameTime)
    {
        circle.SetCircleData(Center, Radius, 16);

        circle.Update();

        if (IsDynamic)
        {
            gameObject.Transform.Position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        // Assuming the scale represents the full dimensions of the object
        int width = (int)gameObject.Transform.Scale.X;  // Object's full width
        int height = (int)gameObject.Transform.Scale.Y; // Object's full height

        if (LetMeDraw)
        {
            float colliderDiameterX = Width  / 2;  // Diameter in X
            float colliderDiameterY = Height / 2 ;  // Diameter in Y
            // The top-left corner of the object
            Vector2 middleObject = gameObject.Transform.Position;
            var raudisSize = Math.Max(colliderDiameterX, colliderDiameterY);
            Radius = raudisSize;
            // Offset the center of the collider within the object
            // X and Y are assumed to be the offsets from the top-left corner of the object
            Center = new Vector2(middleObject.X - 32 + X + colliderDiameterX , middleObject.Y - 32 + Y + colliderDiameterY);


        }


        if (ColliderPointAtBottom && !LetMeDraw)
        {
            if (ColliderShapeCircle)
            {
                // If the point is at the bottom center of the object
                Center = new Vector2(
                    gameObject.Transform.Position.X + width / 2,  // X is still the center
                    gameObject.Transform.Position.Y + height      // Y is at the bottom
                );
            }
            else
            {
                // Bounds with the origin at the bottom center
                Bounds = new Rectangle(
                    (int)(Center.X - width / 2),  // X is at the center
                    (int)(Center.Y - height),     // Y is at the bottom
                    width,
                    height
                );
            }


        }
        else if (!ColliderPointAtBottom && !LetMeDraw)
        {
            if (ColliderShapeCircle)
            {
                // If the point is at the center of the object
                Center = new Vector2(
                    gameObject.Transform.Position.X + width / 2,
                    gameObject.Transform.Position.Y + height / 2
                );

            }
            else
            {
                // Bounds with the origin at the center
                Bounds = new Rectangle(
                    (int)(Center.X - width / 2),
                    (int)(Center.Y - height / 2),
                    width,
                    height
                );
            }

        }



    }




    public override void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {

        if (ShowCollider)
        {
            if (ColliderShapeCircle)
            {
                circle.Draw(spriteBatch, Color.White);
            }
            else
            {
                // Draw the rectangle representing the collider's bounds
                spriteBatch.Draw(pixel, Bounds, Color.Green * 0.5f);
            }
        }

    }

 


    public void Dispose()
    {
        throw new NotImplementedException();
    }


}
