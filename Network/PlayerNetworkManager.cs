using AdilGame.Network.Data;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Xna.Framework;
using PandaGameLibrary.Components;
using PandaGameLibrary.System;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdilGame.Network
{
    public class PlayerNetworkManager
    {
        public static PlayerNetworkManager Instance { get; set; } = new PlayerNetworkManager();
        public List<PlayerController> playerControllers { get; set; } = new List<PlayerController>();
        public int playerId { get; set; } = 0;
        private List<Player> players { get; set; } = new List<Player>();
        public ConcurrentQueue<Action> mainThreadActions = new ConcurrentQueue<Action>();

        public PlayerNetworkManager()
        {

            Core.Instance.NetworkSystem.hubConnection.On<List<Player>>("OnConnectedPlayer", PlayersOnServer =>
            {
                // Queue the action to be processed on the main thread
                mainThreadActions.Enqueue(() =>
                {
                    // All logic that modifies the game state goes here
                    players = PlayersOnServer;
                    if (playerId == 0)
                    {
                        playerId = PlayersOnServer.Last().Id;
                    }

                    for (int i = 0; i < players.Count; i++)
                    {
                        var p = players[i];
                        bool playerExists = false;

                        for (int j = 0; j < playerControllers.Count; j++)
                        {
                            if (playerControllers[j].PlayerComingData.Id == p.Id)
                            {
                                playerExists = true;
                                break;
                            }
                        }

                        if (!playerExists)
                        {
                            GameObject playercon = new GameObject();
                            var player = playercon.AddComponent<PlayerController>();
                            player.PlayerComingData.Id = p.Id;
                            player.PlayerComingData.Name = p.Name;
                            player.PlayerComingData.OnlineID = p.OnlineID;
                            player.gameObject.Transform.Position = new Vector2(p.MovementData.PositionX, p.MovementData.PositionY);
                            player.PlayerGoingData.MouseData.Id = p.Id;
                            playerControllers.Add(player);
                            PandaGameLibrary.System.Core.Instance.GameObjectSystem.AddGameObject(playercon);
                        }
                    }
                    Console.WriteLine($"Player connected: {PlayersOnServer.Last().Name} {PlayersOnServer.Last().OnlineID}");
                });

            });

            Core.Instance.NetworkSystem.hubConnection.On<string>("OnRemovePlayerFromList", playerid =>
            {
                // Handle player disconnection logic here.
                // You can remove the player from your game world.
                Player disconnectedPlayer = players.Find(p => p.OnlineID == playerid);
                if (disconnectedPlayer != null)
                {
                    players.Remove(disconnectedPlayer);
                    PlayerController playerToRemove = playerControllers.Find(w => w.PlayerComingData.OnlineID == playerid);
                    playerControllers.Remove(playerToRemove);
                    Core.Instance.GameObjectSystem.RemoveGameObject(playerToRemove.gameObject);
                    Console.WriteLine($"Player disconnected: {disconnectedPlayer.Name} {disconnectedPlayer.Id}");
                }
            });

            //Core.Instance.NetworkSystem.hubConnection.On<byte[]>("OnPlayerPositionUpdate", (playerMovement) =>
            //{
            //    List<GameObject> allGameObjects = Core.Instance.GameObjectSystem.GetGameObjectsListByTag("player");
            //    for (int i = 0; i < allGameObjects.Count; i++)
            //    {
            //        GameObject playerControllerObject = allGameObjects[i];
            //        if (playerControllerObject != null)
            //        {
            //            PlayerController playerController = playerControllerObject.GetComponent<PlayerController>();
            //            //var Player = Compressing.DecompressPlayer(player);
            //            //if (playerController?.Id == playerMovement.IdPlayer)
            //            //{
            //            //    playerController.PlayerComingData.MovementData.VelocityX = playerMovement.VelocityX;
            //            //    playerController.PlayerComingData.MovementData.VelocityY = playerMovement.VelocityY;
            //            //    playerController.PlayerComingData.MovementData.PositionX = playerMovement.PositionX;
            //            //    playerController.PlayerComingData.MovementData.PositionY = playerMovement.PositionY;
            //            //    playerController.PlayerComingData.MovementData.State = playerMovement.State;
            //            //    //playerController.UpdateOtherPlayersPosition(player);
            //            //    break; // Assuming you only expect one match for the player ID, exit the loop
            //            //}
            //        }
            //    }
            //});

            //Core.Instance.NetworkSystem.hubConnection.On<byte[], float>("OnMousePositionUpdate", (Mouse,characterX) =>
            //{
            //    List<GameObject> allGameObjects = Core.Instance.GameObjectSystem.GetGameObjectsListByTag("player");
            //    for (int i = 0; i < allGameObjects.Count; i++)
            //    {
            //        GameObject playerController = allGameObjects[i];
            //        if (playerController != null)
            //        {
            //            PlayerController controller = playerController.GetComponent<PlayerController>();
            //            //var playerid = Compressing.DecompressString(playerId);
            //            //if (controller?.Id == Mouse.Id)
            //            //{
            //            //    //float xmouse = Compressing.DecompressFloat(MouseX);
            //            //    //float ymouse = Compressing.DecompressFloat(MouseY);
            //            //    //float xcharater = Compressing.DecompressFloat(characterX);
            //            //    controller.PlayerComingData.MouseData.MouseX = Mouse.MouseX;
            //            //    controller.PlayerComingData.MouseData.MouseY = Mouse.MouseY;
            //            //    //controller.FlipCharacterBasedOnMousePosition(MouseX, MouseY, characterX);
            //            //    break; // Assuming you only expect one match, you can break out of the loop once found
            //            //}
            //        }
            //    }
            //});


            Core.Instance.NetworkSystem.hubConnection.On<Player>("UpdatePlayerDataOnClient", (player) =>
            {
                List<GameObject> allGameObjects = Core.Instance.GameObjectSystem.GetGameObjectsListByTag("player");
                for (int i = 0; i < allGameObjects.Count; i++)
                {
                    GameObject playerController = allGameObjects[i];
                    if (playerController != null)
                    {
                        PlayerController controller = playerController.GetComponent<PlayerController>();
                        //var playerid = Compressing.DecompressString(playerId);
                        if (controller?.PlayerComingData.Id == player.Id)
                        {

                            controller.PlayerComingData = player;
                            break; // Assuming you only expect one match, you can break out of the loop once found
                        }
                    }
                }
            });
        }

        public async Task SendMouseLocationToServer(MouseData mouseData)
        {
            try
            {
                //byte[] idplayer = Compressing.CompressString(playerid);
                //byte[] xmouse = Compressing.CompressFloat(mouseX);
                //byte[] ymouse = Compressing.CompressFloat(MouseY);

                await Core.Instance.NetworkSystem.hubConnection.SendAsync("UpdateMosueLocation", mouseData);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating player position: " + ex.Message);
            }
        }



        public async Task SendPlayerPositionToServer(MovementData movementPlayer)
        {
            try
            {
                //var Player = Compressing.CompressPlayer(player);
                // Send the updated position to the server.
                await Core.Instance.NetworkSystem.hubConnection.SendAsync("UpdatePlayerPosition", movementPlayer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating player position: " + ex.Message);
            }
        }
        Random r = new Random();

        public async Task SendUpdatePlayerDataToServer(Player player)
        {
            try
            {
                //var Player = Compressing.CompressPlayer(player);
                // Send the updated position to the server.
                await Core.Instance.NetworkSystem.hubConnection.SendAsync("UpdatePlayerData", player);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating player position: " + ex.Message);
            }
        }

        public async Task ConnectServer(string playerName)
        {
            Player player = new Player
            {
                Id = playerId,
                Name = playerName,
                MovementData = new MovementData
                {
                    PositionX = 550,
                    PositionY = 550,
                }
                
            };
            try
            {
                await Core.Instance.NetworkSystem.hubConnection.SendAsync("PlayerConnected", player);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to the game server: " + ex.Message);
            }



        }

        public async Task DisconnectFromGameServer(string id)
        {
            try
            {
                await Core.Instance.NetworkSystem.hubConnection.SendAsync("PlayerDisconnected", playerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to the game server: " + ex.Message);
            }
        }


       

    }
}
