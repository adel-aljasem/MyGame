using AdilGame.Logic.inventory.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.inventory
{
    public class Inventory
    {

        private List<Iitem> items = new List<Iitem>();


        public void AddItem(Iitem item)
        {
            items.Add(item);
            // Additional logic for adding an item
        }

        public void RemoveItem(Iitem item)
        {
            items.Remove(item);
            // Additional logic for removing an item
        }
    }


  

}
