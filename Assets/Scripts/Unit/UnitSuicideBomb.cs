using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSuicideBomb : UnitBase
{
    private int attackDamage = 0;
    private int moveDistanceBuffer = 2;

    private void suicide(AttackInfo _info)
    {
        Vector2 targetPos = _info.attacker.transform.position;
        AttackInfo info = new AttackInfo()
        {
            damage = int.MaxValue,
            attacker = this,
        };
        Global.atkPooler.Get().Attack(targetPos * Vector2.one, info);
    }
    //공격자 정보를 받아야해서 AttackInfo를 받을 수 있게 ReceivePayload 함수를 재정의 함
    //문제점 라운드 끝날시에도 데미지가 들어가서 라운드 종료시 회복이 씹히는듯
    //그리고 먼저 죽는 판정이라 무승부가 나오진 않음
    public override void ReceivePayload(AttackInfo _info)
    {
        hp.AddToHP(-int.MaxValue);

        if (hp.isDead == true)
        {
            _info.attacker.Kill();
        }

        suicide(_info);
    }
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
    }
}
