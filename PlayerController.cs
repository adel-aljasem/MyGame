using Microsoft.Xna.Framework;

public class PlayerController : GameObject 
{
        public float Speed { get; set; }
    private AnimationComponent AnimationComponent { get; set; }
    public EntityState State { get; private set; }

    public PlayerController(float speed)
    {
        Speed = speed;
        AnimationComponent = new AnimationComponent();
        State = EntityState.Idle;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime); // Call base to update common GameObject logic.
        
        HandleInput(gameTime); // Handle Input and move the player.

        var animation = State switch
        {
            EntityState.Walk => "Walk",
            EntityState.Attack => "Attack",
            _ => "Idle",
        };
        AnimationComponent.PlayAnimation(animation); // Play the correct animation.
    }

    public void HandleInput(GameTime gameTime)
    {
        // Handle inputs, update player’s state, and move the player here
        // Example:
        // var keyboardState = Keyboard.GetState();
        // if (keyboardState.IsKeyDown(Keys.W))
        // {
        //     this.Transform.Position.Y -= Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        //     State = EntityState.Moving;
        // }
        // ...Handle other keys and states...
    }

}