using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSample : UnitBase
{
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Attack()
    {
        if (turnCount <= 0) return;
        turnCount--;
        Vector2 target = transform.position * Vector2.one + lastMoveDir; //공격할 위치
        AttackInfo info = new() //공격 정보
        {
            damage = 1,
            attacker = this
        };

        Global.atkPooler.Get().Attack(target, info); //공격
    }
}
