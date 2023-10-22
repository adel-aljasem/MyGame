using System.ComponentModel;
using AdilGame.content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

public class PlayerController : Component 
{
    public float Speed { get; set; }= 100f;
    public EntityState State { get; private set; }

    public PlayerController()
    {
        ContentManager.instance.LoadTexture("Arthax", "Content/Characters/Champions/Arthax");
        var walkDown = ContentManager.instance.LoadAnimation("Walkdown", "Content/Characters/Champions/Arthax", 5, 0.5f);
        var walkUp = ContentManager.instance.LoadAnimation("WalkUp", "Content/Characters/Champions/Arthax", 5, 0.5f);
        var walkRight = ContentManager.instance.LoadAnimation("WalkRight", "Content/Characters/Champions/Arthax", 5, 0.5f);
        var walkLeft = ContentManager.instance.LoadAnimation("WalkLeft", "Content/Characters/Champions/Arthax", 5, 0.5f);
        ContentManager.instance.LoadAnimation("Idle", "Content/Characters/Champions/Arthax", 1, 0.5f);
        State = EntityState.Idle;
    }

    public void Update(GameTime gameTime)
    {
        
        HandleInput(gameTime); // Handle Input and move the player.

        var animation = State switch
        {
            EntityState.Walkdown => "Walkdown",
            EntityState.WalkUp => "WalkUp",
            EntityState.WalkRight => "WalkRight",
            EntityState.WalkLeft => "WalkLeft",
            EntityState.Attack => "Attack",
            _=> "Idle"
        };
        ContentManager.instance.AnimationManagerComponent.PlayAnimation(animation); // Play the correct animation.
        ContentManager.instance.AnimationManagerComponent.Update(gameTime); // Update the animation.
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        ContentManager.instance.AnimationManagerComponent.Draw(spriteBatch,Gameobject.Transform.Position,2f);
    }


    public void HandleInput(GameTime gameTime)
    {
        var keyboardState = Keyboard.GetState();
        var direction = Vector2.Zero;

        if (keyboardState.IsKeyDown(Keys.W))
        {
            direction.Y -= 1;
            State = EntityState.WalkUp;
        }
        if (keyboardState.IsKeyDown(Keys.S))
        {
            direction.Y += 1;
            State = EntityState.Walkdown;
        }
        if (keyboardState.IsKeyDown(Keys.A))
        {
            direction.X -= 1;
            State = EntityState.WalkLeft;
        }
        if (keyboardState.IsKeyDown(Keys.D))
        {
            direction.X += 1;
            State = EntityState.WalkRight;
        }

        if (direction != Vector2.Zero)
        {
            direction.Normalize();
            Gameobject.Transform.Position += direction * Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
        
    }
}