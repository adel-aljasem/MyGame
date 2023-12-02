using AdilGame;
using AdilGame.Components;
using AdilGame.Interfaces;
using AdilGame.Network;
using AdilGame.System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

public class PlayerController : Component, IStatus, IMovement, IGameObject
{
    public string Id { get; set; }
    public string OnlineID { get; set; }
    public string Name { get; set; }
    public CharcaterStatu State { get; set; }
    public float MouseX { get; set; }
    public float MouseY { get; set; }
    public int SpeedMultiplier { get; set; }
    public int Health { get; set; } = 100;
    public int Speed { get; set; } = 100;
    public bool ConnectToServer { get; set; } = true;
    Player Player;
    public List<PlayerController> PlayerControllers { get; set; } = new List<PlayerController>();
    public Render2D render2D { get; set; }
    ColliderComponent Collider { get; set; }
    MouseState currentMouseState;
    MouseState previousMouseState;

    public PlayerController()
    {
    }

    private void LoadAnimations()
    {
        var Arthax = render2D.LoadTexture("Character/girl");

        render2D.AddAnimation("Idle", new Animation(Arthax, 0, 1, 0.4f));
        render2D.AddAnimation("Walk", new Animation(Arthax, 24, 31, 0.2f));

        gameObject.Transform.Scale = new Vector2(32, 32);

        State = CharcaterStatu.Idle;
    }


    public override void Awake()
    {

        gameObject.Tag = "player";
        Collider = gameObject.AddComponent<ColliderComponent>();
        Collider.IsDynamic = true;
        var RangeCollider = gameObject.AddComponent<ColliderComponent>();
        RangeCollider.Radius = 30;
        Collider.Radius = 7;
        render2D = gameObject.AddComponent<Render2D>();
        if (Collider != null && RangeCollider != null)
        {
            Collider.OnCollision += HandleCollision;
            RangeCollider.OnCollision += HandleLargeCollision;
        }
        LoadAnimations();



    }
    IStatus a;

    private void FlipCharacterBasedOnMousePosition(float MouseX, float CharacterX)
    {
        bool shouldFaceLeft = MouseX < CharacterX;
        gameObject.GetComponent<Render2D>()?.FlipAnimation(shouldFaceLeft);


    }
   

    public void MouseUpdate()
    {
        if(Id == PlayerNetworkManager.Instance.playerId)
        {
            currentMouseState = Mouse.GetState();
            var mouseInWorld = Game1.Instance.map._camera.ScreenToWorld(currentMouseState.X, currentMouseState.Y);
            FlipCharacterBasedOnMousePosition(mouseInWorld.X, gameObject.Transform.Position.X);

        }

    }

    public override void Update(GameTime gameTime)
    {
        MouseUpdate();
        if (currentMouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
        {
            // Handle left mouse button click
        }

        Move(gameTime); // Handle Input and move the player.

        var animation = State switch
        {
            CharcaterStatu.Walk => "Walk",
            CharcaterStatu.Attack => "Attack",
            _ => "Idle"
        };
        render2D.PlayAnimation(animation);
        render2D.Position = gameObject.Transform.Position;
    }



    private void HandleCollision(GameObject gameObject)
    {

        a = gameObject.GetComponentByInterface<IStatus>();

    }

    private void HandleLargeCollision(GameObject gameObject)
    {
        Console.WriteLine(gameObject.GameObjectId);

    }


    public void UpdateOtherPlayersPosition(Player player)
    {

        gameObject.Transform.Position = new Vector2(player.X, player.Y);
        FlipCharacterBasedOnMousePosition(player.MouseX, player.X);
        if (player.State == CharcaterStatu.Walk)
        {
            State = CharcaterStatu.Walk;
        }


    }



    public Vector2 Move(GameTime gameTime)
    {
        if (Id == PlayerNetworkManager.Instance.playerId)
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
                Collider.Velocity = direction * Speed;
                //State = CharcaterStatu.Walk;
                //Player.Id = Id;
                //Player.Name = Name;
                //Player.State = State;
                //Player.X = gameObject.Transform.Position.X;
                //Player.Y = gameObject.Transform.Position.Y;
                PlayerNetworkManager.Instance.UpdatePlayerPosition(new Player { Id = Id, MouseX = MouseX,MouseY = MouseY,X = gameObject.Transform.Position.X,Y = gameObject.Transform.Position.Y});

                return direction;
            }
            else
            {
                Collider.Velocity = Vector2.Zero;
                State = CharcaterStatu.Idle;
                return direction;

            }

        }
        //else
        //{
        //    // updateOtherPlayer
        //    gameObject.Transform.Position = new Vector2(Player.X, Player.Y);
        //    FlipCharacterBasedOnMousePosition(Player.MouseX, Player.X);
        //    if (Player.State == CharcaterStatu.Walk)
        //    {
        //        State = CharcaterStatu.Walk;
        //    }

        //}

        return new Vector2();

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