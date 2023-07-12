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
        float dice = Random.Range(0.01f, 1.00f); //��Ȯ�� Ȯ�� ����� ���� 0�� �A ������ 0.01f�� ����
        if (dice >= evadeProbability) hp.AddToHP(-_info.damage);
    }
    //protected override void Attack()
    //{
    //    if (!matchMode || turnCount <= 0) return;
    //    turnCount--;
    //    foreach (var item in attackRange)
    //    {
    //        Vector2 target = transform.position * Vector2.one + lastMoveDir * item; //������ ��ġ
    //        Global.atkPooler.Get().Attack(target, info); //����
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
