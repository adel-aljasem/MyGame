using AdilGame.Interfaces;
using AdilGame.System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Xna.Framework;
using StbImageSharp;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Network
{
    public class PlayerNetworkManager
    {
        public static PlayerNetworkManager Instance { get; set; } = new PlayerNetworkManager();
        public List<PlayerController> playerControllers { get; set; } = new List<PlayerController>();
        public string playerId { get; set; }
        private List<Player> players { get; set; } = new List<Player>();
        public ConcurrentQueue<Action> mainThreadActions = new ConcurrentQueue<Action>();

        public PlayerNetworkManager()
        {

            Core.Instance.hubConnection.On<List<Player>>("OnConnectedPlayer", PlayersOnServer =>
            {
                // Queue the action to be processed on the main thread
                mainThreadActions.Enqueue(() =>
                {
                    // All logic that modifies the game state goes here
                    players = PlayersOnServer;
                    if (playerId == null)
                    {
                        playerId = PlayersOnServer.Last().Id;
                    }

                    foreach (var p in players)
                    {
                        if (playerControllers.FirstOrDefault(w => w.Id == p.Id) == null)
                        {
                            GameObject playercon = new GameObject();
                            var player = playercon.AddComponent<PlayerController>();
                            player.Id = p.Id;
                            player.Name = p.Name;
                            player.OnlineID = p.OnlineID;
                            player.gameObject.Transform.Position = new Vector2(p.X, p.Y);
                            playerControllers.Add(player);
                            Core.Instance.AddGameObject(playercon);
                            Core.Instance.spatialGrid.AddObject(playercon);
                            Core.Instance.player = playercon;

                        }
                    }
                    Console.WriteLine($"Player connected: {PlayersOnServer.Last().Name} {PlayersOnServer.Last().OnlineID}");
                });

            });

            Core.Instance.hubConnection.On<string>("RemovePlayerFromList", async playerid =>
            {
                // Handle player disconnection logic here.
                // You can remove the player from your game world.
                Player disconnectedPlayer = players.Find(p => p.Id == playerid);
                if (disconnectedPlayer != null)
                {
                    players.Remove(disconnectedPlayer);
                    PlayerController playerToRemove = playerControllers.Find(w => w.Id == playerid);
                    playerControllers.Remove(playerToRemove);
                    Console.WriteLine($"Player disconnected: {disconnectedPlayer.Name} {disconnectedPlayer.Id}");
                }
            });

            Core.Instance.hubConnection.On<Player>("PlayerPositionUpdated", (player) =>
            {
                foreach (GameObject playerController in Core.Instance.GetAllGameObjects())
                {
                    if (playerController?.GetComponent<PlayerController>()?.Id == player.Id)
                    {
                        playerController.GetComponent<PlayerController>().UpdateOtherPlayersPosition(player);

                    }
                }
            });


        }

        public async Task SendMouseLocationToServer(Player player)
        {
            try
            {
                // Send the updated position to the server.
                await Core.Instance.hubConnection.SendAsync("UpdateMosueLocation", player);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating player position: " + ex.Message);
            }
        }



        public async Task UpdatePlayerPosition(Player player)
        {
            try
            {
                // Send the updated position to the server.
                await Core.Instance.hubConnection.SendAsync("UpdatePlayerPosition", player);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error updating player position: " + ex.Message);
            }
        }
        Random r = new Random();


        public async Task ConnectServer(string playerName)
        {
            r.Next(2000);
            Player player = new Player
            {
                Id = playerId,
                Name = playerName,
                X = r.Next(2000),
                Y = r.Next(2000)
            };


            try
            {
                await Core.Instance.hubConnection.SendAsync("PlayerConnected", player);
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
                await Core.Instance.hubConnection.SendAsync("PlayerDisconnected", playerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to the game server: " + ex.Message);
            }
        }


    }
}
