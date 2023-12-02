using Microsoft.Xna.Framework;

namespace AdilGame.Network;
public class NetworkComponent : Component
{
    // Network related data here
    public Vector2 LastKnownPosition { get; set; }
    public PlayerInput LastInput { get; set; }
    public bool HasChanges { get; set; }
}
