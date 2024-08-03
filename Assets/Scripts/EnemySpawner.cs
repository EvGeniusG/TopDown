using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner Instance { get; private set; }

    public float spawnPeriod;
    [SerializeField] private Dictionary<EnemyType, float> spawnRates = new Dictionary<EnemyType, float>
    {
        { EnemyType.Regular, 0.6f },
        { EnemyType.Fast, 0.3f },
        { EnemyType.Armored, 0.1f }
    };

    private Coroutine spawnIncreasingRoutine;
    private Coroutine spawningRoutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartSpawn()
    {
        StopSpawn();
        spawningRoutine = StartCoroutine(SpawnEnemies());
    }

    public void StopSpawn()
    {
        if(spawningRoutine != null) StopCoroutine(spawningRoutine);
    }

    public void SetIncreasingSpawning(float step, float period, float target)
    {
        if(spawnIncreasingRoutine != null) StopCoroutine(spawnIncreasingRoutine);
        spawnIncreasingRoutine = StartCoroutine(spawnIncreasing(step, period, target));
    }

    IEnumerator spawnIncreasing(float step, float period, float target)
    {
        while(spawnPeriod >= target){
            yield return new WaitForSeconds(period);
            spawnPeriod -= step;
        }
        spawnPeriod = target;
    }

    private IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnPeriod);

            Vector3? spawnPosition = PositionGenerator.Instance.GetRandomPositionOutsideCameraView();
            if(spawnPosition != null)
                EnemiesPool.Instance.GetRandomEnemy(spawnRates, spawnPosition!.Value, Quaternion.identity);
        }
    }
}
