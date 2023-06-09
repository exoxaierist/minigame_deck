using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryReduceDamage : MonoBehaviour //무적 <- 데미지를 받지 않음으로 정의
{
    [SerializeField]
    [Tooltip("데미지를 줄이는 턴 수")]
    protected int ReduceDamageTurn = 5;

    [Tooltip("줄이는 데미지")]
    protected int ReduceDamage = int.MaxValue;
    public int getReduceDamage()
    {
        return ReduceDamage;
    }
    //[HideInInspector]
    protected bool isReduceDamage = false;
    public bool getIsReduceDamage()
    {
        return isReduceDamage;
    }


    protected int deltaTurn = 0;
    void check_ReduceDamageTurn()//데미지 줄이는 턴수 체크
    {
        if (isReduceDamage)
        {
            deltaTurn++;
            if(deltaTurn >= ReduceDamageTurn)
            {
                deltaTurn = 0;
                isReduceDamage = false;
            }
        }
    }
    private void Start()
    {
        Global.OnTurnStart += check_ReduceDamageTurn;
        //GetComponent<Hp>().DamageModifier += DamageModify; //피격 받을때 처리 대리자 등록
    }
    /*int DamageModify(int _value)
    {
        var value = _value;
        if (isReduceDamage)
        {
            if (value >=0)//힐 체크
            {
                return value;
            }

            int x = value + ReduceDamage;

            if (x>=0)
            {
                value = 0;
            }
            else
            {
                value = x;
            }
        }
        return value;
    }
    private void Update()
    {
        //check_ReduceDamageTurn();
    }*/
}
