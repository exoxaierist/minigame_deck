using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDivineShield : UnitBase
{
    //함수는 스택 Action은 힙, 따라서 스택쪽에 속한 함수가 더 빠름
    private int attackDamage = 6;

    [SerializeField]
    private Vector2[] attackRange = new Vector2[] {new Vector2(1,2), new Vector2(0,1), new Vector2(-1, 2)};
    private int moveCount = 0;
    private bool isShield = false;
    protected override void OnMove() //밀렸을때도 움직임 판정?
    {
        if (moveCount < 2) moveCount++;
        else
        {
            moveCount = 0;
            isShield = true;
        }
    }
    public override void ReceivePayload(AttackInfo _info)
    {
        if (isShield) isShield = false;
        else hp.AddToHP(-_info.damage);

        if (hp.isDamaged == true)
        {
            _info.attacker.GiveDamage(this);
            hp.isDamaged = false;
        }
        if (hp.isDead == true)
        {
            _info.attacker.Kill();
        }
    }
    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        AttackInfo info = new() //공격 정보
        {
            damage = 6,
            attacker = this
        };
        int attackAmount = attackRange.Length;
        for (int i = 0; i < attackAmount; i++)
        {
            Vector2 target = transform.position * Vector2.one + lastMoveDir * attackRange[i]; //공격할 위치
            Global.atkPooler.Get().Attack(target, info); //공격
        }
    }
    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        moveCount++;
        lastMoveDir = dir;
        dir = (invertMovement ? -dir : dir) * moveDistance;
        MoveRelative(dir);
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
