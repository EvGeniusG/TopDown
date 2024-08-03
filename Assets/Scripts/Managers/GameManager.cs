using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    private IState currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Начальное состояние - главное меню
        SetState(new MainMenuState());
    }

    public void SetState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnEnd();
        }

        currentState = newState;
        currentState.OnStart();
    }
}
