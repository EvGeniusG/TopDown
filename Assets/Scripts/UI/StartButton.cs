using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    public void Press(){
        GameManager.Instance.SetState(new GameState(1));
    }
}
