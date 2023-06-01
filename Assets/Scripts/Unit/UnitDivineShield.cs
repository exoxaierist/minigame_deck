using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDivineShield : UnitBase
{
    //함수는 스택 Action은 힙, 따라서 스택쪽에 속한 함수가 더 빠름
    private int moveCount = 0;
    private bool isShield = false;
    protected override void OnMove() //밀렸을때도 움직임 판정?
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
        //공격
    }

    protected override void Awake()
    {
        base.Awake();
    }
}
