using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛 개발 기반
[RequireComponent(typeof(ShopFieldUnitPlacer))]
[RequireComponent(typeof(Hp))]
public class UnitBase : ControlledObject, IReceiveAttack
{
    [HideInInspector] public Hp hp;

    // 이동관련
    public Vector2 lastMoveDir = Vector2.right;
    public int moveDistance = 1; // 이동 칸 수
    public bool invertMovement = false; // 이동방향 반전

    protected int turnCount; // 턴마다 남아있는 행동 횟수
    public virtual void Kill() { }// 죽였을 때 호출하는 함수

    //공격
    protected Vector2[] attackPattern = { new(1, 0) }; // 공격패턴, 오른쪽을 바라볼때의 패턴 기준으로 좌푯값
    protected AttackInfo attackInfo;
    //추가 체력, 공격력
    public int additionalHP = 0;
    public int additionalDamage = 0;
    protected override void Awake()
    {
        base.Awake();
        Global.OnTurnStart += ResetTurn;
        Global.OnRoundEnd += ResetUnit;
        CheckForHP();
        if(player == Player.Player1)
        {
            gameObject.layer = LayerMask.NameToLayer("P1");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("P2");
            lastMoveDir = Vector2.left;
        }
        attackInfo = new() //공격정보 세팅
        {
            damage = 1,
            attacker = this,
        };
    }
    /// <summary>
    /// 유닛의 초기값을 유닛마다 정해주는 함수
    /// </summary>
    /// <param name="_attackPattern">공격 패턴</param>
    /// <param name="_attackInfo">AttackInfo 데이터</param>
    /// <param name="_moveDistance">움직이는 거리</param>
    protected void UnitPropertiesSet(Vector2[] _attackPattern, AttackInfo _attackInfo, int _moveDistance)
    {
        attackPattern = _attackPattern;
        attackInfo = _attackInfo;
        moveDistance = _moveDistance;
    }
    public void ResetUnit()
    {
        hp.ResetHP();
    }

    // 피격시 해당 함수 호출
    public virtual void ReceivePayload(AttackInfo _info)
    {
        hp.AddToHP(-_info.damage); // 추후 데미지 받는 방식의 수정에 따라 AttackInfo로 변경될 수 있음

        if(hp.isDead == true)
        {
            _info.attacker.Kill();
        }
    }
    protected virtual void OnHeal(UnitBase unit) { }
    protected virtual void OnDamage(UnitBase unit) { }
    protected virtual void OnDeath(UnitBase unit) { }

    protected override void MoveUp() => Move(Vector2.up);
    protected override void MoveDown() => Move(Vector2.down);
    protected override void MoveRight() => Move(Vector2.right);
    protected override void MoveLeft() => Move(Vector2.left);
    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        AttackInfo info = attackInfo;
        if (additionalDamage !=0 )
        {
            attackInfo.damage += additionalDamage;
        }
        foreach (Vector2 target in Global.RotateAttackPattern(attackPattern,lastMoveDir))
        {
            Global.atkPooler.Get().Attack(target+transform.position*Vector2.one, attackInfo);
        }
        attackInfo = info;
    }

    private void ResetTurn()
    {
        turnCount = 1;
    }
    
    public void Stun() // 스턴 당할 턴 수를 인수로 받음
    {
        turnCount = 0;
        Global.OnTurnStart -= Stun;
        //스턴이 됐다면 스턴을 턴 시작시 추가하는 코드를 빼줌
    }

    // 코드 오류로 움직이지 않는데 매치모드랑 턴 카운트 관련 코드가 아직 미작성이라 그런듯?
    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        dir = (invertMovement ? -dir : dir) * moveDistance;
        lastMoveDir = dir;
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
