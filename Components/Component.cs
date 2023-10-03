using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Component 
{
    public GameObject Gameobject { get; internal set; }
    public bool IsEnabled { get; private set; } = true;

    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
    
    public void Enable() => IsEnabled = true;
    public void Disable() => IsEnabled = false;
    public void ToggleEnable() => IsEnabled = !IsEnabled;

}
