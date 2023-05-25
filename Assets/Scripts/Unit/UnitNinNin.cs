using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNinNin : PreprocessingUnit
{
    private float probability = 0.33f;
    private int evadeTurn = 1;

    /// <summary>
    /// 회피를 하였는지 체크하는 함수
    /// </summary>
    /// <param name="_probability">회피할 확률</param>
    /// <returns>회피 성공 실패를 반환</returns>
    protected bool evadeCheck(float _probability)
    {
        float dice = Random.Range(0.01f, 1.00f); //정확한 확률 계산을 위해 0을 뺸 시작을 0.01f로 잡음
        if (dice <= _probability)
        {
            return true; //확률에 포함되지 된다면 회피를 하므로 true
        }
        else return false; //확률에 포함되지 않는다면 회피를 못하므로 false
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
