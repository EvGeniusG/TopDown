public interface IEnemyModel
{
    EnemyType Type { get; }
    float HP { get; }
    float CurrentHP { get; }
    float Speed { get; }
    int KillPoints { get; }

    void Hit(float damage);
}
