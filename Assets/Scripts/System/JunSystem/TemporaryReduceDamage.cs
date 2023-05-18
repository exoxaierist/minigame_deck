using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryReduceDamage : UnitBase //���� <- �������� ���� �������� ����
{
    [SerializeField]
    [Tooltip("�������� ���̴� �� ��")]
    int ReduceDamageTurn = 5;

    [SerializeField]
    [Tooltip("���̴� ������")]
    int ReduceDamage = 10;

    [HideInInspector]
    public bool isReduceDamage = false;
    

    int prevTurn = 0;
    int deltaTurn = 0;
    void check_ReduceDamageTurn()//������ ���̴� �ϼ� üũ
    {
        if (isReduceDamage)
        {
            deltaTurn = 0;

            if (prevTurn < Global.turnManager.GetTurn())
            {
                prevTurn = Global.turnManager.GetTurn();

                deltaTurn++;
            }
            if (deltaTurn == ReduceDamageTurn)
            {
                isReduceDamage = false;

            }
        }
    }
    private void Start()
    {
        GetComponent<Hp>().DamageModifier += DamageModify; //�ǰ� ������ ó�� �븮�� ���
    }
    int DamageModify(int _value)
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
        check_ReduceDamageTurn();
    }
}
