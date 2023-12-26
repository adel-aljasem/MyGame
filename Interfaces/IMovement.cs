using Microsoft.Xna.Framework;

namespace AdilGame.Interfaces
{
    public interface IMovement 
    {
        public int Speed { get; set; }
        public int SpeedMultiplier { get; set; }
        public void Move(GameTime gameTime);
        public void Slow(int slowAmount);
        
    }
}
