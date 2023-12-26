using AdilGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using PandaGameLibrary.System;
using System;

public class ScreenManager
{
    SpriteBatch spriteBatch1;

    GraphicsDeviceManager graphics;
    int baseWidth;
    int baseHeight;

     int frameRate = 0;
    int frameCounter = 0;
    TimeSpan elapsedTime = TimeSpan.Zero;
    SpriteFont font;
    public float ScaleX => (float)graphics.GraphicsDevice.Viewport.Width / baseWidth;
    public float ScaleY => (float)graphics.GraphicsDevice.Viewport.Height / baseHeight;

    public ScreenManager(GraphicsDeviceManager graphics, int baseWidth, int baseHeight)
    {
        this.graphics = graphics;
        this.baseWidth = baseWidth;
        this.baseHeight = baseHeight;
        SetWindowSize(baseWidth,baseHeight);

    }

    public void SetWindowSize(int width, int height)
    {
        graphics.PreferredBackBufferWidth = width;
        graphics.PreferredBackBufferHeight = height;
        graphics.ApplyChanges();
    }

    public Matrix GetScaleMatrix(float multiplayX , float multiplayY)
    {
        float scaleX = (float)graphics.GraphicsDevice.Viewport.Width / baseWidth;
        float scaleY = (float)graphics.GraphicsDevice.Viewport.Height / baseHeight;
        return Matrix.CreateScale(scaleX * multiplayX, scaleY * multiplayY, 1f);
    }

    public void LoadContent(ContentManager content)
    {
        font = content.Load<SpriteFont>("Fonts/File"); //Assuming you have a SpriteFont named "myFont" added in your content.
        spriteBatch1 = new SpriteBatch(Game1.Instance.GraphicsDevice);
    }

    public void Update(GameTime gameTime)
    {
        // FPS counter logic
        elapsedTime += gameTime.ElapsedGameTime;
        if (elapsedTime > TimeSpan.FromSeconds(1))
        {
            elapsedTime -= TimeSpan.FromSeconds(1);
            frameRate = frameCounter;
            frameCounter = 0;
        }
    }

    public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
    {

        frameCounter++;
        string fps = string.Format("FPS: {0}", frameRate);
        spriteBatch1.Begin(samplerState: SamplerState.PointWrap,transformMatrix:Game1.Instance.screenManager.GetScaleMatrix(2,2));
        spriteBatch1.DrawString(font, "Alpha 0.0.1", new Vector2(32, 15), Color.White);
        spriteBatch1.DrawString(font, fps, new Vector2(32, 32), Color.White);
        spriteBatch1.DrawString(font, $"Ping: {Core.Instance.NetworkSystem.Ping}", new Vector2(32, 48), Color.Yellow);
        spriteBatch1.End();
    }

}
