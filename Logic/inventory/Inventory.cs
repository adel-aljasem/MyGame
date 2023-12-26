using AdilGame.Logic.inventory.Items;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.inventory
{
    public class Inventory
    {

        public List<Iitem> items = new List<Iitem>();
        private List<Iitem> droppableItems = new List<Iitem>(); // Cache for droppable items


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

        public Iitem GetItem(int ItemId)
        {
            return items.FirstOrDefault(w=>w.Id == ItemId);
        }

       

    }




}
