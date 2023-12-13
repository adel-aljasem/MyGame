using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Network.Data
{
    public interface IWeaponData
    {
        public int Id { get; set; }
        public WeaponTypeEnum WeaponTypeenum { get; protected set; }
        public string PlayerId { get; set; }
        public int Level { get; set; }
        public int PlusHealth { get; set; }
        public int Damage { get; set; } 
        public int BulletSpeed { get; set; } 
        public int LifeTime { get; set; }
        public float FireCooldownDuration { get; set; } 

    }
}
