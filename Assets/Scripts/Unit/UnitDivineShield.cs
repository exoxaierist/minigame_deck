using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDivineShield : UnitBase
{
    //�Լ��� ���� Action�� ��, ���� �����ʿ� ���� �Լ��� �� ����
    private int attackDamage = 6;
    AttackInfo info;
    [SerializeField]
    private Vector2[] attackRange = new Vector2[] {new Vector2(1,2), new Vector2(0,1), new Vector2(-1, 2)};
    private int moveCount = 0;
    private bool isShield = false;
    protected override void OnMove() //�з������� ������ ����?
    {
        if (moveCount < 2) moveCount++;
        else
        {
            moveCount = 0;
            isShield = true;
        }
    }
    public override void ReceivePayload(AttackInfo _info)
    {
        if (isShield) isShield = false;
        else hp.AddToHP(-_info.damage);
    }
    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        foreach (var item in attackRange)
        {
            Vector2 target = transform.position * Vector2.one + lastMoveDir * item; //������ ��ġ
            Global.atkPooler.Get().Attack(target, info); //����
        }
    }
    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        moveCount++;
        lastMoveDir = dir;
        dir = (invertMovement ? -dir : dir) * moveDistance;
        MoveRelative(dir);
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
