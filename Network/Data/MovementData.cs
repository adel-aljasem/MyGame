using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Network.Data
{
    [MessagePackObject]
    public class MovementData
    {
        [Key(0)]
        public int IdPlayer { get; set; }
        [Key(1)]
        public float PositionX { get; set; }
        [Key(2)]
        public float PositionY { get; set; }
        [Key(3)]
        public short VelocityX { get; set; }
        [Key(4)]
        public short VelocityY { get; set; }
        [Key(5)]
        public byte State { get; set; }
    }
}
