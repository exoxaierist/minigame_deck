using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitSW : UnitBase
{
    List<int> lastUnitCount = new List<int>();
    /// <summary>
    /// 유닛을 끌어당기는 함수, 이미 인접해있거나 타겟 유닛과 주제 유닛 사이가 가득 차있다면 발동하지 않음
    /// </summary>
    /// <param name="_unit">타겟이 되는 유닛</param>
    public void PullUnit(UnitBase _unit)
    {
        Vector3 standardPos = transform.position; // 능력 주체가 되는 유닛의 좌표
        Vector3 targetPos = _unit.transform.position; // 능력 타겟이 되는 유닛의 좌표

        if (standardPos.x == targetPos.x)
        {
            for(int i = 1; i < Mathf.Abs(targetPos.y - standardPos.y); i++)
            {
                // 타겟 유닛이 주체 유닛보다 위에 있는 경우
                if(targetPos.y > standardPos.y)
                {
                    
                    if (Global.CheckOverlap(new(standardPos.x,standardPos.y + i), 1 << 1)) continue;
                    _unit.transform.position = new Vector3(standardPos.x, standardPos.y + i);
                    return;
                }
                // 타겟 유닛이 주체 유닛보다 아래에 있는 경우
                else
                {
                    if (Physics.Raycast(new Vector3(standardPos.x, standardPos.y - i, 0), Vector3.down)) continue;
                    _unit.transform.position = new Vector3(standardPos.x, standardPos.y - i);
                    return;
                }
            }
        }
        else if (standardPos.y == targetPos.y)
        {
            for (int i = 1; i < Mathf.Abs(targetPos.x - standardPos.x); i++)
            {
                // 타겟 유닛이 주체 유닛의 오른쪽에 있는 경우
                if (targetPos.x > standardPos.x)
                {
                    if (Physics.Raycast(new Vector3(standardPos.x + i, standardPos.y, 0), Vector3.down)) continue;
                    _unit.transform.position = new Vector3(standardPos.x + i, standardPos.y);
                    return;
                }
                // 타겟 유닛이 주체 유닛 왼쪽에 있는 경우
                else
                {
                    if (Physics.Raycast(new Vector3(standardPos.x - i, standardPos.y, 0), Vector3.down)) continue;
                    _unit.transform.position = new Vector3(standardPos.x - i, standardPos.y);
                    return;
                }
            }
        }
    }
    // 자동 공격이라고 써있지만 결국 조건에 만족하면 공격임
    public void AttackClosestEnemy()
    {
        int index = UnitManager.Instance.GetMyIndex(this, player);
        UnitBase target = UnitManager.Instance.GetClosestEnemy(index, player);
        //공격하는 코드
    }
    public void GetCloseToEnemyAutoAttack()
    {
        int index = UnitManager.Instance.GetMyIndex(this, player);
        if(lastUnitCount[(int)AllyOrEnemy.Enemy] < UnitManager.Instance.GetMyUnitCount(index, player)[(int)AllyOrEnemy.Enemy])
        {
            AttackClosestEnemy();
            lastUnitCount[(int)AllyOrEnemy.Enemy] = UnitManager.Instance.GetMyUnitCount(index, player)[(int)AllyOrEnemy.Enemy];
        }
    }
    public void CreateWall()
    {
        //벽 생성
        //벽은 따로 움직임 관련해서 코드와 오브젝트를 작성해야하기 떄문에 비워둠
    }
    public void SeperatedFromEnemyCreateWall()
    {
        int index = UnitManager.Instance.GetMyIndex(this, player);
        if (lastUnitCount[(int)AllyOrEnemy.Enemy] > UnitManager.Instance.GetMyUnitCount(index, player)[(int)AllyOrEnemy.Enemy])
        {
            CreateWall();
            lastUnitCount[(int)AllyOrEnemy.Enemy] = UnitManager.Instance.GetMyUnitCount(index, player)[(int)AllyOrEnemy.Enemy];
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
