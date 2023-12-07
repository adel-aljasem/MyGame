using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.System
{
    public class RenderSystem
    {
        public void Draw(SpriteBatch spriteBatch, List<GameObject> gameObjects, GameTime gameTime)
        {
            var sortedObjects = gameObjects
                .OfType<GameObject>()
                .OrderBy(gameobject => gameobject.Transform.Position.Y)
                .ToArray(); // Create a non-dynamic snapshot of the game objects

            for (int i = 0; i < sortedObjects.Length; i++)
            {
                sortedObjects[i].Draw(spriteBatch, gameTime);

            }
        }


    }

}
