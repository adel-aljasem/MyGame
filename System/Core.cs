using AdilGame.Network;
using AdilGame.World;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using Myra.MML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.System
{
    public class Core
    {
        private Stopwatch stopwatch = new Stopwatch();

        public static Core Instance { get; set; } = new Core();
        public HubConnection hubConnection;
        private List<GameObject> AllGameObjectsInGame { get;  set; } = new List<GameObject>();
        public SpatialGrid spatialGrid;
        private Task updateTask = Task.CompletedTask;
        public GameObject player { get; set; } = new GameObject();
        const float targetPhysicsUpdateTime = 1f / 30f; // 60 updates per second
        float accumulator = 0f;

        public Core()
        {

            hubConnection = new HubConnectionBuilder()
           .WithUrl("http://192.168.1.25:5000/gameHub") // Replace with your server URL
             .Build();

            // Start the SignalR connection.
            _ = hubConnection.StartAsync()
                .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("Error starting SignalR connection: " + task.Exception.GetBaseException());
                    }
                    else
                    {
                        Console.WriteLine("SignalR connection started successfully.");
                    }
                });
            stopwatch.Start();
            spatialGrid = new SpatialGrid(1920, 1000, 500);
            
        }
        // Method to add a GameObject
        public void AddGameObject(GameObject gameObject)
        {
            // You can include additional logic here if needed
            AllGameObjectsInGame.Add(gameObject);
        }
        public List<GameObject> GetAllGameObjects()
        {
            // Returns a new list that is a copy of the current AllGameObjectsInGame
            return new List<GameObject>(AllGameObjectsInGame);
        }
        // Method to remove a GameObject
        public void RemoveGameObject(GameObject gameObject)
        {
            // Additional logic for removal can be included here
            AllGameObjectsInGame.Remove(gameObject);
        }

        // Method to get GameObjects based on a criteria (example: by tag)
        public GameObject GetGameObjectsByTag(string tag)
        {
            return AllGameObjectsInGame.Where(gameObject => gameObject.Tag == tag).FirstOrDefault();
        }

        public void CheckCollisions(List<GameObject> gameObjects)
        {
            var colliderComponents = gameObjects.Select(go => go.GetComponent<ColliderComponent>())
                                    .Where(component => component != null)
                                    .ToList();

            for (int i = 0; i < colliderComponents.Count; i++)
            {

                var colliderA = colliderComponents[i];
                for (int j = i + 1; j < colliderComponents.Count; j++)
                {
                    var colliderB = colliderComponents[j];
                  

                    if (colliderA.Bounds.Intersects(colliderB.Bounds) || Vector2.Distance(colliderA.Center, colliderB.Center) < (colliderA.Radius + colliderB.Radius))
                    {

                        colliderA.OnCollide(gameObjects[j]);
                        colliderB.OnCollide(gameObjects[i]);
                        ResolveCollision(colliderB, colliderA);
                        ResolveCollision(colliderA, colliderB);

                    }
                }
            }
        }


        private void ResolveCollision(ColliderComponent colliderA, ColliderComponent colliderB)
        {
            if (colliderA.IsDynamic && !colliderB.IsDynamic)
            {
                Vector2 directionToOther =   colliderA.Center - colliderB.Center;
                float distance = directionToOther.Length();
                float minDistance = colliderA.Radius + colliderB.Radius;

                if (distance < minDistance && colliderA.IsDynamic)
                {
                    float overlap = minDistance - distance;
                    Vector2 moveDirection = Vector2.Normalize(directionToOther);

                    // Move colliderA outside of colliderB's boundary
                    colliderA.gameObject.Transform.Position += moveDirection * overlap;

                    //// Stop the collider's movement in the direction of the collision
                    //colliderA.Velocity -= Vector2.Dot(colliderA.Velocity, moveDirection) * moveDirection;
                }

            }
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            var sortedObjects = AllGameObjectsInGame
    .OfType<GameObject>()
    .OrderBy(gameobject => gameobject.Transform.Position.Y)
    .ToList();


            for (int i = 0; i < sortedObjects.Count ; i++)
            {
                sortedObjects[i].Draw(spriteBatch, gameTime);

            }

        }

        public void Update(GameTime gameTime)
        {

            accumulator += (float)gameTime.ElapsedGameTime.TotalSeconds;
            while (accumulator >= targetPhysicsUpdateTime)
            {
                UpdatePhysics(targetPhysicsUpdateTime);
                accumulator -= targetPhysicsUpdateTime;
            }


            for (int i = 0; i < AllGameObjectsInGame.Count ; i++)
            {
                AllGameObjectsInGame[i].Update( gameTime);
            }


            if (updateTask.IsCompleted)
            {
                updateTask = Task.Run(() => CheckCollisions(AllGameObjectsInGame));

            }



            //var nearbyObjects = spatialGrid.GetNearbyObjects(player);

            // Handle collisions
            //CheckCollisions(AllGameObjectsInGame);

        }


        private void UpdatePhysics(float deltaTime)
        {

        }

    }
}
