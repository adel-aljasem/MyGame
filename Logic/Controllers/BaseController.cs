using AdilGame.Logic.inventory;
using AdilGame.Network;
using AdilGame.Network.Data;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Controllers
{
    public class BaseController : Component
    {
        public static Inventory Inventory { get; private set; }
        public  Player PlayerComingData { get; set; } = new Player();
        public  Player PlayerGoingData { get; set; } = new Player();

        internal override void Awake()
        {
            base.Awake();
        }

        internal override void Update(GameTime gameTime)
        {
           
        }

        internal override void NetworkUpdate(GameTime gameTime)
        {
            if (PlayerComingData.Id == PlayerNetworkManager.Instance.playerId)
            {
                PlayerGoingData.MovementData.PositionX = gameObject.Transform.Position.X;
                PlayerGoingData.MovementData.PositionY = gameObject.Transform.Position.Y;
                PlayerGoingData.OnlineID = PlayerComingData.OnlineID;
                PlayerGoingData.Id = PlayerComingData.Id;
                _ = PlayerNetworkManager.Instance.SendUpdatePlayerDataToServer(PlayerGoingData);

            }
        }
    }
}
