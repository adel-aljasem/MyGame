using MessagePack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Network.Data
{
    [MessagePackObject]
    public class WeaponData
    {
        [Key(1)]
        public int Id { get; set; }
        [Key(2)]
        public int PlayerId { get; set; }
        [Key(3)]
        public WeaponTypeEnum weaponType { get; set; }
        [Key(4)]
        public int Level { get; set; }
        [Key(5)]
        public int PlusHealth { get; set; }
        [Key(6)]
        public int Damage { get; set; } = 10;
        [Key(7)]
        public int BulletSpeed { get; set; } = 5;
        [Key(8)]
        public int LifeTime { get; set; } = 100;
        [Key(9)]
        public float FireCooldownDuration { get; set; } = 0.00001f;
        [Key(10)]
        public bool Fire { get; set; }
    }
}
