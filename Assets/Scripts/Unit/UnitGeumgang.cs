using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeumgang : UnitBase
{
    protected override void Awake()
    {
        base.Awake();
        hp.OnDamage += onDamage;
    }
    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;

        AttackInfo info = new() //공격 정보
        {
            damage = 1 + addattack,
            attacker = this
        };
        for (int i = 0; i < 2; i++)
        {
            Vector2 target = transform.position * Vector2.one + lastMoveDir * (i + 1); //공격할 위치
            Global.atkPooler.Get().Attack(target, info); //공격
        }

    }
    int addattack = 0;
    void onDamage(UnitBase unit)
    {
        addattack++;
    }
}
