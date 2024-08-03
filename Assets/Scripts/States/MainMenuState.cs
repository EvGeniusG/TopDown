using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuState : IState
{
    public async void OnStart()
    {
        await LoadMainMenuAsync();
    }

    private async Task LoadMainMenuAsync(){
        if(SceneManager.GetActiveScene().buildIndex != 0){
            AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(0);
            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }
        }
        MainMenuUIManager.Instance.OpenMainScreen();
    }

    public void OnEnd()
    {
        MainMenuUIManager.Instance.OpenLoadingScreen();
    }
}
