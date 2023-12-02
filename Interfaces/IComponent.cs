using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Interfaces
{
    public interface IComponents
    {
        public GameObject gameObject { get; set; }

        public bool IsEnabled { get; set; }

        public void Enable() => IsEnabled = true;
        public void Disable() => IsEnabled = false;
        public void ToggleEnable() => IsEnabled = !IsEnabled;


        public abstract void Awake();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime);
       
    }
}
