using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGeumgang : UnitBase
{
    protected override void Awake()
    {
        base.Awake();
        hp.OnDamage += onDamage;
        UnitPropertiesSet(new Vector2[] { new(1, 0), new(2, 0) }, new() { attacker = this, damage = 3 }, 2);
    }
    
    int addattack = 0;
    void onDamage(UnitBase unit)
    {
        addattack++;
        UnitPropertiesSet(new Vector2[] { new(1, 0), new(2, 0) }, new() { attacker = this, damage = 3  + addattack}, 2);
    }
}
