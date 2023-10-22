using System.Collections.Generic;
using AdilGame;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace AdilGame.content;
public class ContentManager
{
    private Dictionary<string, Texture2D> _textures = new Dictionary<string, Texture2D>();
    private Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();
    public AnimationManagerComponent AnimationManagerComponent { get; set; } = new AnimationManagerComponent();
    private int _totalFrameCount = 0;  // Initial frame count

    public static ContentManager instance { get; set; } = new ContentManager();

    public Texture2D LoadTexture(string name, string path)
    {
        if (_textures.TryGetValue(name, out Texture2D texture))
        {
            return texture;
        }

        texture = Game1.Instance.Content.Load<Texture2D>(path);
        _textures.Add(name, texture);
        return texture;
    }

    public Animation LoadAnimation(string name, string path, int framecount, float frameDuration)
    {
        if (_animations.TryGetValue(name, out Animation animation))
        {
            return animation;
        }

        int frameStart = _totalFrameCount;
        int frameEnd = _totalFrameCount + framecount - 1;

        // Update the total frame count for next time
        _totalFrameCount += 5;  // Increment by 5 for each new animation

        var texture = LoadTexture(name, path);
        animation = new Animation(texture,frameStart,frameEnd ,frameDuration);
        _animations.Add(name, animation);
        AnimationManagerComponent.AddAnimation(name, animation);
        return animation;
    }

  
}