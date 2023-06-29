using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNewRedHood : UnitBase
{
    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        Vector2 target = transform.position * Vector2.one + lastMoveDir; //공격할 위치
        AttackInfo info = new() //공격 정보
        {
            damage = 1,
            attacker = this
        };

        Global.atkPooler.Get().Attack(target, info); //공격
        if(player == Player.Player1)
        {
            if (Global.shopManager.p1Coins <= 10)
            {
                Global.atkPooler.Get().Attack(target, info); //공격
            }
        }
        else if (player == Player.Player2)
        {
            if (Global.shopManager.p2Coins <= 10)
            {
                Global.atkPooler.Get().Attack(target, info); //공격
            }
        }
    }
}
