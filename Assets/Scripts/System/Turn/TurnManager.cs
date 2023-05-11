using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public bool turnActive = true;
    public float turnDuration = 1;
    private float turnTimer = 0;
    public float GetTurnTimer() { return turnTimer; }
    private int turn = 1;
    public int GetTurn() { return turn; }

    private bool p1Input = false;
    private bool p2Input = false;

    private void Awake()
    {
        Global.turnManager = this;
        Global.OnP1Any += CheckP1Input;
        Global.OnP2Any += CheckP2Input;

        turn = 1;
    }

    private void Update()
    {
        if (turnActive) 
        {
            if (turnTimer == 0) turn++;

            turnTimer += Time.deltaTime; 
        }

        if (p1Input && p2Input)
        {
            Global.OnTurnStart?.Invoke();
            turnTimer = 0;
        }
        else if (turnTimer >= turnDuration)
        {
            if (!p1Input) Global.OnP1NoInput?.Invoke();
            if (!p2Input) Global.OnP2NoInput?.Invoke();
            p1Input = false;
            p2Input = false;

            Global.OnTurnStart?.Invoke();
            turnTimer = 0;
        }
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
