using AdilGame.Logic.inventory;
using AdilGame.Logic.inventory.Items;
using AdilGame.Logic.Weapons;
using AdilGame.Network.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.Logic.Controllers
{
    public class InventoryController : Component
    {
        public int PlayerId { get; set; }
        public Inventory Inventory { get; set; } = new Inventory();
        public Player PlayerComingData { get; set; } = new Player();
        public Player PlayerGoingData { get; set; } = new Player();
        WeaponController WeaponController { get; set; }


        public void PickItem()
        {

        }

        public void DropItem(Iitem iitem)
        {
            Random random = new Random();
            var random1= random.Next(-15, 15);
            var random2= random.Next(-15, 15);
            Inventory.RemoveItem(iitem);
            GameObject ItemGameObejct = new GameObject();
            var item = ItemGameObejct.AddComponentByInterface<Iitem>();
            item.IsDropped = true;
            ItemGameObejct.Transform.Position = gameObject.Transform.Position + new Vector2(random1,random2);
            Core.Instance.GameObjectSystem.AddGameObject(ItemGameObejct);
            

        }

        internal override void Awake()
        {
            WeaponController = gameObject.GetComponent<WeaponController>();

            Inventory.AddItem(WeaponController.EquipedWeapon);

        }

        internal override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.E))
            {
                GameObject s = new GameObject();
                var eq = s.AddComponent<Sword>();

                Inventory.AddItem(eq);
                DropItem(eq);
            }
        }
    }
}
