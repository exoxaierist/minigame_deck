using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 라운드 (몇대몇인지) 관리하는거
public class RoundManager : MonoBehaviour
{
    public bool roundActive = false;
    public int currentRound = 0;
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
        if (!roundActive) return;
        p1Score++;
        RoundOver();
    }

    private void OnP2Win()
    {
        if (!roundActive) return;
        p2Score++;
        RoundOver();
    }

    private void RoundOver()
    {
        if (!roundActive) return;

        roundActive = false;
        Global.OnRoundEnd?.Invoke();
        Global.unitManager.ResetAllUnits();
        Global.shopManager.OpenShop();
    }

    public void StartRound()
    {
        print("roundStart");
        if (roundActive) return;
        currentRound++;
        roundActive = true;
        Global.OnRoundStart?.Invoke();
    }
}
