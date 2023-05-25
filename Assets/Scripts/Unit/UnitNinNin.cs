using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitNinNin : UnitBase
{
    private float evadeProbability = 0.33f;

    public override void ReceivePayload(AttackInfo _info)
    {
        float dice = Random.Range(0.01f, 1.00f); //정확한 확률 계산을 위해 0을 뺸 시작을 0.01f로 잡음
        if (dice >= evadeProbability) hp.AddToHP(-_info.damage);
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
}
