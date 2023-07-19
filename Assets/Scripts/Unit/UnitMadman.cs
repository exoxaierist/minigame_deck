using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMadman : UnitBase
{
    public int AttackDamage = 5;
    public int SelfDamage = 3;
    //[SerializeField] Vector2[] attackRange = new Vector2[] { new Vector2(1, 0), new Vector2(1, 1), new Vector2(0, 1), new Vector2(-1, 1), new Vector2(-1, 0) };
    AttackInfo info;
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[]{ new(1, 0), new(1, 1), new(1, -1), new(0, 1), new(0, -1) }, new() { damage = AttackDamage, attacker = this }, 1);
        /*info = new() //공격 정보
        {
            damage = AttackDamage,
            attacker = this
        };*/
    }
    protected override void Attack()
    {
        base.Attack();
        hp.AddToHP(-SelfDamage);
        
        
    }
}
