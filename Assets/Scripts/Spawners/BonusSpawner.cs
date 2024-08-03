using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    public static BonusSpawner Instance {get; private set;}
    [SerializeField] private float spawnInterval = 5f; // Интервал между спавнами бонусов

    private void Awake()
    {
        Instance = this;
    }

    public void StartSpawn()
    {
        InvokeRepeating(nameof(SpawnRandomBonus), spawnInterval, spawnInterval);
    }
    public void StopSpawn()
    {
        CancelInvoke(nameof(SpawnRandomBonus));
    }

    private void SpawnRandomBonus()
    {
        Vector3? randomPosition = PositionGenerator.Instance.GetRandomPositionInsideCameraView();
        
        if (randomPosition != null) // Проверка, что была найдена валидная позиция
        {
            BonusPool.Instance.SetRandomBonus(randomPosition!.Value);
        }
        else
        {
            Debug.LogWarning("Не удалось найти подходящую позицию для спавна бонуса.");
        }
    }
}
