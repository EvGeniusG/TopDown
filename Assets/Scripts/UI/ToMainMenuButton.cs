using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToMainMenuButton : MonoBehaviour
{
    public void OnPress(){
        GameManager.Instance.SetState(new MainMenuState());
    }
}
