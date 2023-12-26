using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Status
{
    public interface IMainStatus
    {
        public int Health { get; set; }
        public int MovementSpeed { get; set; }
        public int MagicDamage { get; set; }
        public int MagicDefense { get; set; }
        public int PhysicDamage { get; set; }
        public int PhysicDefense { get; set; }
        public int Haste { get; set; }
        public int Critical { get; set; }
        public int Dodge { get; set; }
    }
}
