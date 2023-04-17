using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛 개발 기반
public class UnitBase : ControlledObject
{
    [Header("유닛 정보")]
    public string id = "unassigned";
    [HideInInspector] public Hp hp;
    public PerkBase perk;

    protected override void Awake()
    {
        base.Awake();
        CheckForHP();
    }

    protected virtual void OnHeal() { }
    protected virtual void OnDamage() { }
    protected virtual void OnDeath() { }

    // 오브젝트에 같이 딸려있는 HP 컴포넌트 찾아서 이벤트 등록
    protected void CheckForHP()
    {
        if (TryGetComponent<Hp>(out hp))
        {
            hp.OnHeal += OnHeal;
            hp.OnDamage += OnDamage;
            hp.OnDeath += OnDeath;
        }
    }
}
