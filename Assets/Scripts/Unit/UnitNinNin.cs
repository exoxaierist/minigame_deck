using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNinNin : UnitBase
{
    private int attackDamage = 5;
    private int moveDistanceBuffer = 4;
    
    //private AttackInfo info;
    //[SerializeField]
    //private Vector2[] attackRange = new Vector2[] {new (1,0),new (0,-1), new (0,1), new (3, 0), new (-1,0)};
    private float evadeProbability = 0.33f;
    public override void ReceivePayload(AttackInfo _info)
    {
        float dice = Random.Range(0.01f, 1.00f); //정확한 확률 계산을 위해 0을 뺸 시작을 0.01f로 잡음
        if (dice >= evadeProbability) hp.AddToHP(-_info.damage);
    }
    //protected override void Attack()
    //{
    //    if (!matchMode || turnCount <= 0) return;
    //    turnCount--;
    //    foreach (var item in attackRange)
    //    {
    //        Vector2 target = transform.position * Vector2.one + lastMoveDir * item; //공격할 위치
    //        Global.atkPooler.Get().Attack(target, info); //공격
    //    }
    //}
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
        UnitPropertiesSet(new Vector2[] { new(1, 0), new(0, -1), new(0, 1), new(3, 0), new(-1, 0) },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
    }

}
