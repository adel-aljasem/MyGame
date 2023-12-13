using AdilGame.Logic.Weapons.bullet;

public class BulletFactory
{
    public static Bullet CreateBullet<T>() where T : Bullet, new()
    {
        return new T(); // Creates a new instance of the bullet type
    }
}
