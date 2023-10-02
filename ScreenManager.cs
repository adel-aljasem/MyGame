using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

public class ScreenManager
{
    GraphicsDeviceManager graphics;
    int baseWidth;
    int baseHeight;

     int frameRate = 0;
    int frameCounter = 0;
    TimeSpan elapsedTime = TimeSpan.Zero;
    SpriteFont font;

    public ScreenManager(GraphicsDeviceManager graphics, int baseWidth, int baseHeight)
    {
        this.graphics = graphics;
        this.baseWidth = baseWidth;
        this.baseHeight = baseHeight;
        SetWindowSize(baseWidth,baseHeight);
    }

    private void SetWindowSize(int width, int height)
    {
        graphics.PreferredBackBufferWidth = width;
        graphics.PreferredBackBufferHeight = height;
        graphics.ApplyChanges();
    }

    public Matrix GetScaleMatrix()
    {
        float scaleX = (float)graphics.GraphicsDevice.Viewport.Width / baseWidth;
        float scaleY = (float)graphics.GraphicsDevice.Viewport.Height / baseHeight;
        return Matrix.CreateScale(scaleX, scaleY, 1f);
    }

    public void LoadContent(ContentManager content)
    {
        font = content.Load<SpriteFont>("Content/Fonts/File"); //Assuming you have a SpriteFont named "myFont" added in your content.

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
        spriteBatch.DrawString(font, fps, new Vector2(33, 33), Color.Black);
        spriteBatch.DrawString(font, fps, new Vector2(32, 32), Color.White);
    }

}
