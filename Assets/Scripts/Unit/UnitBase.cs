using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ���
//[RequireComponent(typeof(ShopFieldUnitPlacer))]
public class UnitBase : ControlledObject, IReceiveAttack
{
    [HideInInspector] public Hp hp;

    // �̵�����
    public int moveDistance = 1; // �̵� ĭ ��
    public bool invertMovement = false; // �̵����� ����

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

    protected override void MoveUp() => Move(Vector2.up);
    protected override void MoveDown() => Move(Vector2.down);
    protected override void MoveRight() => Move(Vector2.right);
    protected override void MoveLeft() => Move(Vector2.left);

    private void Move(Vector2 dir)
    {
        dir = (invertMovement ? -dir : dir) * moveDistance;
        if (!canMove) return;
        MoveRelative(dir);
    }



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
