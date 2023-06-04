using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitM : UnitBase
{
    int additionalCoin = 2;
    protected override void OnDeath(UnitBase unit) 
    {
        if (player == Player.Player1)
        {
            Global.shopManager.ChangeP1Coin(additionalCoin);
        }
        else if (player == Player.Player2)
        {
            Global.shopManager.ChangeP2Coin(additionalCoin);
        }
    }

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
    }
}
