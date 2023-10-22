using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Animation
{
    public Texture2D SpriteSheet { get; set; }
    public int StartFrame { get; set; }
    public int EndFrame { get; set; }
    public int CurrentFrame { get; set; }
    public float FrameTime { get; set; }
    public float Timer { get; set; }
    public int FrameWidth { get; } = 16; // width of a frame
    public int FrameHeight { get; } = 16; // height of a frame

    public Animation(Texture2D spriteSheet, int startFrame, int endFrame, float frameTime)
    {
        SpriteSheet = spriteSheet;
        StartFrame = startFrame;
        EndFrame = endFrame;
        CurrentFrame = StartFrame;
        FrameTime = frameTime;
    }

    public void Update(GameTime gameTime)
    {
        Timer += (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (Timer > FrameTime)
        {
            Timer = 0f;
            CurrentFrame = (CurrentFrame + 1 - StartFrame) % (EndFrame - StartFrame + 1) + StartFrame;
        }
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, float scale)
    {
        int row = CurrentFrame / (SpriteSheet.Width / FrameWidth);
        int column = CurrentFrame % (SpriteSheet.Width / FrameWidth);

        Rectangle sourceRectangle = new Rectangle(column * FrameWidth, row * FrameHeight, FrameWidth, FrameHeight);
        Vector2 destinationPosition = position * scale;
        Vector2 origin = new Vector2(0, 0); // Adjust if needed
        spriteBatch.Draw(SpriteSheet, destinationPosition, sourceRectangle, Color.White, 0f, origin, scale, SpriteEffects.None, 0f);
    }
}
