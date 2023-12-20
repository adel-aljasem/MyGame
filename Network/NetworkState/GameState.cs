using System.Collections.Generic;

public class GameState
{
    public List<PlayerState> Players { get; set; }
    public LevelState CurrentLevel { get; set; }
    public List<EnemyState> Enemies { get; set; }
    public List<ItemState> Items { get; set; }

    public GameState()
    {
        Players = new List<PlayerState>();
        Enemies = new List<EnemyState>();
        Items = new List<ItemState>();
    }

    public void Update()
    {

    }
}
