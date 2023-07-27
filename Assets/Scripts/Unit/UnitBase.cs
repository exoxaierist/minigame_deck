using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ���
[RequireComponent(typeof(ShopFieldUnitPlacer))]
[RequireComponent(typeof(Hp))]
public class UnitBase : ControlledObject, IReceiveAttack
{
    // ���� �� ������Ʈ alpha���� �پ��� �ڵ尡 ����� �𸣰���
    [HideInInspector] public Hp hp;

    // �̵�����
    public Vector2 lastMoveDir = Vector2.right;
    public int moveDistance = 1; // �̵� ĭ ��
    public bool invertMovement = false; // �̵����� ����

    protected int turnCount; // �ϸ��� �����ִ� �ൿ Ƚ��
    public virtual void Kill() { }// �׿��� �� ȣ���ϴ� �Լ�
    public virtual void GiveDamage(UnitBase _unitBase) // �������� �־��� �� ȣ���ϴ� �Լ�
    {
        Global.unitManager.DamagedUnitData.Add(this); // ������ ����
        Global.unitManager.DamagedUnitData.Add(_unitBase); // �ǰ��� ����
    }

    //����
    protected Vector2[] attackPattern = { new(1, 0) }; // ��������, �������� �ٶ󺼶��� ���� �������� ��ǩ��
    protected AttackInfo attackInfo;
    //�߰� ü��, ���ݷ�
    public int additionalHP = 0;
    public int additionalDamage = 0;
    protected override void Awake()
    {
        base.Awake();
        Global.OnTurnStart += ResetTurn; // �� �κ��� ���ֿ��� ���� �߰����ִ� �κ�
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
        attackInfo = new() //�������� ����
        {
            damage = 1,
            attacker = this,
        };
    }
    /// <summary>
    /// ������ �ʱⰪ�� ���ָ��� �����ִ� �Լ�
    /// </summary>
    /// <param name="_attackPattern">���� ����</param>
    /// <param name="_attackInfo">AttackInfo ������</param>
    /// <param name="_moveDistance">�����̴� �Ÿ�</param>
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

    // �ǰݽ� �ش� �Լ� ȣ��
    public virtual void ReceivePayload(AttackInfo _info)
    {
        hp.AddToHP(-_info.damage); // ���� ������ �޴� ����� ������ ���� AttackInfo�� ����� �� ����

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
                //�÷��̾���� ������ �ƴ� ���
                break;
        }
        gameObject.layer = 9; // Dead Layer�� �ٲ�
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

    //�ش� ���ֵ��� �ٽ� P1UnitList�� P2UnitList�� �߰����ִ� �ڵ�
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
                //�÷��̾���� ������ �ƴ� ���
                break;
        }
    }
    private void ResetTurn()
    {
        turnCount = 1;
    }
    
    public void Stun() // ���� ���� �� ���� �μ��� ����
    {
        turnCount = 0;
        Global.OnTurnStart -= Stun;
        //������ �ƴٸ� ������ �� ���۽� �߰��ϴ� �ڵ带 ����
    }

    // �ڵ� ������ �������� �ʴµ� ��ġ���� �� ī��Ʈ ���� �ڵ尡 ���� ���ۼ��̶� �׷���?
    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        dir = (invertMovement ? -dir : dir);
        lastMoveDir = dir;
        MoveRelative(dir*moveDistance);
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
