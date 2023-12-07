using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.System
{
    public class CollisionSystem
    {
        public void CheckCollisions(List<GameObject> gameObjects)
        {
            var gameObjectsSnapshot = gameObjects.ToList(); // Snapshot of the gameObjects
            var colliderPairs = gameObjectsSnapshot.SelectMany(go => go.GetComponents<ColliderComponent>()
                                                .ToList() // Snapshot of components
                                                .Where(component => component != null)
                                                .Select(component => new { GameObject = go, Collider = component }))
                                                .ToList();

            for (int i = 0; i < colliderPairs.Count; i++)
            {
                var colliderA = colliderPairs[i].Collider;
                var gameObjectA = colliderPairs[i].GameObject;

                for (int j = i + 1; j < colliderPairs.Count; j++)
                {
                    var colliderB = colliderPairs[j].Collider;
                    var gameObjectB = colliderPairs[j].GameObject;

                    // Skip collision check if colliders belong to the same GameObject
                    if (gameObjectA.GameObjectId == gameObjectB.GameObjectId)
                    {
                        continue;
                    }

                    // Collision check logic
                    if (colliderA.Bounds.Intersects(colliderB.Bounds) ||
                        Vector2.Distance(colliderA.Center, colliderB.Center) < (colliderA.Radius + colliderB.Radius))
                    {
                        colliderA.OnCollide(gameObjectB);
                        colliderB.OnCollide(gameObjectA);
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
                Vector2 directionToOther = colliderA.Center - colliderB.Center;
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

    }

}
