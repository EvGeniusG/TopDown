using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{

    public static MainMenuUIManager Instance { get; private set;}
    [SerializeField] GameObject MainScreen;
    [SerializeField] GameObject LoadingScreen;

    void Awake(){
        Instance = this;
    }


    public void OpenMainScreen(){
        MainScreen.SetActive(true);
        LoadingScreen.SetActive(false);
    }

    public void OpenLoadingScreen(){
        LoadingScreen.SetActive(true);
        MainScreen.SetActive(false);
    }
}
