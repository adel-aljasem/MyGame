using AdilGame;
using AdilGame.Components;
using AdilGame.Interfaces;
using AdilGame.Logic.Controllers;
using AdilGame.Logic.inventory;
using AdilGame.Logic.Weapons;
using AdilGame.Network;
using AdilGame.Network.Data;
using Cyotek.Drawing.BitmapFont;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Serialization;
using System;

public class PlayerController : Component, IStatus, IMovement
{
    //public int Id { get; set; }
    //public string OnlineID { get; set; }
    //public string Name { get; set; }
    public int RankLevel { get; set; } = 2;
    public CharcaterStatu State { get; set; }
    public int SpeedMultiplier { get; set; }
    public int Health { get; set; } = 100;
    public int Speed { get; set; } = 100;
    public Render2D render2D { get; set; }
    ColliderComponent Collider { get; set; }
    MouseState currentMouseState;
    public Player PlayerComingData { get; set; } = new Player();
    public Player PlayerGoingData { get; set; } = new Player();
    public InventoryController inventoryController { get; set; }
    public WeaponController weaponController { get; set; }

    public bool IsLocalPlayer { get; set; }
    public PlayerController()
    {
    }

    private void LoadAnimations()
    {
        var Arthax = render2D.LoadTexture("Character/girl", 32, 32);

        render2D.AddAnimation("Idle", new Animation(Arthax, 0, 1, 0.4f));
        render2D.AddAnimation("Walk", new Animation(Arthax, 24, 31, 0.2f));

        gameObject.Transform.Scale = new Vector2(32, 32);

        State = CharcaterStatu.Idle;
    }


    internal override void Awake()
    {
        gameObject.Tag = "player";
        Collider = gameObject.AddComponent<ColliderComponent>();
        Collider.IsDynamic = true;
        Collider.Radius = 7;
        Collider.ShowCollider = false;
        render2D = gameObject.AddComponent<Render2D>();
        weaponController = gameObject.AddComponent<WeaponController>();
        inventoryController = gameObject.AddComponent<InventoryController>();
        if (Collider != null)
        {
            Collider.OnCollision += HandleCollision;
        }
        LoadAnimations();

        render2D.Origin = new Vector2(gameObject.Transform.Scale.X / 2, gameObject.Transform.Scale.Y / 2);

    }
    IStatus a;

    public void FlipCharacterBasedOnMousePosition(float MouseX, float mouseY, float characterX)
    {
        bool shouldFaceLeft = MouseX < characterX;

        gameObject.GetComponent<Render2D>()?.FlipAnimation(shouldFaceLeft);
    }

    public void MouseUpdate(GameTime gameTime)
    {
        if (IsLocalPlayer)
        {

            currentMouseState = Mouse.GetState();
            var mouseInWorld = Game1.Instance.map._camera.ScreenToWorld(currentMouseState.X, currentMouseState.Y);
            PlayerGoingData.MouseData.MouseX = mouseInWorld.X;
            PlayerGoingData.MouseData.MouseY = mouseInWorld.Y;

        }
    }

    internal override void Update(GameTime gameTime)
    {
        if (PlayerComingData.Id == PlayerNetworkManager.Instance.playerId)
        {
            IsLocalPlayer = true;
            PlayerGoingData.MovementData.PositionX = gameObject.Transform.Position.X;
            PlayerGoingData.MovementData.PositionY = gameObject.Transform.Position.Y;

        }
        else { IsLocalPlayer = false; }
        FlipCharacterBasedOnMousePosition(PlayerComingData.MouseData.MouseX, PlayerComingData.MouseData.MouseY, gameObject.Transform.Position.X);
        UpdateOtherPlayersPosition();
        Move(gameTime);
        MouseUpdate(gameTime);
        string animation = Animation();
        render2D.PlayAnimation(animation);
        render2D.Position = gameObject.Transform.Position;
        //UpdateControllers();

    }

    private string Animation()
    {
        return (CharcaterStatu)PlayerComingData.MovementData.State switch
        {
            CharcaterStatu.Walk => "Walk",
            CharcaterStatu.Attack => "Attack",
            _ => "Idle"
        };
    }

 
    internal override void NetworkUpdate(GameTime gameTime)
    {
        base.NetworkUpdate(gameTime);
        if (IsLocalPlayer)
        {
            PlayerGoingData.OnlineID = PlayerComingData.OnlineID;
            PlayerGoingData.Id = PlayerComingData.Id;
            _ = PlayerNetworkManager.Instance.SendUpdatePlayerDataToServer(PlayerGoingData);

        }

    }



    private void HandleCollision(GameObject gameObject)
    {

        a = gameObject.GetComponentByInterface<IStatus>();
        Console.WriteLine("SMALL COLLISION");


    }


    public void UpdateOtherPlayersPosition()
    {
      
        Vector2 playercomingdata = new Vector2(PlayerComingData.MovementData.PositionX, PlayerComingData.MovementData.PositionY);
        gameObject.Transform.Position = Vector2.Lerp(gameObject.Transform.Position, playercomingdata, 0.01f);

        Collider.Velocity = new Vector2(PlayerComingData.MovementData.VelocityX, PlayerComingData.MovementData.VelocityY);
        //gameObject.Transform.Position = new Vector2(PlayerComingData.PositionX,PlayerComingData.po);
        if ((CharcaterStatu)PlayerComingData.MovementData.State == CharcaterStatu.Walk)
        {
            PlayerComingData.MovementData.State =(byte)CharcaterStatu.Walk;
        }
        else
        {
            PlayerComingData.MovementData.State =(byte)CharcaterStatu.Idle;
        }

    }



    public void Move(GameTime gameTime)
    {
        if (PlayerComingData.Id == PlayerNetworkManager.Instance.playerId)
        {
            Game1.Instance.map.CameraFocus = gameObject.Transform.Position;

            var keyboardState = Keyboard.GetState();
            var direction = Vector2.Zero;

            if (keyboardState.IsKeyDown(Keys.W))
            {
                direction.Y -= 1;

            }
            if (keyboardState.IsKeyDown(Keys.S))
            {
                direction.Y += 1;

            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                direction.X -= 1;

            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                direction.X += 1;
            }

            if (direction != Vector2.Zero)
            {
                direction.Normalize();
                //Collider.Velocity = direction * Speed;
                direction = direction * Speed;
                PlayerGoingData.MovementData.VelocityX = (short)direction.X;
                PlayerGoingData.MovementData.VelocityY = (short)direction.Y;
                PlayerGoingData.MovementData.State = (byte)CharcaterStatu.Walk;

            }
            else
            {
                //Collider.Velocity = Vector2.Zero;
                PlayerGoingData.MovementData.State = (byte)CharcaterStatu.Idle;
                PlayerGoingData.MovementData.VelocityX = 0;
                PlayerGoingData.MovementData.VelocityY = 0;
            }

        }


        //PlayerNetworkManager.Instance.SendPlayerPositionToServer(new Player { Id = Id, MouseX = MouseX, MouseY = MouseY, X = direction.X, Y = direction.Y });

    }

    public void Slow(int slowamount)
    {
        throw new NotImplementedException();
    }

    public void DmgTaken(int dmg)
    {
        Health -= dmg;
    }

    public void HealthIncress(int healthAmount)
    {
        Health += healthAmount;
    }


}