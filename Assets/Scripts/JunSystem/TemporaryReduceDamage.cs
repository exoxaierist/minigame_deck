using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryReduceDamage : UnitBase //���� <- �������� ���� �������� ����
{
    [SerializeField]
    [Tooltip("�������� ���̴� �� ��")]
    int ReduceDamageTurn = 5;

    [Tooltip("���̴� ������")]
    public int ReduceDamage = int.MaxValue;

    //[HideInInspector]
    public bool isReduceDamage = false;
    
    int deltaTurn = 0;
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
