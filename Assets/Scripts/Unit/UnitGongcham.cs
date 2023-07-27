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
        if (isEndured == false && _info.damage >= nowHp) // �ɷ��� �ߵ����� �ʾҴµ� �״� ���
        {
            isEndured = true;
            hp.AddToHP(-(nowHp - 1)); // ü���� 1�� ����
        }
        else
        {
            hp.AddToHP(-_info.damage); // ���� ������ �޴� ����� ������ ���� AttackInfo�� ����� �� ����
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
