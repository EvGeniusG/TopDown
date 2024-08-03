using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    [SerializeField] private GameObject playerControllerPrefab; // Префаб игрока
    private GameObject playerInstance; // Экземпляр игрока
    private IPlayerModel playerModel; // Модель игрока

    private void Awake()
    {
        Instance = this;
    }

    // Создание игрока в указанной позиции
    public GameObject CreatePlayer(Vector3 position)
    {
        if (playerInstance != null)
        {
            Debug.LogWarning("Player already exists.");
            return playerInstance;
        }

        playerInstance = Instantiate(playerControllerPrefab, position, Quaternion.identity);
        playerModel = playerInstance.GetComponent<IPlayerModel>(); // Получаем модель игрока
        return playerInstance;
    }

    // Получение текущего игрока
    public GameObject GetPlayer()
    {
        return playerInstance;
    }

    // Получение модели игрока
    public IPlayerModel GetPlayerModel()
    {
        return playerModel;
    }

    // Уничтожение текущего игрока
    public void DestroyPlayer()
    {
        if (playerInstance != null)
        {
            Destroy(playerInstance);
            playerInstance = null; // Устанавливаем в null после уничтожения
            playerModel = null; // Устанавливаем в null после уничтожения
        }
    }
}
