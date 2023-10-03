using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class GameObject : Component
{
    public string Name { get; set; }
    public string Tag { get; set; }
    public Transform Transform { get; set; } // Assuming you have a Transform class to handle position, rotation, and scale.
    
    public GameObject()
    {
        Transform = new Transform(); // Initialize Transform or any other common properties here.
    }
    
private List<Component> _components = new List<Component>();
    
    public T AddComponent<T>() where T : Component, new()
    {
        var component = new T { Gameobject = this };
        _components.Add(component);
        return component;
    }

    public T GetComponent<T>() where T : Component
    {
        return _components.OfType<T>().FirstOrDefault();
    }

     public override void Update(GameTime gameTime)
    {
        foreach (var component in _components.Where(c => c.IsEnabled))
            component.Update(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        foreach (var component in _components.Where(c => c.IsEnabled))
            component.Draw(spriteBatch);
    }


}