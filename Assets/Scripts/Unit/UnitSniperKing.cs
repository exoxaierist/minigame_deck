using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSniperKing : UnitBase
{
    int damageAdd = 0;
    public override void Kill()
    {
        damageAdd++;
    }

    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        
        AttackInfo info = new() //���� ����
        {
            damage = 1 + damageAdd,
            attacker = this
        };
        for (int i = 0; i < 6; i++)
        {
            Vector2 target = transform.position * Vector2.one + lastMoveDir * (i+1); //������ ��ġ
            Global.atkPooler.Get().Attack(target, info); //����
        }
        
    }
}
