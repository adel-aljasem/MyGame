using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

public class Entity : GameObject
{
    private List<Component> _components = new List<Component>();

    public T AddComponent<T>() where T : Component, new()
    {
        var component = new T { Entity = this };
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
