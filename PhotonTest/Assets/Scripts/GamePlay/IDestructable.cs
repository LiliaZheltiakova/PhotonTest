public interface IDestructable
{
    float Health {get; set;}
    void RecieveHit(float getDamage);
    void Die();
}