using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAgainButton : MonoBehaviour
{
   public void OnPress(){
    GameManager.Instance.SetState(new GameState(1));
   }
}
