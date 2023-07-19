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
        //시체 때리면 공격력 강화되는데 어떻게 해결하는게 좋을지 모르겠음
    }

    /*protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        
        AttackInfo info = new() //공격 정보
        {
            damage = 1 + damageAdd,
            attacker = this
        };
        for (int i = 0; i < 6; i++)
        {
            Vector2 target = transform.position * Vector2.one + lastMoveDir * (i+1); //공격할 위치
            Global.atkPooler.Get().Attack(target, info); //공격
        }
        
    }*/
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { new(1, 0), new(2, 0), new(3, 0), new(4, 0), new(5, 0), new(6, 0) }, new() { damage = 5, attacker = this }, 1);
    }
}
