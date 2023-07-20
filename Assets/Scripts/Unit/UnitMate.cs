using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMate : UnitBase
{
    protected override void Awake()
    {
        base.Awake();

        UnitPropertiesSet(new Vector2[] { new(2, 0) }, new() { damage = 2, attacker = this }, 1);
        
    }

    void _onattack()
    {
        var _unit = Physics2D.OverlapCircle(lastMoveDir.normalized * -1, 0.3f, Global.collisionPlayer);
        if (_unit != null)
        {
            if(_unit.GetComponent<UnitBase>().player == player)
            {
                additionalDamage += 1;
            }
        }
    }

    protected override void Attack()
    {
        _onattack();
        base.Attack();

    }
}
