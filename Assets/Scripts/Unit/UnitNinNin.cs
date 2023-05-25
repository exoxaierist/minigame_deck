using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNinNin : PreprocessingUnit
{
    private float probability = 0.33f;
    private int evadeTurn = 1;

    /// <summary>
    /// ȸ�Ǹ� �Ͽ����� üũ�ϴ� �Լ�
    /// </summary>
    /// <param name="_probability">ȸ���� Ȯ��</param>
    /// <returns>ȸ�� ���� ���и� ��ȯ</returns>
    protected bool evadeCheck(float _probability)
    {
        float dice = Random.Range(0.01f, 1.00f); //��Ȯ�� Ȯ�� ����� ���� 0�� �A ������ 0.01f�� ����
        if (dice <= _probability)
        {
            return true; //Ȯ���� ���Ե��� �ȴٸ� ȸ�Ǹ� �ϹǷ� true
        }
        else return false; //Ȯ���� ���Ե��� �ʴ´ٸ� ȸ�Ǹ� ���ϹǷ� false
    }
    public override AttackInfo PreprocessingDamage(AttackInfo _info)
    {
        AttackInfo preprocessedAttackInfo = _info;
        if (evadeCheck(probability)) preprocessedAttackInfo.damage = 0;
        return preprocessedAttackInfo;
    }
    protected override void Attack(AttackInfo _attackInfo)
    {

    }
    protected override void MoveUp()
    {
        if (canMove) MoveRelative(new(0, 4), collisionLayer.value);
    }
    protected override void MoveDown()
    {
        if (canMove) MoveRelative(new(0, -4), collisionLayer.value);
    }
    protected override void MoveRight()
    {
        if (canMove) MoveRelative(new(4, 0), collisionLayer.value);
    }
    protected override void MoveLeft()
    {
        if (canMove) MoveRelative(new(-4, 0), collisionLayer.value);
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
