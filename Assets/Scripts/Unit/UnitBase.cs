using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ���
//[RequireComponent(typeof(ShopFieldUnitPlacer))]
public class UnitBase : ControlledObject, IReceiveAttack
{
    [Header("���� ����")]
    public string id = "unassigned";
    [HideInInspector] public Hp hp;

    protected override void Awake()
    {
        base.Awake();
        CheckForHP();
    }

    // �ǰݽ� �ش� �Լ� ȣ��
    public virtual void ReceivePayload(AttackInfo _info)
    {
        hp.AddToHP(_info.damage); // ���� ������ �޴� ����� ������ ���� AttackInfo�� ����� �� ����
    }

    protected virtual void OnHeal(UnitBase unit) { }
    protected virtual void OnDamage(UnitBase unit) { }
    protected virtual void OnDeath(UnitBase unit) { }

    // ������Ʈ�� ���� �����ִ� HP ������Ʈ ã�Ƽ� �̺�Ʈ ���
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
