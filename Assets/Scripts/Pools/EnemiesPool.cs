using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemiesPool : MonoBehaviour
{
    public static EnemiesPool Instance { get; private set;}

    private Dictionary<EnemyType, GameObject> enemiesDic = new Dictionary<EnemyType, GameObject>();
    [SerializeField] List<EnemyType> enemyTypes = new List<EnemyType>();
    [SerializeField] List<GameObject> enemies = new List<GameObject>();

    private Dictionary<EnemyType, Queue<GameObject>> pools = new Dictionary<EnemyType, Queue<GameObject>>();

    private void Awake(){
        Instance = this;
    }
    private void Start()
    {
        InitializeEnemiesDictionary();
        InitializePools();
    }

    private void InitializeEnemiesDictionary()
    {
        // Проверка на совпадение размеров
        if (enemyTypes.Count != enemies.Count)
        {
            Debug.LogError("Количество типов врагов не соответствует количеству префабов.");
            return;
        }

        // Заполнение словаря
        enemiesDic.Clear();
        for (int i = 0; i < enemyTypes.Count; i++)
        {
            if (!enemiesDic.ContainsKey(enemyTypes[i]))
            {
                enemiesDic.Add(enemyTypes[i], enemies[i]);
            }
        }
    }

    private void InitializePools()
    {
        foreach(var type in enemiesDic.Keys){
            pools.Add(type, new Queue<GameObject>());
        }
    }

    public GameObject GetRandomEnemy(Dictionary<EnemyType, float> spawnRates, Vector3 position, Quaternion rotation)
    {
        float totalWeight = 0f;
        foreach (var rate in spawnRates.Values)
        {
            totalWeight += rate;
        }

        float randomValue = UnityEngine.Random.Range(0, totalWeight);
        float accumulatedWeight = 0f;

        foreach (var kvp in spawnRates)
        {
            accumulatedWeight += kvp.Value;
            if (randomValue <= accumulatedWeight)
            {
                return GetEnemy(kvp.Key, position, rotation);
            }
        }

        // Если по какой-то причине ничего не выбрано (не должно случиться), вернем врага по умолчанию
        return GetEnemy(spawnRates.Keys.First(), position, rotation);
    }

    public GameObject GetEnemy(EnemyType type, Vector3 position, Quaternion rotation)
    {
        GameObject enemy = null;
        try{
            if (pools[type].Count > 0)
            {
                enemy = pools[type].Dequeue();
            }
            else
            {
                enemy = Instantiate(enemiesDic[type]);
            }

            if (enemy != null)
            {
                enemy.transform.position = position;
                enemy.transform.rotation = rotation;
                enemy.SetActive(true);
            }
            
        }
        catch(Exception e)
        {
            Debug.LogError(e.ToString());
        }
        return enemy;        
    }

    public void ReturnEnemy(GameObject enemyObject, IEnemyModel enemyModel, bool addScoreForEnemy = true)
    {
        try{
            if(addScoreForEnemy)
            {
                ScoreManager.Instance.AddScore(enemyModel.KillPoints);
            }
            enemyObject.gameObject.SetActive(false);
            pools[enemyModel.Type].Enqueue(enemyObject);
        }
        catch(Exception e)
        {
            Debug.LogError(e.ToString());
        }
    }
}