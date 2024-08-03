using UnityEngine;

public class EndMenuState : IState
{
    public void OnStart()
    {
        ScoreManager.Instance.SaveScore();
        GameUIManager.Instance.OpenEndScreen();
    }

    public void OnEnd()
    {
        
    }
}
