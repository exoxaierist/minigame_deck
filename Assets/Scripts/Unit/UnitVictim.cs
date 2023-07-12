using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitVictim : UnitBase
{
    private int attackDamage = 6;
    private Vector2[] attackRange = new Vector2[] { new(1, -1)};
    //지금 보니 움직이는 Move 함수가 UnitBase에서 private로 선언되어 있는데 protected로 안한 이유가?
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
        attackInfo = new() //공격정보 세팅 유닛 베이스의 값이 바뀔 수 있으므로 지정
        {
            damage = 1,
            attacker = this,
        };
    }
}
