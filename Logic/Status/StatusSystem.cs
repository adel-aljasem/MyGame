using AdilGame.Interfaces;
using AdilGame.Logic.inventory.Items;
using Microsoft.Xna.Framework;
using PandaGameLibrary.Components;
using PandaGameLibrary.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Status
{
    public class StatusSystem
    {
        public static StatusSystem instance { get; set; } = new StatusSystem();
        private void SetStatusForAll()
        {
            var St = Core.Instance.GameObjectSystem.GetAllGameObjects();
            // Create a copy of the items list to avoid InvalidOperationException
            var StCopy = new List<GameObject>(St);

            foreach (var status in StCopy)
            {
                var existStatus = status.GetComponentByInterface<IDamageable>();
                if (existStatus != null)
                {
                    ApplyStatusBasedOnRank(existStatus);
                }
            }
        }


        public void ApplyStatusBasedOnRank(IDamageable damageable)
        {
            damageable.MainStatus = new MainStatus();
            int maxRank = 10; // Example: set this to the maximum rank level you have
            for (int i = 1; i <= maxRank; i++)
            {
                if (damageable.RankLevel == i)
                {
                    damageable.MainStatus.Health += 10 * i; // Multiply by rank level
                    damageable.MainStatus.MovementSpeed += 50 * i;
                    damageable.MainStatus.MagicDefense += 5 * i;
                    damageable.MainStatus.MagicDamage += 5 * i;
                    damageable.MainStatus.PhysicDefense += 5 * i;
                    damageable.MainStatus.PhysicDamage += 5 * i;
                    damageable.MainStatus.Dodge += 2 * i;
                    damageable.MainStatus.Critical += 2 * i;
                    damageable.MainStatus.Haste += 2 * i;
                    break; // Exit the loop once the matching rank is found and updated
                }
            }
        }

    }
}
