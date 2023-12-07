using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.System
{
    public class GameObjectSystem
    {
        private List<GameObject> allGameObjects = new List<GameObject>();

        public void AddGameObject(GameObject gameObject)
        {
            allGameObjects.Add(gameObject);
        }

        public void RemoveGameObject(GameObject gameObject)
        {
            allGameObjects.Remove(gameObject);
        }

        public List<GameObject> GetAllGameObjects()
        {
            return allGameObjects;
        }

        public GameObject GetGameObjectByTag(string tag)
        {
            return allGameObjects.FirstOrDefault(g => g.Tag == tag);
        }

        public void UpdateGameObjects(GameTime gameTime)
        {
            for (int i = 0; i < allGameObjects.Count; i++)
            {
                allGameObjects[i].Update(gameTime);
            }
        }

    }

}
