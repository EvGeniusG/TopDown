using UnityEngine;

public class GameUIManager : MonoBehaviour
{

    public static GameUIManager Instance { get; private set;}
    [SerializeField] GameObject GameScreen;
    [SerializeField] GameObject EndScreen;

    void Awake(){
        Instance = this;
    }


    public void OpenGameScreen(){
        GameScreen.SetActive(true);
        EndScreen.SetActive(false);
    }

    public void OpenEndScreen(){
        EndScreen.SetActive(true);
        GameScreen.SetActive(false);
    }
}
