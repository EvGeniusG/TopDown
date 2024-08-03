using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour, IEnemyController
{
    private IEnemyModel model;
    private IPlayerDamageSource damageSource;
    private NavMeshAgent navMeshAgent;
    private Transform playerTransform;
    private IEnemyView enemyView; // Добавляем ссылку на IEnemyView

    private void Start()
    {
        model = GetComponent<IEnemyModel>();
        damageSource = GetComponent<IPlayerDamageSource>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        enemyView = GetComponent<IEnemyView>(); // Инициализируем IEnemyView
        
        // Найти игрока в сцене
        FindPlayer();
        
        navMeshAgent.speed = model.Speed;
    }

    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Перемещаем врага к игроку
            navMeshAgent.speed = model.Speed;
            navMeshAgent.SetDestination(playerTransform.position);
        }
        else{
            navMeshAgent.speed = 0;
            FindPlayer();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Проверяем, что столкновение произошло с игроком
        IPlayerController playerController = other.GetComponent<IPlayerController>();
        if (playerController != null)
        {
            // Наносим урон игроку
            playerController.Hit(damageSource);
        }
    }

    public float GetSpeed()
    {
        return navMeshAgent.speed;
    }

    public void Hit(float damage)
    {
        model.Hit(damage);

        // Проверяем, если здоровье врага упало до нуля или ниже
        if (model.CurrentHP <= 0)
        {
            // Если есть представление врага (например, рэгдолл)
            if (enemyView != null)
            {
                enemyView.CreateRagdoll(); // Создаем рэгдолл
            }

            // Уничтожаем врага
            EnemiesPool.Instance.ReturnEnemy(gameObject, model);
        }
    }

    private void FindPlayer()
    {
        // Найти игрока в сцене
        playerTransform = PlayerManager.Instance.GetPlayer()?.transform;
    }
}
