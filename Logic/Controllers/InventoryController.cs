using AdilGame.Logic.inventory;
using AdilGame.Logic.inventory.Items;
using AdilGame.Logic.Weapons;
using AdilGame.Network.Data;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PandaGameLibrary.Components;
using PandaGameLibrary.System;
using System;

namespace AdilGame.Logic.Controllers
{
    public class InventoryController : Component
    {
        public int PlayerId { get; set; }
        public Inventory Inventory { get; set; } = new Inventory();
        public Player PlayerComingData { get; set; } = new Player();
        public Player PlayerGoingData { get; set; } = new Player();
        WeaponController WeaponController { get; set; }


        public void PickItem(GameObject gameObject)
        {
            var item = gameObject.GetComponentByInterface<Iitem>();
            if (item != null && Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                Inventory.AddItem(item);
                var go = Core.Instance.GameObjectSystem.GetGameObjectById(item.gameObject.GameObjectId);
                Core.Instance.GameObjectSystem.RemoveGameObject(go);
            }
        }

        public void DropItem(Iitem iitem)
        {
            Random random = new Random();
            var random1 = random.Next(-15, 15);
            var random2 = random.Next(-15, 15);
            Inventory.RemoveItem(iitem);
            iitem.IsDropped = true;
            iitem.gameObject.Transform.Position = gameObject.Transform.Position + new Vector2(random1, random2);
            Core.Instance.GameObjectSystem.AddGameObject(iitem.gameObject);


        }

        public override void Awake()
        {
            WeaponController = gameObject.GetComponent<WeaponController>();

            Inventory.AddItem(WeaponController.EquipedWeapon);

        }
        private KeyboardState _previousKeyboardState;

        public override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.E))
            {
                GameObject s = new GameObject();
                var eq = s.AddComponent<Sword>();

                Inventory.AddItem(eq);
                DropItem(eq);
            }
            KeyboardState currentKeyboardState = Keyboard.GetState();
            if (currentKeyboardState.IsKeyDown(Keys.B) && !_previousKeyboardState.IsKeyDown(Keys.B))
            {
                Game1.Instance.UIManager.CreateInventoryWindow(Inventory.items);
                Game1.Instance.UIManager._inventoryWindow.Visible = true;

            }

            _previousKeyboardState = currentKeyboardState; // Update the previous state
        }
    }
}
