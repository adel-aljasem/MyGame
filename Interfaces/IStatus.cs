using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Interfaces
{
    public interface IStatus
    {
        public int RankLevel { get; set; }
        public int Health { get; set; }
        public int Speed { get; set; }
        public CharcaterStatu State { get; set; }
        public void DmgTaken(int dmg);
        public void HealthIncress(int healthAmount);
    }
}
