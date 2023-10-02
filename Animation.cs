using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Animation
{
    public Texture2D SpriteSheet { get; set; }
    public int FrameCount { get; set; }
    public int CurrentFrame { get; set; } = 0;
    public float FrameTime { get; set; }
    public float Timer { get; set; }
    public int FrameWidth { get; } = 16; // width of a frame
    public int FrameHeight { get; } = 16; // height of a frame

    public Animation(Texture2D spriteSheet, int frameCount, float frameTime)
    {
        SpriteSheet = spriteSheet;
        FrameCount = frameCount;
        FrameTime = frameTime;
    }

    public void Update(GameTime gameTime)
    {
        Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (Timer > FrameTime)
        {
            Timer = 0f;
            CurrentFrame = (CurrentFrame + 1) % FrameCount;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale)
    {
        int row = CurrentFrame / (SpriteSheet.Width / FrameWidth);
        int column = CurrentFrame % (SpriteSheet.Width / FrameWidth);

        Rectangle sourceRectangle = new Rectangle(column * FrameWidth, row * FrameHeight, FrameWidth, FrameHeight);
        Vector2 destinationPosition = position * scale;
        Vector2 origin = new Vector2(0,0); // Adjust if needed
        spriteBatch.Draw(SpriteSheet, destinationPosition, sourceRectangle, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
    }
}