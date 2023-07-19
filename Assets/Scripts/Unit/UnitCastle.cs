using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCastle : UnitBase
{

    protected override void Awake()
    {
        base.Awake();

        hp.OnDamage += ondamage;
        UnitPropertiesSet(new Vector2[] { new(1, 0) }, new() { attacker = this, damage = 1 }, 1);
    }

    void ondamage(UnitBase @base)
    {
        hp.AddToHP(Random.Range(1, 3));
    }

}
