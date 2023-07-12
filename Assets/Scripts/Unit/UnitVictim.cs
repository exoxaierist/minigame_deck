using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVictim : UnitBase
{
    private int attackDamage = 6;
    private Vector2[] attackRange = new Vector2[] { new(1, -1)};
    //���� ���� �����̴� Move �Լ��� UnitBase���� private�� ����Ǿ� �ִµ� protected�� ���� ������?
    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        lastMoveDir = dir;
        dir = (invertMovement ? -dir : dir) * moveDistance;
        MoveRelative(dir);
    }
    protected override void Awake()
    {
        base.Awake();
        attackInfo = new() //�������� ���� ���� ���̽��� ���� �ٲ� �� �����Ƿ� ����
        {
            damage = 1,
            attacker = this,
        };
    }
}
