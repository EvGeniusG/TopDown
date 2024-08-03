using UnityEngine;

public class EquipmentSpawner : MonoBehaviour
{
    public static EquipmentSpawner Instance { get; private set; }

    [SerializeField] private float spawnInterval = 10f; // Интервал спавна

    private void Awake()
    {
        Instance = this;
    }

    public void StartSpawn()
    {
        InvokeRepeating(nameof(SpawnRandomEquipment), spawnInterval, spawnInterval);
    }
        public void StopSpawn()
    {
        CancelInvoke(nameof(SpawnRandomEquipment));
    }

    private void SpawnRandomEquipment()
    {
        Vector3? spawnPosition = PositionGenerator.Instance.GetRandomPositionInsideCameraView();
        if(spawnPosition != null)
            EquipmentPool.Instance.SetRandomEquipment(spawnPosition!.Value);
    }


}
