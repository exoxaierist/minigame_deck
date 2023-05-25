using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHealer : UnitBase
{
    List<UnitBase> closeAllies = new();

    private void UpdateCloseAllies()
    {
        RemoveFromDelegate();
        closeAllies = Global.unitManager.GetCloseAllies(Global.unitManager.GetMyIndex(this,player),player);
        AddToDelegate();
    }

    private void AddToDelegate()
    {
        foreach (UnitBase unit in closeAllies)
        {
            unit.hp.OnDamage += OnCloseAllyDamage;
        }
    }

    private void RemoveFromDelegate()
    {
        foreach (UnitBase unit in closeAllies)
        {
            unit.hp.OnDamage -= OnCloseAllyDamage;
        }
    }

    private void OnCloseAllyDamage(UnitBase unit)
    {
        unit.hp.AddToHP(1);
    }
}
