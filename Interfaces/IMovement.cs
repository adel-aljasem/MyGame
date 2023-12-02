using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Interfaces
{
    public interface IMovement : IGameObject
    {
        public int Speed { get; set; }
        public int SpeedMultiplier { get; set; }
        public Vector2 Move(GameTime gameTime);
        public void Slow(int slowAmount);
        
    }
}
