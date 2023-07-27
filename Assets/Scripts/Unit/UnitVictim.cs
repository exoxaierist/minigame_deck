using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVictim : UnitBase
{
    //죽을때 멈춰버림 버그 있음
    private int attackDamage = 1;
    private int moveDistanceBuffer = 2;
    //private Vector2[] attackRange = new Vector2[] { new(1, -1)};
    //지금 보니 움직이는 Move 함수가 UnitBase에서 private로 선언되어 있는데 protected로 안한 이유가?
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
        int cnt = 0;
        while (true) //while문은 현업에서 coroutine안에 넣어서 사용하는 경우가 많다고 함
        {
            targetUnit = Global.unitManager.GetRandomDeadAlly(player);
            if (targetUnit == null) return;
            if (targetUnit != this) break;
            else if (cnt > 20)
            {
                Debug.LogError("Revive Target Unit Not Exist");
                return;
            }
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
