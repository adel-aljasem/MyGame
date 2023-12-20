using AdilGame.Logic.inventory.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.inventory
{
    public class InventorySystem
    {
        public static InventorySystem Instance { get; set; } = new InventorySystem();
        public void ItemDropAnimation(GameTime gameTime)
        {
            var items = Core.Instance.GameObjectSystem.GetAllGameObjects();
            foreach (var item in items)
            {
                if (item != null)
                {
                    var exsitItem = item.GetComponentByInterface<Iitem>();
                    if (exsitItem != null)
                    {
                        if (exsitItem.IsDropped)
                        {
                            exsitItem.gameObject.Transform.Position = CalculateVerticalMovement(exsitItem.gameObject.Transform.Position, 0.06f, 2f, (float)gameTime.TotalGameTime.TotalSeconds);
                            exsitItem.Collider.OnCollision += ItemOnCollistion;
                        }
                    }
                }
            }
        }

        public void ItemOnCollistion(GameObject gameObject)
        {
            if(gameObject != null)
            {
                var item = gameObject.GetComponentByInterface<Iitem>();
                Console.WriteLine(item.Name);
            }
        }

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
