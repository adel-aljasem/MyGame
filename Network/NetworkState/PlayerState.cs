using Microsoft.Xna.Framework;

public class PlayerState
{
    public string PlayerId { get; set; }
    public Vector2 Position { get; set; }
    public int Health { get; set; }
    // Add other player-specific properties such as inventory, status effects, etc.
}
