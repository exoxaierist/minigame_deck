using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMaster : UnitBase
{
    protected override void Awake()
    {
        base.Awake();

        hp.OnDamage += _ondamage;

        UnitPropertiesSet(new Vector2[] { new(1, 0), new(1, 1), new(1, -1), new(0, 1), new(0, -1), new(-1, 1), new(-1, 0), new(-1, -1) }, new() { attacker = this, damage = 1 }, 2);
    }

    void _ondamage(UnitBase unit)
    {
        if (unit.player == player)
        {
            if (unit.hp.GetHp() > 0)
            {
                unit.hp.AddToHP(1);
                unit.additionalDamage += 1;
            }
        }
    }
}
