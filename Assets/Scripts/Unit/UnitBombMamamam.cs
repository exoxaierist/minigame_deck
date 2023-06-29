using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBombMamamam : UnitBase
{
    private int attackDamage = 2;
    AttackInfo info;
    [SerializeField]
    private Vector2[] attackRange = new Vector2[] { new Vector2(1,-1), new Vector2(1,0), new Vector2(1,1), new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1) };
    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        lastMoveDir = dir;
        dir = (invertMovement ? -dir : dir) * moveDistance;
        MoveRelative(dir);
    }
    protected override void OnMove()
    {
        //공격하는 함수가 아직 만들어지지 않아 주석처리
        //Global.atkPooler.Get().Attack(Global.fieldManager.GetRandomTile(), info);
        //공격하는 턴 때문에 한번 설명 듣고 짤 예정
    }
    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        foreach (var item in attackRange)
        {
            Vector2 target = transform.position * Vector2.one + lastMoveDir * item; //공격할 위치
            Global.atkPooler.Get().Attack(target, info); //공격
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
