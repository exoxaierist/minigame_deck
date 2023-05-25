using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛 개발 기반
//[RequireComponent(typeof(ShopFieldUnitPlacer))]
public class UnitBase : ControlledObject, IReceiveAttack
{
    [Header("유닛 정보")]
    public string id = "unassigned";
    [HideInInspector] public Hp hp;

    protected override void Awake()
    {
        base.Awake();
        CheckForHP();
    }

    // 피격시 해당 함수 호출
    public virtual void ReceivePayload(AttackInfo _info)
    {
        hp.AddToHP(_info.damage); // 추후 데미지 받는 방식의 수정에 따라 AttackInfo로 변경될 수 있음
    }

    protected virtual void OnHeal(UnitBase unit) { }
    protected virtual void OnDamage(UnitBase unit) { }
    protected virtual void OnDeath(UnitBase unit) { }

    // 오브젝트에 같이 딸려있는 HP 컴포넌트 찾아서 이벤트 등록
    protected void CheckForHP()
    {
        if (TryGetComponent(out hp))
        {
            hp.unit = this;
            hp.OnHeal += OnHeal;
            hp.OnDamage += OnDamage;
            hp.OnDeath += OnDeath;
        }
    }
}
