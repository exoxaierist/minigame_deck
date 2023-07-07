using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBlowout : UnitBase
{
    protected override void Awake()
    {
        base.Awake();

        Global.OnTurnStart += turnstart;
    }
    UnitBase[] units;
    void turnstart()
    {
        if (player == Player.Player1)
        {
            units = Global.unitManager.P2UnitList.ToArray();

        }
        else if (player == Player.Player2) 
        {
            units = Global.unitManager.P1UnitList.ToArray();
        }

        int ran= Random.Range(0, units.Length);

        units[ran].Stun();
    }
}
