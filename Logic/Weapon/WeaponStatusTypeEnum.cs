using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Weapons
{
    public enum WeaponStatusTypeEnum
    {
        MagicDefense,
        PhysicalDefense,
        MagicalPower,
        PhysicalPower,
        Critical,
        MovementSpeed,
        Haste
        // Add other statuses as needed
    }
    public enum WeaponState
    {
        None,
        Attacking,
        Dropped
    }

}
