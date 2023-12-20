using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
    private double networkUpdateElapsed = 0;
    private const double NetworkUpdateInterval = 1/15.0; // 10 times per second

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

    public T AddComponentByInterface<T>() where T : class
    {
        // Find a type that implements the interface T and has a parameterless constructor
        var type = Assembly.GetExecutingAssembly().GetTypes()
                    .FirstOrDefault(t => typeof(T).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null);

        if (type != null)
        {
            // Create an instance of the found type
            var component = Activator.CreateInstance(type) as T;

            if (component is Component comp)
            {
                comp.gameObject = this; // Set the gameObject property
                comp.gameobjectId = GameObjectId;
                comp.Awake();
                _components.Add(comp);
            }

            return component;
        }

        throw new InvalidOperationException($"No component found that implements {typeof(T).Name}");
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

    public void RemoveComponent<T>() where T : Component
    {
        var component = _components.OfType<T>().FirstOrDefault();
        if (component != null)
        {
            _components.Remove(component);
        }
    }

    public void Update(GameTime gameTime)
    {
        networkUpdateElapsed += gameTime.ElapsedGameTime.TotalSeconds;
        for (int i = 0; i < _components.Count; i++)
        {
            var component = _components[i];
            if (component.IsEnabled)
            {
                component.Update(gameTime);
            }
            // Check if it's time to call NetworkUpdate
            if (networkUpdateElapsed >= NetworkUpdateInterval)
            {
                component.NetworkUpdate(gameTime);
            }

        }
        // Reset the timer after NetworkUpdate has been called on all components
        if (networkUpdateElapsed >= NetworkUpdateInterval)
        {
            networkUpdateElapsed = 0;
        }

    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var component in _components)
        {
            if (component.IsEnabled)
            {
                component.Draw(spriteBatch, gameTime);
            }
        }
    }





}