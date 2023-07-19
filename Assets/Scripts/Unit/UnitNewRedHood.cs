using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNewRedHood : UnitBase
{
    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        AttackInfo info = attackInfo;
        if (additionalDamage != 0)
        {
            attackInfo.damage += additionalDamage;
        }
        
        foreach (Vector2 target in Global.RotateAttackPattern(attackPattern, lastMoveDir))
        {
            Global.atkPooler.Get().Attack(target + transform.position * Vector2.one, attackInfo);

            //추가공격
            if (player == Player.Player1)
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
        attackInfo = info;

        
        
    }
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { new(1, 0) }, new() { damage = 1, attacker = this }, 1);
    }
}
