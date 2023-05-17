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
    void check_ReduceDamageTurn()
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
    private void Update()
    {
        check_ReduceDamageTurn();
    }
    public int GetReduceDamage()
    {
        return ReduceDamage;
    }



}
