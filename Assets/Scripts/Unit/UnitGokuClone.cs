using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGokuClone : UnitBase
{
    private int attackDamage = 3;
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
        AttackInfo info = new() //���� ����
        {
            damage = 6,
            attacker = this
        };
        int attackAmount = attackRange.Length;
        for (int i = 0; i < attackAmount; i++)
        {
            Vector2 target = transform.position * Vector2.one + lastMoveDir * attackRange[i]; //������ ��ġ
            Global.atkPooler.Get().Attack(target, info); //����
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
