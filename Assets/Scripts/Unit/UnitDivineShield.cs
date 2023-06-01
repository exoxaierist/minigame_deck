using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDivineShield : UnitBase
{
    //�Լ��� ���� Action�� ��, ���� �����ʿ� ���� �Լ��� �� ����
    private int moveCount = 0;
    private bool isShield = false;
    protected override void OnMove() //�з������� ������ ����?
    {
        if (moveCount < 2) moveCount++;
        else
        {
            moveCount = 0;
            isShield = true;
        }
    }
    public override void ReceivePayload(AttackInfo _info)
    {
        if (isShield) isShield = false;
        else hp.AddToHP(-_info.damage);
    }
    protected override void Attack()
    {
        //����
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
