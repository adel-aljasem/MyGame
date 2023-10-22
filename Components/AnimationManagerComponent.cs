using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


public class AnimationManagerComponent
{
    private Dictionary<string, Animation> animations = new Dictionary<string, Animation>();
    private Animation currentAnimation;
    
    public bool IsPlaying { get; private set; }
    
    public AnimationManagerComponent() => IsPlaying = true;
    
    public void AddAnimation(string name, Animation animation)
    {
        animations[name] = animation;
        if (currentAnimation == null) currentAnimation = animation;
    }
    
    public void PlayAnimation(string name)
    {
        if (animations.TryGetValue(name, out var animation))
        {
            currentAnimation = animation;
            IsPlaying = true;
        }
    }

    public void Pause() => IsPlaying = false;

    public void Resume() => IsPlaying = true;

    public void TogglePlayPause() => IsPlaying = !IsPlaying;
    
    public void Update(GameTime gameTime)
    {
        if (IsPlaying)
            currentAnimation?.Update(gameTime);

    }
    
    public void Draw(SpriteBatch spriteBatch , Vector2 pos, float scale)
    {
        currentAnimation?.Draw(spriteBatch, pos, scale);
    }
}
