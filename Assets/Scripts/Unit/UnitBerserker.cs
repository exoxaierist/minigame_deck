using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBerserker : UnitBase
{
    public int SelfDamage = -3;
    void OnAttack()
    {
        hp.AddToHP(SelfDamage);
    }
}
