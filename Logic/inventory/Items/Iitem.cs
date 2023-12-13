using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.inventory.Items
{
    public interface Iitem
    {
        public int Id { get; set; }
        public string PlayerId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool CanBeDropped { get; set; }
    }
}
