using AdilGame.Logic.inventory.Items;
using Microsoft.Xna.Framework;
using PandaGameLibrary.Components;
using PandaGameLibrary.System;
using System;
using System.Collections.Generic;

namespace AdilGame.Logic.inventory
{
    public class InventorySystem
    {
        public static InventorySystem Instance { get; set; } = new InventorySystem();
        public void ItemDropAnimation(GameTime gameTime)
        {
            var items = Core.Instance.GameObjectSystem.GetAllGameObjects();
            // Create a copy of the items list to avoid InvalidOperationException
            var itemsCopy = new List<GameObject>(items);

            foreach (var item in itemsCopy)
            {
                if (item != null)
                {
                    var existItem = item.GetComponentByInterface<Iitem>();
                    if (existItem != null && existItem.IsDropped)
                    {
                        existItem.gameObject.Transform.Position = CalculateVerticalMovement(
                            existItem.gameObject.Transform.Position,
                            0.06f,
                            2f,
                            (float)gameTime.TotalGameTime.TotalSeconds);

                        // Uncomment if you need to handle collisions
                        // existItem.Collider.OnCollision += ItemOnCollision;
                    }
                }
            }
        }

        //public void ItemOnCollistion(GameObject gameObject)
        //{
        //    // it make sure it only collide with player
        //    //if(gameObject != null && gameObject.GetComponent<PlayerController>()!= null)
        //    //{
        //    //    var item = gameObject.GetComponentByInterface<Iitem>();
        //    //    Console.WriteLine(item.Name);
        //    //}
        //}

        private Vector2 CalculateVerticalMovement(Vector2 originalPosition, float amplitude, float speed, float elapsedTime)
        {
            float newY = originalPosition.Y + amplitude * (float)Math.Sin(elapsedTime * speed);
            return new Vector2(originalPosition.X, newY);
        }


        public void Update(GameTime gameTime)
        {
            ItemDropAnimation(gameTime);
        }
    }
}
