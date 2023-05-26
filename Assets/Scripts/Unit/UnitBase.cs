using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛 개발 기반
//[RequireComponent(typeof(ShopFieldUnitPlacer))]
public class UnitBase : ControlledObject, IReceiveAttack
{
    [HideInInspector] public Hp hp;

    // 이동관련
    public int moveDistance = 1; // 이동 칸 수
    public bool invertMovement = false; // 이동방향 반전

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
