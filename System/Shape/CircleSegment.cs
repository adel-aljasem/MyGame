using Microsoft.Xna.Framework;
using System.Collections.Generic;

public class CircleSegment
{
    public Vector2 Start { get; set; }
    public Vector2 End { get; set; }
    public List<CircleSegment> circleSegments = new List<CircleSegment>();

}

