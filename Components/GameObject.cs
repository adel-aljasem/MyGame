using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using AdilGame.Interfaces;
using AdilGame.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.MML;

public sealed class GameObject
{
    public Guid GameObjectId { get; set; }
    public string GameObjectName { get; set; }
    public string Tag { get; set; }
    public Transform Transform { get; set; } // Assuming you have a Transform class to handle position, rotation, and scale.

    public GameObject()
    {
        Transform = new Transform();
        GameObjectId = Guid.NewGuid();
    }
    private List<Component> _components { get; set; } = new List<Component>();

    public void AwakeComponents()
    {

    }

  
    //public List<Component> components { get { return _components; } private set{} }

    //public GameObject ADDg<T>(T component) where T : Component 
    //{
    //    _components.Add(component);
    //    return this;
    //}

    //public T AddComponent<T>() where T : Component, new()
    //{
    //    var component = new T();
    //    component.gameObject = this; // Set the gameObject property
    //    component.Awake();
    //    component.OnAddComponentInvoke(component);
    //    _components.Add(component);
    //    return component;
    //}

    public T AddComponent<T>() where T : Component, new()
    {
        var component = new T();
        component.gameObject = this; // Set the gameObject property
        component.gameobjectId = GameObjectId;
        component.Awake();
        _components.Add(component);
        return component;
    }





    public T AddComponent<T>(T component) where T : Component
    {
        _components.Add(component);
        return component;
    }

    //public T GetComponent<T>() where T : Component 
    //{
    //    return _components.OfType<T>().FirstOrDefault();
    //}

    public T GetComponent<T>() where T : Component 
    {
        return _components.OfType<T>().Where(w=>w.gameobjectId == GameObjectId).FirstOrDefault();
    }

    public T GetComponentByInterface<T>() where T : class
    {
        foreach (Component component in GetComponents<Component>())
        {
            if (component is T)
                return component as T;
        }

        return null;

    }

    public T GetComponent<T>(T com) where T : Component
    {
        return _components.OfType<T>().Where(w => w.gameobjectId == GameObjectId).FirstOrDefault();
    }

    public List<T> GetComponents<T>() where T : Component
    {
        return _components.OfType<T>().ToList();
    }


    public void Update(GameTime gameTime)
    {
        foreach (var component in _components)
        {
            component.Update(gameTime);
        }
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var component in _components)
        {
            component.Draw(spriteBatch, gameTime);
        }
    }





}