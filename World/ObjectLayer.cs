using AdilGame.Logic;
using AdilGame.System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Tiled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.World
{
    public class ObjectLayer
    {
        Texture2D _objectImage;
        private TiledMap _tiledMap { get; set; }
        private string ObjectLayerName { get; set; }
        private List<GameObject> _objects = new List<GameObject>();
        TiledMapObjectLayer objectLayer => _tiledMap.GetLayer<TiledMapObjectLayer>(ObjectLayerName);

        public ObjectLayer()
        {
        }



        //public void DrawObjectLayer(SpriteBatch spriteBatch)
        //{
        //    // Retrieve the object layer

        //    if (objectLayer == null)
        //        return;

        //    // Sort the objects by their Y-coordinate
        //    var sortedObjects = objectLayer.Objects
        //        .OfType<TiledMapTileObject>()
        //        .Where(tileObject => tileObject.Tile != null)
        //        .OrderBy(tileObject => tileObject.Position.Y)
        //        .ToList();

        //    // Draw each sorted object
        //    foreach (var tileObject in sortedObjects)
        //    {
        //        var sourceRect = new Rectangle(
        //            Convert.ToInt32(tileObject.Tile.Properties["ImageRectX"]),
        //            Convert.ToInt32(tileObject.Tile.Properties["ImageRectY"]),
        //            64, 64 // Assuming all tiles are 64x64
        //        );



        //        if (tileObject.IsVisible)
        //        {
        //            spriteBatch.Draw(_objectImage, tileObject.Position, sourceRect, Color.White);
        //        }
        //    }
        //}

        public void InitializeObjects(string objectlayerName, TiledMap tiledMap)
        {
            ObjectLayerName = objectlayerName;
            _tiledMap = tiledMap;
            _objectImage = Game1.Instance.Content.Load<Texture2D>("map/ob_0");
            foreach (var obj in objectLayer.Objects)
            {
                GameObject gameObject = new GameObject();
                gameObject.Tag = "res";

                gameObject.Transform.Position = obj.Position;
                gameObject.Transform.Scale = new Vector2(obj.Size.Width, obj.Size.Height);
                var collider = gameObject.AddComponent<ColliderComponent>();
                collider.IsDynamic = false;
                var objectholder = gameObject.AddComponent<TileMapObjectHolderComponent>();
                collider.LetMeDraw = true;
                // Cast obj to TiledMapTileObject to access the Tile property
                if (obj is TiledMapTileObject tileObject && tileObject.Tile != null)
                {
                    objectholder.TiledMapObject = tileObject;
                    objectholder.LoadData();
                    var col = tileObject.Tile.Objects.Where(w => w.Name == "col").FirstOrDefault();
                    if (col != null)
                    {
                        collider.DrawColliderFromTiled(col.Position.X, col.Position.Y);
                        collider.Width = col.Size.Width;
                        collider.Height = col.Size.Height;

                    }
                }

                Core.Instance.AddGameObject(gameObject);
                Core.Instance.spatialGrid.AddObject(gameObject);
            }
        }

        //public void InitializeTileColliders(string tileLayerName, TiledMap tiledMap)
        //{
        //    var tileLayer = tiledMap.GetLayer<TiledMapTileLayer>(tileLayerName);
        //    if (tileLayer == null)
        //        return;

        //    for (int x = 0; x < tileLayer.Width; x++)
        //    {
        //        for (int y = 0; y < tileLayer.Height; y++)
        //        {
        //            var tile = tileLayer.GetTile(x, y);
        //            if (tile != null && tile.IsCollidable)
        //            {
        //                tile.
        //                // Create a game object for each collidable tile
        //                GameObject tileObject = new GameObject();
        //                tileObject.Tag = "tileCollider";

        //                // Calculate the position based on the tile's x, y, and the tile size
        //                Vector2 position = new Vector2(x * tiledMap.TileWidth, y * tiledMap.TileHeight);
        //                tileObject.Transform.Position = position;

        //                // Set the scale to the size of the tile
        //                tileObject.Transform.Scale = new Vector2(tiledMap.TileWidth, tiledMap.TileHeight);

        //                var collider = tileObject.AddComponent<ColliderComponent>();
        //                // Set up your collider component based on your game's needs
        //                collider.ColliderPointAtBottom = false; // Set this as needed
        //                collider.Radius = tiledMap.TileWidth / 2; // For circle colliders, if needed

        //                // Add the tile game object to your game
        //                Core.Instance.AddGameObject(tileObject);
        //            }
        //        }
        //    }
        //}



    }
}
