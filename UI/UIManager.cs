using AdilGame.Logic.inventory.Items;
using AdilGame.Logic.Weapons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Myra.Graphics2D;
using Myra.Graphics2D.Brushes;
using Myra.Graphics2D.TextureAtlases;
using Myra.Graphics2D.UI;
using PandaGameLibrary.Components;
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
        public Window _inventoryWindow;
        public Desktop _desktop;
        private VerticalStackPanel _tooltip;

        public List<Texture2D> Texture2Ds { get; set; } = new List<Texture2D>();

        public UIManager()
        {
            _desktop = new Desktop();
            //_inventoryWindow.Visible = false; // Initially not visible
        }
        public void CreateInventoryWindow(List<Iitem> iitems)
        {
            var panel = new Panel
            {
                Width = Game1.Instance.graphics.PreferredBackBufferWidth,
                Height = Game1.Instance.graphics.PreferredBackBufferHeight,
            };

            _inventoryWindow = new Window
            {
                Title = "Inventory",
                Width = 500,
                Height = 700,
            };
            var scrollViewer = new ScrollViewer
            {
                Width = 480, // Adjust as needed
                Height = 680, // Adjust as needed
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch
            };

            var grid = new Grid
            {
                RowSpacing = 8,
                ColumnSpacing = 8,
                ShowGridLines = true,
            };

            _tooltip = CreateTooltipContainer();
            _tooltip.Visible = false; // Initially hidden

            // Initialize the grid with 8 rows and 6 columns
            const int initialRows = 8;
            const int columns = 6;

            for (int row = 0; row < initialRows; row++)
            {
                grid.RowsProportions.Add(new Proportion(ProportionType.Part, 1));
            }

            for (int col = 0; col < columns; col++)
            {
                grid.ColumnsProportions.Add(new Proportion(ProportionType.Part, 1));
            }

            for (int i = 0; i < iitems.Count; i++)
            {
                var item = iitems[i];
                int row = i / columns;
                int col = i % columns;

                // Add new row if needed
                if (row >= initialRows && grid.RowsProportions.Count <= row)
                {
                    grid.RowsProportions.Add(new Proportion(ProportionType.Part, 1));
                }
                var render2d = item.gameObject.GetComponent<Render2D>();
                var weapon = item.gameObject.GetComponent<Weapon>();
                if (render2d != null && render2d.Texture != null && weapon !=null)
                {
                    var button = new ImageButton
                    {
                        Width = 64,
                        Height = 64,
                        Image = new TextureRegion(render2d.Texture, render2d.SourceRectangle),
                        ContentHorizontalAlignment = HorizontalAlignment.Center,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        ImageHeight = 64,
                        ImageWidth = 64,
                    };
                    Console.WriteLine($"Button bounds after setup: {button.Bounds}");


                    button.MouseEntered += (s, a) => 
                    {
                       

                        _tooltip.Visible = true;
                        _tooltip.Left = 30 ; 
                        _tooltip.Top = button.Bounds.Top;
                        Point globalPosition = GetGlobalButtonPosition(button);
                        Console.WriteLine($"Global position: {globalPosition.X}, {globalPosition.Y}");

                    };
                   
                    button.MouseLeft += (s, a) =>
                    {
                        _tooltip.Visible = false;
                    };
                    Grid.SetColumn(button, col);
                    Grid.SetRow(button, row);
                    grid.Widgets.Add(button);
                }
            }
            // Add the grid to the scroll viewer
            scrollViewer.Content = grid;

            // Set the scroll viewer as the content of the window
            _inventoryWindow.Content = scrollViewer;
            panel.Widgets.Add(_tooltip);
            panel.Widgets.Add(_inventoryWindow);
            _inventoryWindow.Closing += (sender, args) =>
            {
                args.Cancel = true; // Prevent the window from closing
                _inventoryWindow.Visible = false; // Hide the window
            };
            _desktop.Root = panel;

        }
        private VerticalStackPanel CreateTooltipContainer()
        {
            var tooltipContainer = new VerticalStackPanel
            {
                Spacing = 0,
                Width = 300,
                Height = 300,
                Background = new SolidBrush(Color.Black)
            };

            // Example: Adding two lines with different colors
            var line1 = new Label
            {
                Text = "Line 1",
                TextColor = Color.Red,
            };
            tooltipContainer.Widgets.Add(line1);

            var line2 = new Label
            {
                Text = "Line 2",
                TextColor = Color.Blue,

            };
            tooltipContainer.Widgets.Add(line2);

            return tooltipContainer;
        }

        private Point GetGlobalButtonPosition(ImageButton button)
        {
            // Start with the button's local position
            Point position = new Point(button.Bounds.X, button.Bounds.Y);

            // Traverse up the parent hierarchy, adding each parent's position
            var currentWidget = button.Parent;
            while (currentWidget != null)
            {
                position.X += currentWidget.Bounds.X;
                position.Y += currentWidget.Bounds.Y;

                // Move to the next parent
                currentWidget = currentWidget.Parent;
            }

            return position;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            _desktop.Render();
            spriteBatch.End();
        }


    }
}
