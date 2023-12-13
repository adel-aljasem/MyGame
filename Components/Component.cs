using AdilGame.Components;
using AdilGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

public abstract class Component 
{
    public Component()
    {
        ComponentId = Guid.NewGuid();   
    }

    public  Guid ComponentId { get; set; } 
    public  Guid gameobjectId { get; set; }
    public GameObject gameObject { get; set; }
    public string Tag { get; set; }
    public bool IsEnabled { get; set; } = true;

    public void Enable() => IsEnabled = true;
    public void Disable() => IsEnabled = false;
    public void ToggleEnable() => IsEnabled = !IsEnabled;



    //public T AddComponent<T>(T component) where T : Component, new()
    //{
    //    gameObject._components.Add(component);

    //    return component;
    //}

    //public T GetComponent<T>() where T : Component
    //{
    //    return gameObject._components.OfType<T>().FirstOrDefault();
    //}

    //public List<T> GetComponents<T>() where T : Component
    //{
    //    return gameObject._components.OfType<T>().ToList();
    //}
    internal virtual void Awake()
    {
        
        
    }

    internal virtual void Update(GameTime gameTime)
    {

    }
    internal virtual void NetworkUpdate(GameTime gameTime)
    { 

    }
    internal virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {

    }



}
