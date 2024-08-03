using UnityEngine;

public class EnemyModel : MonoBehaviour, IEnemyModel, IPlayerDamageSource
{
    [SerializeField] EnemyType type;
    [SerializeField] private float hp = 10f; // Полное здоровье
    [SerializeField] private float speed = 3f; // Скорость врага
    [SerializeField] private int killPoints = 7; // Очки за убийство

    private float currentHP;

    private void Awake()
    {
        currentHP = hp; // Инициализируем текущее здоровье
    }

    public float HP => hp;
    public float CurrentHP => currentHP;
    public float Speed => speed;
    public int KillPoints => killPoints;

    public EnemyType Type => type;


    // Метод для получения урона врагу
    public void Hit(float damage)
    {
        currentHP -= damage;
    }

    public bool IsDamageUnavoidable() => false;
}
