using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Network
{
    public class NetworkUpdate
    {
        private double interval;
        private double elapsed;
        private bool isReady;

        public NetworkUpdate(double intervalSeconds)
        {
            interval = intervalSeconds;
            elapsed = 0;
            isReady = false;
        }

        public void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.TotalSeconds;
            if (elapsed >= interval)
            {
                isReady = true;
            }
        }

        public bool IsReady()
        {
            if (isReady)
            {
                isReady = false; // Reset the flag
                elapsed -= interval; // Adjust for the next cycle
                return true;
            }
            return false;
        }
    }


}
