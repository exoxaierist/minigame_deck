using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreprocessingUnit : UnitBase, IReceiveAttack
{
    protected Vector2 prevPos;
    protected Vector2 lookDirection;
    protected override void Awake()
    {
        base.Awake();
        prevPos = transform.position * Vector2.one;
    }

    // �ǰݽ� �ش� �Լ� ȣ��
    public virtual void ReceivePayload(AttackInfo _info)
    {
        AttackInfo preprocessedAttackInfo = PreprocessingDamage(_info);
        hp.AddToHP(preprocessedAttackInfo.damage); // ���� ������ �޴� ����� ������ ���� AttackInfo�� ����� �� ����
    }

    //�������� ��ó���ϴ� �Լ�
    public virtual AttackInfo PreprocessingDamage(AttackInfo _info)
    {
        AttackInfo preprocessedAttackInfo = _info;
        //������ ���ҽ�Ű�� �ڵ�
        //���� ü�¿��� �����͸� �־���
        return preprocessedAttackInfo;
    }
    
    //���� ��ġ ���� �� ���� �ִ� ������ �޴� �Լ�
    protected virtual void getLookDirection()
    {
        //�����϶� �۵��ϴ� �Լ��̹Ƿ� �������� ���� ���� ������
        //�ѹ� �����϶� x,y �� �� �ٲ��, �� �밢�� �̵��� ���ٰ� ����
        lookDirection = Vector2.zero;
        Vector2 movePos = prevPos - transform.position * Vector2.one;
        if(movePos.x == 0)
        {
            lookDirection = movePos.y < 0 ? new Vector2(1, -1) : new Vector2(1, 1);
        }
        else
        {
            lookDirection = movePos.x < 0 ? new Vector2(-1, 1) : new Vector2(1, 1);
        }
    }

    //�����ϴ� �Լ�
    protected virtual void Attack(AttackInfo _attackInfo)
    {
        // ���� �ִ� �������� ���� ����Ʈ ���� �� �� ������ AttackInfo ����
        // AttackInfo�� ���� ��ǥ�ʵ� ������ ������?
        // AttakEffectInstansiate(AttackInfo.attackPos[index] * lookDirction)
    }
    protected override void OnMove()
    {
        getLookDirection();
    }
}
