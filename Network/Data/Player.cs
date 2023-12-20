using AdilGame.Network.Data;
using MessagePack;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace AdilGame.Network.Data;
[MessagePackObject]
public class Player 
{
    [Key(0)]
    public int Id { get; set; }
    [Key(1)]
    public string OnlineID { get; set; }
    [Key(2)]
    public string Name { get; set; }
    [Key(3)]
    public MovementData MovementData { get; set; } = new MovementData();
    [Key(4)]
    public MouseData MouseData { get; set; } = new MouseData();
    [Key(5)]
    public WeaponData weaponData { get; set; } = new WeaponData();
    // Add other player-related properties as needed

}
