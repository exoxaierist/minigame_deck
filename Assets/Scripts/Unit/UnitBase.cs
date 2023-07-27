using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 유닛 개발 기반
[RequireComponent(typeof(ShopFieldUnitPlacer))]
[RequireComponent(typeof(Hp))]
public class UnitBase : ControlledObject, IReceiveAttack
{
    // 죽을 때 오브젝트 alpha값이 줄어드는 코드가 어딘지 모르겠음
    [HideInInspector] public Hp hp;

    // 이동관련
    public Vector2 lastMoveDir = Vector2.right;
    public int moveDistance = 1; // 이동 칸 수
    public bool invertMovement = false; // 이동방향 반전

    protected int turnCount; // 턴마다 남아있는 행동 횟수
    public virtual void Kill() { }// 죽였을 때 호출하는 함수
    public virtual void GiveDamage(UnitBase _unitBase) // 데미지를 주었을 때 호출하는 함수
    {
        Global.unitManager.DamagedUnitData.Add(this); // 공격자 정보
        Global.unitManager.DamagedUnitData.Add(_unitBase); // 피격자 정보
    }

    //공격
    protected Vector2[] attackPattern = { new(1, 0) }; // 공격패턴, 오른쪽을 바라볼때의 패턴 기준으로 좌푯값
    protected AttackInfo attackInfo;
    //추가 체력, 공격력
    public int additionalHP = 0;
    public int additionalDamage = 0;
    protected override void Awake()
    {
        base.Awake();
        Global.OnTurnStart += ResetTurn; // 이 부분이 유닛에게 턴을 추가해주는 부분
        Global.OnRoundEnd += ResetUnit;
        Global.OnRoundEnd += AddUnitToUnitList;
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
        Global.OnTurnStart += ResetTurn;
        if (player == Player.Player1) gameObject.layer = 7;
        else if(player == Player.Player2) gameObject.layer = 8;
    }

    // 피격시 해당 함수 호출
    public virtual void ReceivePayload(AttackInfo _info)
    {
        hp.AddToHP(-_info.damage); // 추후 데미지 받는 방식의 수정에 따라 AttackInfo로 변경될 수 있음

        if(hp.isDamaged == true)
        {
            _info.attacker.GiveDamage(this);
            hp.isDamaged = false;
        }
        if(hp.isDead == true)
        {
            _info.attacker.Kill();
        }
    }
    protected virtual void OnHeal(UnitBase unit) { }
    protected virtual void OnDamage(UnitBase unit) { }
    protected virtual void OnDeath(UnitBase unit)
    {
        switch(player)
        {
            case Player.Player1:
                Global.unitManager.P1UnitList.Remove(this);
                Global.unitManager.P1DeadUnitList.Add(this);
                break;
            case Player.Player2:
                Global.unitManager.P2UnitList.Remove(this);
                Global.unitManager.P2DeadUnitList.Add(this);
                break;
            default:
                //플레이어들의 유닛이 아닌 경우
                break;
        }
        gameObject.layer = 9; // Dead Layer로 바꿈
        turnCount = 0;
        Global.OnTurnStart -= ResetTurn;
    }

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

    //해당 유닛들을 다시 P1UnitList와 P2UnitList에 추가해주는 코드
    protected virtual void AddUnitToUnitList()
    {
        switch (player)
        {
            case Player.Player1:
                Global.unitManager.AddToP1Units(this);
                break;
            case Player.Player2:
                Global.unitManager.AddToP2Units(this);
                break;
            default:
                //플레이어들의 유닛이 아닌 경우
                break;
        }
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
        dir = (invertMovement ? -dir : dir);
        lastMoveDir = dir;
        MoveRelative(dir*moveDistance);
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
