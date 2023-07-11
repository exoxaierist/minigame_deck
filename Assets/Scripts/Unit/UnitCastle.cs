using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitCastle : UnitBase
{
    
    protected override void Awake()
    {
        base.Awake();

        hp.OnDamage += ondamage;
    }

    void ondamage(UnitBase @base)
    {
        hp.AddToHP(Random.Range(1, 3));
    }

}
