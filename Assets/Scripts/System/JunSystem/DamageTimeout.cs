using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTimeout : UnitBase
{
    [Tooltip("TimeoutTurn 동안 데미지를 입지 않으면 발동")]
    public int TimeoutTurn = 1;

    //prevTurn - deltaTurn == TimeoutTurn 일때 함수 발동
    //prevTurn: 이전 턴
    //deltaTurn: 마지막 함수 발동 이후로 지난 턴

    private int deltaTurn = 0;
    private int prevTurn = 0;

    //데미지를 받지 않았을때 발동
    protected virtual void OnNoDamage() { }
    void ResetTimer()
    {
        deltaTurn = 0;
    }

    protected override void Awake()
    {
        base.Awake();
        hp.OnDamage += ResetTimer;//데미지를 입는 경우에 타이머 리셋
    }

    void checkTurn()
    {
        if (prevTurn < Global.turnManager.GetTurn())
        {
            prevTurn = Global.turnManager.GetTurn();

            deltaTurn++;
        }
        if (deltaTurn == TimeoutTurn)
        {
            OnNoDamage();
            Debug.Log("nodamage");
            deltaTurn = 0; //함수 발동시 타이머 리셋
        }
    }

    protected void Update()
    {

        checkTurn();

    }
}
