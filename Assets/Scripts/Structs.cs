using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackInfo{
    public int damage;
}

public interface IReceiveAttack
{
    public void ReceivePayload(AttackInfo info)
    {

    }
}
