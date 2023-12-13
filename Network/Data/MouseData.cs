using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Network.Data
{
    [MessagePackObject]
    public class MouseData
    {
        [Key(0)]
        public int Id { get; set; }
        [Key(1)]
        public float MouseX { get; set; }
        [Key(2)]
        public float MouseY { get; set; }
    }

}
