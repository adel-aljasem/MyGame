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
            // Create a copy of the gameObjects list to avoid InvalidOperationException
            var gameObjectsCopy = new List<GameObject>(gameObjects);

            var sortedObjects = gameObjectsCopy
                .OrderBy(gameobject => gameobject.Transform.Position.Y)
                .ToArray(); // Create a non-dynamic snapshot of the game objects

            foreach (var gameObject in sortedObjects)
            {
                gameObject.Draw(spriteBatch, gameTime);
            }
        }


    }

}
