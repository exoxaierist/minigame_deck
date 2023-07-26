using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGongcham : UnitBase
{
    private int attackDamage = 1;
    private int moveDistanceBuffer = 1;
    private bool isEndured = false;
    public override void ReceivePayload(AttackInfo _info)
    {
        int nowHp = hp.GetHp();
        if (isEndured == false && _info.damage >= nowHp) // 능력이 발동되지 않았는데 죽는 경우
        {
            isEndured = true;
            hp.AddToHP(-(nowHp - 1)); // 체력을 1로 만듬
        }
        else
        {
            hp.AddToHP(-_info.damage); // 추후 데미지 받는 방식의 수정에 따라 AttackInfo로 변경될 수 있음
        }
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
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] {new (1,1), new (1,-1) },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
    }
}
