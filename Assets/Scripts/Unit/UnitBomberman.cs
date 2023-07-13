using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBomberman : UnitBase
{
    private int attackDamage = 2;
    private int moveDistanceBuffer = 1;
    [SerializeField]
    //private Vector2[] attackRange = new Vector2[] { new Vector2(1,-1), new Vector2(1,0), new Vector2(1,1), new Vector2(0, -1), new Vector2(0, 1), new Vector2(-1, -1), new Vector2(-1, 0), new Vector2(-1, 1) };
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

        randomFieldAttack();
    }
    private void randomFieldAttack()
    {
        Vector2 targetPos = Global.fieldManager.GetRandomTile();

        AttackInfo info = attackInfo;
        if (additionalDamage != 0)
        {
            attackInfo.damage += additionalDamage;
        }
        Global.atkPooler.Get().Attack(targetPos * Vector2.one, attackInfo);
        attackInfo = info;
    }
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { new (1, -1), new (1, 0), new (1, 1), new (0, -1), new (0, 1), new (-1, -1), new (-1, 0), new (-1, 1) },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
    }
}
