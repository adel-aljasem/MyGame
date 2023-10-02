using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public abstract class GameObject
{
    public string Name { get; set; }
    public string Tag { get; set; }
    public Transform Transform { get; set; } // Assuming you have a Transform class to handle position, rotation, and scale.
    
    public GameObject()
    {
        Transform = new Transform(); // Initialize Transform or any other common properties here.
    }
    
    public virtual void Update(GameTime gameTime) { }
    public virtual void Draw(SpriteBatch spriteBatch) { }
}