using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛 개발 기반
public class UnitBase : ControlledObject
{
    public string id;
    public Hp hp;
    public PerkBase perk;

    private void Awake()
    {
        if(TryGetComponent<Hp>(out hp))
        {
            hp.OnHeal += OnHeal;
            hp.OnDamage += OnDamage;
            hp.OnDeath += OnDeath;
        } 
    }

    private void Start()
    {
        perk = new test();
        perk.Test();
    }

    protected virtual void OnHeal() { }
    protected virtual void OnDamage() { }
    protected virtual void OnDeath() { }
}
