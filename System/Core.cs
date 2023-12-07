using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using AdilGame.System;
using System.Threading.Tasks;

public class Core
{
    public static Core Instance { get; } = new Core();
    public GameObjectSystem GameObjectSystem { get; } = new GameObjectSystem();
    public CollisionSystem CollisionSystem { get; } = new CollisionSystem();
    public RenderSystem RenderSystem { get; } = new RenderSystem();
    public NetworkSystem NetworkSystem { get; }
    private Task updateTask = Task.CompletedTask;

    private float targetPhysicsUpdateTime = 1f / 30f;
    private float accumulator = 0f;

    public Core()
    {
        NetworkSystem = new NetworkSystem("http://192.168.1.25:5000/gameHub");
    }

    public void Update(GameTime gameTime)
    {
        Secound30FpsSetting(gameTime);

        GameObjectSystem.UpdateGameObjects(gameTime);
        if (updateTask.IsCompleted)
        {
            updateTask = Task.Run(() => CollisionSystem.CheckCollisions(GameObjectSystem.GetAllGameObjects()));

        }

    }

    private void Secound30FpsSetting(GameTime gameTime)
    {
        accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
        while (accumulator >= targetPhysicsUpdateTime)
        {
            UpdatePhysics(targetPhysicsUpdateTime);
            accumulator -= targetPhysicsUpdateTime;
        }
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        RenderSystem.Draw(spriteBatch, GameObjectSystem.GetAllGameObjects(), gameTime);
    }

    private void UpdatePhysics(float deltaTime)
    {

    }
}
