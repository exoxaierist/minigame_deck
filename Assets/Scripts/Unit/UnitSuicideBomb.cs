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
    //������ ������ �޾ƾ��ؼ� AttackInfo�� ���� �� �ְ� ReceivePayload �Լ��� ������ ��
    //������ ���� �����ÿ��� �������� ���� ���� ����� ȸ���� �����µ�
    //�׸��� ���� �״� �����̶� ���ºΰ� ������ ����
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
