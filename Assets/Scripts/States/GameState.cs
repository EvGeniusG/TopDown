using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class GameState : IState
{

    private int sceneNumber;

    public GameState(int sceneNumber){
        this.sceneNumber = sceneNumber;
    }

    public async void OnStart()
    {
        try
        {
            await LoadGameAsync();
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load game: {ex.Message}");
        }
    }

    private async Task LoadGameAsync()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneNumber); // Номер игровой сцены
        while (!asyncOperation.isDone)
        {
            await Task.Yield();
        }

        PlayerManager.Instance.CreatePlayer(Vector3.zero);
        ZonesSpawner.Instance.SpawnZones();
        LaunchEnemySpawner();
        BonusSpawner.Instance.StartSpawn();
        EquipmentSpawner.Instance.StartSpawn();
        GameUIManager.Instance.OpenGameScreen();

        ScoreManager.Instance.ResetScore();
    }

    void LaunchEnemySpawner(){
        EnemySpawner.Instance.spawnPeriod = 2f;
        EnemySpawner.Instance.StartSpawn();
        EnemySpawner.Instance.SetIncreasingSpawning(0.1f, 10, 0.5f);
    }

    public void OnEnd()
    {
        EnemySpawner.Instance.StopSpawn();
        BonusSpawner.Instance.StopSpawn();
        EquipmentSpawner.Instance.StopSpawn();
    }
}
