using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdilGame.UI
{
    public class UIManager
    {
        public Grid _inventoryGrid;
        private Desktop _desktop;
        public List<Texture2D> Texture2Ds { get; set; } = new List<Texture2D>();

        public UIManager()
        {
            CreateInventoryUI();
            _desktop = new Desktop();
            _desktop.Root = _inventoryGrid;

        }

        private void CreateInventoryUI()
        {
            _inventoryGrid = new Grid
            {
                RowSpacing = 8,
                ColumnSpacing = 8
            };

            // Define rows and columns
            _inventoryGrid.ColumnsProportions.Add(new Proportion(ProportionType.Part));
            _inventoryGrid.RowsProportions.Add(new Proportion(ProportionType.Part));
            // Add more rows/columns as needed

            // Add item slots (buttons or images) to the grid
            var itemButton = new ImageButton 
            {
                
            };
            itemButton.Background = new SolidBrush(Color.Red);
    

            // Set properties of the itemButton
            _inventoryGrid.Widgets.Add(itemButton);

            var longButton = new TextButton();
            longButton.Text = "Long Button";
            Grid.SetColumn(longButton, 3);
            Grid.SetRow(longButton, 2);
            _inventoryGrid.Widgets.Add(longButton);


            var veryLongButton = new TextButton();
            veryLongButton.Text = "Very Long Button";
            Grid.SetColumn(veryLongButton, 8);
            _inventoryGrid.Widgets.Add(veryLongButton);

            // Repeat for other inventory slots
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime,SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _desktop.Render();
            spriteBatch.End();
        }


    }
}
