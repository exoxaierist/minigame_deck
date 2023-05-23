using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryReduceDamage : MonoBehaviour //���� <- �������� ���� �������� ����
{
    [SerializeField]
    [Tooltip("�������� ���̴� �� ��")]
    protected int ReduceDamageTurn = 5;

    [Tooltip("���̴� ������")]
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
    void check_ReduceDamageTurn()//������ ���̴� �ϼ� üũ
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
        //GetComponent<Hp>().DamageModifier += DamageModify; //�ǰ� ������ ó�� �븮�� ���
    }
    /*int DamageModify(int _value)
    {
        var value = _value;
        if (isReduceDamage)
        {
            if (value >=0)//�� üũ
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
