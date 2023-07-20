using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAuto : UnitBase
{
    private int attackDamage = 1;
    private int moveDistanceBuffer = 0;
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
    }
}
