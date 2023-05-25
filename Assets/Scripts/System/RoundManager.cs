using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : MonoBehaviour
{
    public bool roundOver = false;
    public int p1Score = 0;
    public int p2Score = 0;

    private void Awake()
    {
        Global.roundManager = this;
        Global.OnP1Win += OnP1Win;
        Global.OnP2Win += OnP2Win;
    }

    private void OnP1Win()
    {
        if (roundOver) return;
        p1Score++;
        RoundOver();
    }

    private void OnP2Win()
    {
        if (roundOver) return;
        p2Score++;
        RoundOver();
    }

    private void RoundOver()
    {
        roundOver = true;
        Global.unitManager.ResetAllUnits();
        Global.shopManager.OpenShop();
    }

    public void StartRound()
    {
        Global.shopManager.CloseShop();
        roundOver = false;
    }
}
