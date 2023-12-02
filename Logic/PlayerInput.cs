using Microsoft.Xna.Framework;

public class PlayerInput
{
    public Vector2 MovementDirection { get; set; }
    public bool IsAttacking { get; set; }
    public Vector2 MousePosition { get; set; }
    // Other fields as needed

    // You can override the Equals method to simplify comparison
    public override bool Equals(object obj)
    {
        if (obj is PlayerInput other)
        {
            return MovementDirection == other.MovementDirection
                   && IsAttacking == other.IsAttacking
                   && MousePosition == other.MousePosition;
        }
        return false;
    }

    public override int GetHashCode()
    {
        unchecked // Overflow is fine, just wrap
        {
            int hash = 17;
            // Suitable prime numbers for combining hash codes
            hash = hash * 23 + MovementDirection.GetHashCode();
            hash = hash * 23 + IsAttacking.GetHashCode();
            hash = hash * 23 + MousePosition.GetHashCode();
            return hash;
        }
    }
}
