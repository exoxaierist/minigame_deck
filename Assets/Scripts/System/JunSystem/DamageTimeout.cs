using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTimeout : UnitBase
{
    [Tooltip("TimeoutTurn ���� �������� ���� ������ �ߵ�")]
    public int TimeoutTurn = 1;

    //prevTurn - deltaTurn == TimeoutTurn �϶� �Լ� �ߵ�
    //prevTurn: ���� ��
    //deltaTurn: ������ �Լ� �ߵ� ���ķ� ���� ��

    private int deltaTurn = 0;
    private int prevTurn = 0;

    
    protected virtual void OnNoDamage() { } //�������� ���� �ʾ����� �ߵ�
    void ResetTimer()
    {
        deltaTurn = 0;
    }

    protected override void Awake()
    {
        base.Awake();
        hp.OnDamage += ResetTimer;//�������� �Դ� ��쿡 Ÿ�̸� ����
        Global.OnTurnStart += CheckTurn;
    }

    void CheckTurn()
    {
        deltaTurn++;
  
        if (deltaTurn >= TimeoutTurn)
        {
            OnNoDamage();
            Debug.Log("nodamage");
            //deltaTurn = 0; //�Լ� �ߵ��� Ÿ�̸� ����
        }
    }
}
