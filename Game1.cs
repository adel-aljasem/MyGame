using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Myra;
using System.Windows;
using AdilGame.Network;
using AdilGame.System;
using System.Threading.Tasks;
using AdilGame.World;
using System.Numerics;
using AdilGame.UI;
using AdilGame.Logic.inventory;

namespace AdilGame;

public class Game1 : Game
{
    public GraphicsDeviceManager graphics;
    public SpriteBatch spriteBatch;
    public ScreenManager screenManager;
    public static Game1 Instance;
    UIManager UIManager;
    public Map map { get; set; }
    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        IsMouseVisible = true;
        Content.RootDirectory = "Content";
        Instance = this;
        screenManager = new ScreenManager(graphics, 1920, 1080);
        Window.AllowUserResizing = true;
        MyraEnvironment.Game = this;
        map = new Map("map/ge");

    }

    protected override async void Initialize()
    {
        base.Initialize();
        map.Initialize();


        PlayerNetworkManager.Instance.ConnectServer($"e");



    }


    protected override async void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        screenManager.LoadContent(Content);
        UIManager = new UIManager();
    }
    private Task updateTask = Task.CompletedTask;

    protected override void Update(GameTime gameTime)
    {
        UIManager = new UIManager();
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        map.Update(gameTime);
        screenManager.Update(gameTime);
        //Run Core.Instance.Update(gameTime) on a background thread if the previous task has completed
        while (PlayerNetworkManager.Instance.mainThreadActions.TryDequeue(out Action action))
        {
            action.Invoke();
        }

        Core.Instance.Update(gameTime);
        UIManager.Update(gameTime);
        InventorySystem.Instance.Update(gameTime);
        base.Update(gameTime);

    }



    protected override void Draw(GameTime gameTime)
    {


        GraphicsDevice.Clear(Color.CornflowerBlue);
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
         SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise, null, map._camera.GetViewMatrix());

        map.Draw(spriteBatch, gameTime);
        Core.Instance.Draw(spriteBatch, gameTime);

        //playerNameInputUI1.Draw();

        spriteBatch.End();

        screenManager.Draw(gameTime, spriteBatch);
        UIManager.Draw(gameTime, spriteBatch);
        base.Draw(gameTime);
    }



    protected override void OnExiting(Object sender, EventArgs args)
    {
        base.OnExiting(sender, args);

        //PlayerNetworkManager.Instance.DisconnectFromGameServer(PlayerNetworkManager.Instance.playerId);
        // Stop the threads
    }

}