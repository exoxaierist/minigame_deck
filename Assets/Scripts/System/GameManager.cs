using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Start()
    {
        StartGame();
    }
    public void StartGame()
    {
        Global.shopManager.OpenShop();
    }
}
