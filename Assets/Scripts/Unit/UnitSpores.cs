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
        //턴 시작시 턴 리셋이 들어가 있기 때문에 턴 시작시 해당 코드에서 자기 자신을 스턴하는 코드를 턴 시작시 추가함
        //여러 턴 스턴시 해당 코드를 반복하면 될듯?
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
