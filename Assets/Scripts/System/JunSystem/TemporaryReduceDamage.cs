using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryReduceDamage : UnitBase //무적 <- 데미지를 받지 않음으로 정의
{
    [SerializeField]
    [Tooltip("데미지를 줄이는 턴 수")]
    int ReduceDamageTurn = 5;

    [SerializeField]
    [Tooltip("줄이는 데미지")]
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
