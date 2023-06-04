using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBerserker : UnitBase
{
    public int AttackDamage = 5;
    public int SelfDamage = -3;
    [SerializeField] Vector2[] attackRange = new Vector2[] { new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1), new Vector2(-1, 1), new Vector2(-1, 0) };
    AttackInfo info;
    protected override void Awake()
    {
        base.Awake();
        info = new() //공격 정보
        {
            damage = AttackDamage,
            attacker = this
        };
    }
    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        hp.AddToHP(SelfDamage);
        foreach (var item in attackRange)
        {
            Vector2 target = transform.position * Vector2.one + lastMoveDir * item; //공격할 위치
            Global.atkPooler.Get().Attack(target, info); //공격
        }
        
    }
}
