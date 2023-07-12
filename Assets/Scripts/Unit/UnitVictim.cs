using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVictim : UnitBase
{
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

    }
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { new(1, 1), new(1, 0), new(1, -1) },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
    }
}
