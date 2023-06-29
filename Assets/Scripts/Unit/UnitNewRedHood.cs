using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNewRedHood : UnitBase
{
    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        Vector2 target = transform.position * Vector2.one + lastMoveDir; //������ ��ġ
        AttackInfo info = new() //���� ����
        {
            damage = 1,
            attacker = this
        };

        Global.atkPooler.Get().Attack(target, info); //����
        if(player == Player.Player1)
        {
            if (Global.shopManager.p1Coins <= 10)
            {
                Global.atkPooler.Get().Attack(target, info); //����
            }
        }
        else if (player == Player.Player2)
        {
            if (Global.shopManager.p2Coins <= 10)
            {
                Global.atkPooler.Get().Attack(target, info); //����
            }
        }
    }
}
