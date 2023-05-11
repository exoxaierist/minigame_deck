using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeNoDamage : UnitBase
{
    public int TimeoutTurn = 1;
    private int deltaTurn = 0;
    private int prevTurn = 0;

    protected virtual void OnNoDamageTimeout() { }
    void ResetTimer()
    {
        deltaTurn = 0;
    }

    protected override void Awake()
    {
        base.Awake();
        hp.OnDamage += ResetTimer;
    }
    protected void Update()
    {
        
        if( prevTurn < Global.turnManager.GetTurn()) 
        {
            prevTurn = Global.turnManager.GetTurn();

            deltaTurn++;
        }
        if (deltaTurn == TimeoutTurn)
        {
            OnNoDamageTimeout();
            Debug.Log("nodamage");
            deltaTurn = 0;
        }

    }
}
