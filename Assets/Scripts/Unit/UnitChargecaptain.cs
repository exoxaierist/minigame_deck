using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitChargecaptain : UnitBase
{
    void Ondeath(UnitBase @base)
    {
        if (player == Player.Player1)
        {
            foreach (var item in Global.unitManager.P1UnitList)
            {
                item.additionalDamage += 2;
            }
        }
        else if (player == Player.Player2)
        {
            foreach (var item in Global.unitManager.P2UnitList)
            {
                item.additionalDamage += 2;
            }
        }
        
        
    }

    protected override void Awake()
    {
        base.Awake();

        hp.OnDeath += Ondeath;
    }
}
