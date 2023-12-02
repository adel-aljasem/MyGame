using Myra.Graphics2D.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using AdilGame;
using AdilGame.Network;

public class PlayerNameInputUI
{
    private Desktop _desktop;
    private TextBox _nameTextBox;
    private TextButton _submitButton;

    public string PlayerName => _nameTextBox.Text;

    public PlayerNameInputUI()
    {
        _desktop = new Desktop();

        var grid = new Grid
        {
            RowSpacing = 8,
            ColumnSpacing = 8
        };
        
        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.ColumnsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));
        grid.RowsProportions.Add(new Proportion(ProportionType.Auto));

        _nameTextBox = new TextBox
        {
            Id = "nameTextBox",
            Width = 200
        };
        Grid.SetColumn(_nameTextBox, 0);
        Grid.SetRow(_nameTextBox, 0);
        grid.Widgets.Add(_nameTextBox);

        _submitButton = new TextButton
        {
            Text = "Submit"
        };
        Grid.SetColumn(_submitButton, 1);
        Grid.SetRow(_submitButton, 0);

        _submitButton.Click += async (s, a) =>
        {
            // Handle the submit button click, e.g., get the entered text.
            string enteredText = PlayerName;
            //await PlayerNetworkManager.Instance.ConnectServer(enteredText);
            // Do something with the entered text.
        };

        grid.Widgets.Add(_submitButton);
        _desktop.Root = grid;
        grid.HorizontalAlignment = HorizontalAlignment.Center;
        grid.VerticalAlignment = VerticalAlignment.Center;
    }

    public void Update(GameTime gameTime)
    {
    }

    public void Draw()
    {
        _desktop.Render();
    }
}
