using System.Collections.Generic;
using UnityEngine;

public class ZonesSpawner : MonoBehaviour
{
    public static ZonesSpawner Instance { get; private set; }

    [SerializeField] Transform areaMin; // Минимальная граница области
    [SerializeField] Transform areaMax; // Максимальная граница области
    [SerializeField] float minDistanceFromPlayer = 10f; // Минимальное расстояние от игрока
    [SerializeField] float minDistanceBetweenObjects = 5f; // Минимальное расстояние между объектами
    [SerializeField] float gridSpacing = 10f; // Расстояние между точками сетки
    [SerializeField] private GameObject[] zonePrefabs; // Массив префабов зон
    [SerializeField] private int[] zoneCounts; // Массив количеств для каждой зоны

    private List<Vector3> availablePositions = new List<Vector3>();
    private List<GameObject> spawnedObjects = new List<GameObject>(); // Список созданных объектов
    private GameObject player;

    private Dictionary<GameObject, int> zones = new Dictionary<GameObject, int>();

    private void Awake()
    {
        Instance = this;
        InitializeZonesDictionary();
        GenerateGrid();
    }

    private void InitializeZonesDictionary()
    {
        if (zonePrefabs.Length != zoneCounts.Length)
        {
            Debug.LogError("The lengths of zonePrefabs and zoneCounts arrays must be equal.");
            return;
        }

        for (int i = 0; i < zonePrefabs.Length; i++)
        {
            zones[zonePrefabs[i]] = zoneCounts[i];
        }
    }

    public void SpawnZones()
    {
        player = PlayerManager.Instance.GetPlayer();

        foreach (var entry in zones)
        {
            GameObject prefab = entry.Key;
            int count = entry.Value;

            for (int i = 0; i < count; i++)
            {
                if (availablePositions.Count == 0)
                {
                    Debug.LogWarning("Нет доступных позиций для спавна объектов.");
                    break;
                }

                Vector3 spawnPosition = Vector3.zero;
                bool positionFound = false;

                while (availablePositions.Count > 0)
                {
                    int index = Random.Range(0, availablePositions.Count);
                    spawnPosition = availablePositions[index];

                    if (IsValidPosition(spawnPosition))
                    {
                        availablePositions.RemoveAt(index);
                        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
                        spawnedObjects.Add(spawnedObject);
                        positionFound = true;
                        break;
                    }
                    else
                    {
                        availablePositions.RemoveAt(index);
                    }
                }

                if (!positionFound)
                {
                    Debug.LogWarning("Не удалось найти подходящую позицию для объекта " + prefab.name);
                }
            }
        }
    }

    private void GenerateGrid()
    {
        float width = areaMax.position.x - areaMin.position.x;
        float height = areaMax.position.z - areaMin.position.z;

        int xCount = Mathf.FloorToInt(width / gridSpacing);
        int zCount = Mathf.FloorToInt(height / gridSpacing);

        for (int x = 0; x <= xCount; x++)
        {
            for (int z = 0; z <= zCount; z++)
            {
                float posX = areaMin.position.x + x * gridSpacing;
                float posZ = areaMin.position.z + z * gridSpacing;
                Vector3 position = new Vector3(posX, 0, posZ);

                availablePositions.Add(position);
            }
        }
    }

    private bool IsValidPosition(Vector3 position)
    {
        // Проверить расстояние до игрока
        if (Vector3.Distance(position, player.transform.position) < minDistanceFromPlayer)
        {
            return false;
        }

        // Проверить расстояние до всех уже размещенных объектов
        foreach (var obj in spawnedObjects)
        {
            if (Vector3.Distance(position, obj.transform.position) < minDistanceBetweenObjects)
            {
                return false;
            }
        }

        return true;
    }
}
