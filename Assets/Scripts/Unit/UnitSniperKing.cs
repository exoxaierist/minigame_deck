using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSniperKing : UnitBase
{
    int damageAdd = 0;
    public override void Kill()
    {
        damageAdd++;
        UnitPropertiesSet(new Vector2[] { new(1, 0), new(2, 0), new(3, 0), new(4, 0), new(5, 0), new(6, 0) }, new() { damage = 5 + damageAdd, attacker = this }, 1);
        //��ü ������ ���ݷ� ��ȭ�Ǵµ� ��� �ذ��ϴ°� ������ �𸣰���
    }

    /*protected override void Attack()
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
        
    }*/
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { new(1, 0), new(2, 0), new(3, 0), new(4, 0), new(5, 0), new(6, 0) }, new() { damage = 5, attacker = this }, 1);
    }
}
