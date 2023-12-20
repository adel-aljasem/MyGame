using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.inventory.Items
{
    public interface Iitem
    {
        public int Id { get; set; }
        public GameObject gameObject { get; set; }
        public ColliderComponent Collider { get; set; }
        public string PlayerId { get; set; }
        public Texture2D ItemTexture { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanBeDropped { get; set; }
        public bool IsDropped { get; set; }
    }
}
