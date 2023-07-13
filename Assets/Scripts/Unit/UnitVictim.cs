using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVictim : UnitBase
{
    private int attackDamage = 1;
    private int moveDistanceBuffer = 2;
    //private Vector2[] attackRange = new Vector2[] { new(1, -1)};
    //���� ���� �����̴� Move �Լ��� UnitBase���� private�� ����Ǿ� �ִµ� protected�� ���� ������?
    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        lastMoveDir = dir;
        dir = (invertMovement ? -dir : dir) * moveDistance;
        MoveRelative(dir);
    }
    private void ReviveAlly()
    {
        UnitBase targetUnit = new UnitBase();
        while (true)
        {
            int cnt = 0;
            targetUnit = Global.unitManager.GetRandomDeadAlly(player);
            if (targetUnit != this) break;
            else if (cnt > 20) Debug.LogError("Revive Target Unit Not Exist");
            else cnt++;
        }
        Global.unitManager.ReviveUnits(new List<UnitBase>() { targetUnit });
    }
    protected override void OnDeath(UnitBase unit)
    {
        base.OnDeath(unit);
        ReviveAlly();
    }
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { new(1, -1) },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
    }
}
