using AdilGame.content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace AdilGame;

public class Game1 : Game
{
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;
    ScreenManager screenManager;
    public static Game1 Instance ;
    public GameObject Player { get; set; } = new GameObject();
    PlayerController playerController1;

    public Game1()
    {
        graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        screenManager = new ScreenManager(graphics, 1280,720);
        Window.AllowUserResizing = true;
        Instance = this;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        playerController1 = Player.AddComponent<PlayerController>();
        screenManager.LoadContent(Content);

    }

    protected override void Update(GameTime gameTime)
    {
        screenManager.Update(gameTime);
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
      
        base.Update(gameTime);
        playerController1.Update(gameTime);
        
    }

    protected override void Draw(GameTime gameTime)
    {

        GraphicsDevice.Clear(Color.CornflowerBlue);

        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend,
         SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullCounterClockwise,null,screenManager.GetScaleMatrix());



        screenManager.Draw(gameTime, spriteBatch);
        playerController1.Draw(spriteBatch, gameTime);
        spriteBatch.End();

        base.Draw(gameTime);
    }
}