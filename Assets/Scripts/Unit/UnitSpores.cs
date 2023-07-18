using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpores : UnitBase
{
    private int attackDamage = 1;
    private int moveDistanceBuffer = 1;
    UnitBase attackedTarget = null;

    protected override void OnDamage(UnitBase unit)
    {
        attackedTargetStun();
    }
    public override void ReceivePayload(AttackInfo _info)
    {
        attackedTarget = _info.attacker;
        base.ReceivePayload(_info);
    }
    private void attackedTargetStun()
    {
        Global.OnTurnStart += attackedTarget.Stun;
        //�� ���۽� �� ������ �� �ֱ� ������ �� ���۽� �ش� �ڵ忡�� �ڱ� �ڽ��� �����ϴ� �ڵ带 �� ���۽� �߰���
        //���� �� ���Ͻ� �ش� �ڵ带 �ݺ��ϸ� �ɵ�?
    }
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { new(2, 0) },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
    }
}
