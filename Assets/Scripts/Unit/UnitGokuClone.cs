using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGokuClone : UnitBase
{
    private int attackDamage = 3;
    private AttackInfo info;
    [SerializeField]
    private Vector2[] attackRange = new Vector2[] { new Vector2(1, 1), new Vector2(0, 1), new Vector2(-1, -1) };

    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        lastMoveDir = dir;
        dir = (invertMovement ? -dir : dir) * moveDistance;
        MoveRelative(dir);
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
    public void Summon(Player _player)
    {
        player = _player;
        if(player == Player.Player1) Global.unitManager.P1UnitList.Add(this);
        else Global.unitManager.P1UnitList.Add(this);
    }
    protected override void Awake()
    {
        base.Awake();
        moveDistance = 2;
    }
}
