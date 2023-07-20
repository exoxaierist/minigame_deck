using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public bool counterActive = false;
    public float turnDuration = 1;
    private float turnTimer = 0;
    public float GetTurnTimer() { return turnTimer; }
    private int turn = 1;
    public int GetTurn() { return turn; }

    private bool p1Input = false;
    private bool p2Input = false;

    [SerializeField] private Image timer;

    private void Awake()
    {
        Global.turnManager = this;
        Global.OnP1Any += CheckP1Input;
        Global.OnP2Any += CheckP2Input;
        Global.OnTurnStart += AddTurn;
        Global.OnRoundStart += StartTurnCounting;
        Global.OnRoundEnd += StopTurnCounting;
    }

    private void Update()
    {
        if (!counterActive) return;
        // 턴 타이머 카운팅
        turnTimer += Time.deltaTime;
        timer.fillAmount = turnTimer / turnDuration;
        
        // 플레이어 행동했는지, 행동 안했는지 체크
        if (p1Input && p2Input)
        {
            p1Input = false;
            p2Input = false;
            turnTimer = 0;
            Global.OnTurnStart?.Invoke();
            Global.OnTurnStartLate?.Invoke();
        }
        else if (turnTimer >= turnDuration)
        {
            if (!p1Input) Global.OnP1NoInput?.Invoke();
            if (!p2Input) Global.OnP2NoInput?.Invoke();
            p1Input = false;
            p2Input = false;

            turnTimer = 0;
            Global.OnTurnStart?.Invoke();
            Global.OnTurnStartLate?.Invoke();
        }
    }

    private void AddTurn()
    {
        turn += 1;
    }

    private void StartTurnCounting()
    {
        counterActive = true;
    }

    private void StopTurnCounting()
    {
        counterActive = false;
    }

    private void CheckP1Input()
    {
        p1Input = true;
    }

    private void CheckP2Input()
    {
        p2Input = true;
    }
}
