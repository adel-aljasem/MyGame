public interface IPlayerStatus
{
    public string Id { get; set; }
    public string OnlineID { get; set; }
    public string Name { get; set; }
    public float X { get; set; }
    public float Y { get; set; }
    public float MouseX { get; set; }
    public float MouseY { get; set; }
    public CharcaterStatu State { get; set; }

}