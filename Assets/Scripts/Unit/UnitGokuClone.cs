using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGokuClone : UnitBase
{
    private int attackDamage = 3;
    private int moveDistanceBuffer = 2;
    [SerializeField]

    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        lastMoveDir = dir;
        dir = (invertMovement ? -dir : dir) * moveDistance;
        MoveRelative(dir);
    }
    //protected override void Attack()
    //{
    //    if (!matchMode || turnCount <= 0) return;
    //    turnCount--;
    //    AttackInfo info = new() //공격 정보
    //    {
    //        damage = 6,
    //        attacker = this
    //    };
    //    int attackAmount = attackRange.Length;
    //    for (int i = 0; i < attackAmount; i++)
    //    {
    //        Vector2 target = transform.position * Vector2.one + lastMoveDir * attackRange[i]; //공격할 위치
    //        Global.atkPooler.Get().Attack(target, info); //공격
    //    }
    //}
    public void Summon(Player _player)
    {
        player = _player;
        if(player == Player.Player1) Global.unitManager.P1UnitList.Add(this);
        else Global.unitManager.P1UnitList.Add(this);
    }
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { new (1, 1), new (0, 1), new (-1, -1) },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
    }
}
