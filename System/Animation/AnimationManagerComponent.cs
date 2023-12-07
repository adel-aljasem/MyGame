using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class AnimationManagerComponent
{
    private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
    private Animation currentAnimation;
    private int FrameWidth;
    private int FrameHeight;
    public bool IsPlaying { get; private set; }
    
    public AnimationManagerComponent() => IsPlaying = true;
    
  
    public void AddAnimation(string name, Animation animation)
    {
        animation.FrameWidth = FrameWidth;
        animation.FrameHeight = FrameHeight;
        animations[name] = animation;
        if (currentAnimation == null) currentAnimation = animation;
    }
    public void SetFrameSizeAnimation(int width, int height)
    {
        
            FrameHeight = height;
            FrameWidth = width;    
        
    }

    public void PlayAnimation(string name)
    {
        if (animations.TryGetValue(name, out var animation))
        {
            currentAnimation = animation;
            IsPlaying = true;
        }
    }

    public Rectangle GetCurrentAnimationFirstFrame()
    {
        if (currentAnimation != null)
        {
            return currentAnimation.GetFirstFrame();
        }

        return Rectangle.Empty; // Return an empty rectangle if there's no current animation
    }
    public void Pause() => IsPlaying = false;

    public void Resume() => IsPlaying = true;

    public void TogglePlayPause() => IsPlaying = !IsPlaying;
    
    public void Update(GameTime gameTime)
    {
        if (IsPlaying)
            currentAnimation?.Update(gameTime);

    }
    
    public void Draw(SpriteBatch spriteBatch , Vector2 pos,Vector2 Origin,float rotation,float layer , SpriteEffects spriteEffects)
    {
        currentAnimation?.Draw(spriteBatch, pos, Origin,rotation, layer,spriteEffects);
    }
}
