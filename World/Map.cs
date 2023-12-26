using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Tiled;
using MonoGame.Extended.Tiled.Renderers;
using MonoGame.Extended.ViewportAdapters;
using PandaGameLibrary.System;
using System;
using System.Threading.Tasks;

namespace AdilGame.World
{
    public class Map
    {
        TiledMap _tiledMap;
        TiledMapRenderer _tiledMapRenderer;
        public OrthographicCamera _camera;
        public Vector2 CameraFocus { get; set; }
        public PlayerController Player { get; set; }
        BoxingViewportAdapter viewportadapter = new BoxingViewportAdapter(Game1.Instance.Window, Game1.Instance.GraphicsDevice, Game1.Instance.Window.ClientBounds.Width, Game1.Instance.Window.ClientBounds.Height);
        bool WindowSizeChanged = false;
        public ObjectLayer objectLayer1 { get; set; }
        public Map(string mapName)
        {
            _tiledMap = Game1.Instance.Content.Load<TiledMap>(mapName);
            _tiledMapRenderer = new TiledMapRenderer(Game1.Instance.GraphicsDevice, _tiledMap);
            Game1.Instance.Window.ClientSizeChanged += OnResize;
            _camera = new OrthographicCamera(viewportadapter);
            _camera.Zoom = 2f;
            objectLayer1 = new ObjectLayer();
            objectLayer1.InitializeObjects("Object Layer 1", _tiledMap);
        }

        private  void OnResize(object sender, EventArgs args)
        {
            Game1.Instance.screenManager.SetWindowSize(Game1.Instance.Window.ClientBounds.Width, Game1.Instance.Window.ClientBounds.Height);
            WindowSizeChanged = true;

        }

        public void LoadContent(string mapName)
        {
            try
            {

            }
            catch
            {
            }
        }

        public void GetPlayer()
        {
            Core.Instance.GameObjectSystem.GetAllGameObjects().ForEach(x =>
            {
                var playerController = x.GetComponent<PlayerController>();
                if (playerController != null && Player == null)
                {
                    Player = playerController;
                }
            });

        }


        public void Initialize()
        {


        }
        public void Update(GameTime gameTime)
        {
            _tiledMapRenderer.Update(gameTime);
            _camera.LookAt(CameraFocus);
        }

        public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            if (WindowSizeChanged)
            {
                Game1.Instance.screenManager.SetWindowSize(Game1.Instance.Window.ClientBounds.Width, Game1.Instance.Window.ClientBounds.Height);
                WindowSizeChanged = false;
            }


            var viewMatrix = _camera.GetViewMatrix();
            var scaleMatrix = Game1.Instance.screenManager.GetScaleMatrix(3, 3);
            var combinedMatrix = Matrix.Multiply(scaleMatrix, viewMatrix);

            //objectLayer1.DrawObjectLayer(spriteBatch);

            _tiledMapRenderer.Draw(viewMatrix);

            //DrawObjectLayer(spriteBatch, "Object Layer 1");
            //objectLayer1.Draw(spriteBatch,gameTime);


        }

    }
}
