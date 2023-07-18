using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitHook : UnitBase
{
    private int attackDamage = 1;
    private int moveDistanceBuffer = 1;
    private List<UnitBase> closeUnitList = new List<UnitBase>();
    private void grabFarthestEnemy()
    {
        bool isNearUnitChanged = nearUnitChanged();
        if(isNearUnitChanged == true)
        {
            int m_index = Global.unitManager.GetMyIndex(this, player);
            UnitBase m_target = Global.unitManager.GetFarthestEnemyInCross(m_index, player);
            if (m_target == null) return; // 타겟이 없다면 스킵
            if (m_target.TryGetComponent<UnitHook>(out UnitHook unitHook)) return; // 같은 종류의 유닛이면 스킵
            Vector2 m_moveTargetPos = transform.position;
            Vector2 m_targetPos = m_target.transform.position;
            Vector2 m_compareDir = Vector2.zero;
            //x좌표가 같을 때
            if (m_targetPos.x == m_moveTargetPos.x)
            {
                //이 유닛보다 위에 있는 경우
                if(m_targetPos.y >= m_moveTargetPos.y) m_compareDir = new(0,1);
                //이 유닛보다 아래에 있는 경우
                else m_compareDir = new (0,-1);
            }
            //y좌표가 같을 때
            else if(m_targetPos.y == m_moveTargetPos.y)
            {
                //이 유닛보다 오른쪽에 있는 경우
                if (m_targetPos.x >= m_moveTargetPos.x) m_compareDir = new(1, 0);
                //이 유닛보다 왼쪽에 있는 경우
                else m_compareDir = new(-1, 0);
            }
            m_moveTargetPos += m_compareDir;

            int m_count = 0;
            while(true)
            {
                List<UnitBase> m_searchList = new List<UnitBase>();
                if (m_count % 2 == 0) m_searchList = Global.unitManager.P1UnitList;
                else m_searchList = Global.unitManager.P2UnitList;
                int m_searchListCount = m_searchList.Count;
                bool targetListSearch()
                {
                    for (int i = 0; i < m_searchListCount; i++)
                    {
                        UnitBase m_compareTarget = m_searchList[i];
                        //비교 대상 Unit이 target 유닛과 같을 경우 스킵
                        if (m_compareTarget == m_target) continue;

                        Vector2 m_compareTargetPos = m_compareTarget.transform.position;
                        if (m_compareTargetPos == m_moveTargetPos)
                        {
                            m_moveTargetPos += m_compareDir;
                            return false;
                        }
                    }
                    return true;
                }
                bool isFinish = targetListSearch();
                if (isFinish == false) continue;
                else m_count++;
                if (m_count > 2) break;
            }
            //Debug.Log(m_target);
            //Debug.Log(m_moveTargetPos);
            m_target.gameObject.transform.position = m_moveTargetPos;
        }
    }
    private bool nearUnitChanged()
    {
        int m_index = Global.unitManager.GetMyIndex(this, player);
        List<UnitBase> m_closeUnitList = Global.unitManager.GetCloseAllies(m_index, player);

        if (closeUnitList.Count == m_closeUnitList.Count)
        {
            int m_count = closeUnitList.Count;
            int sameUnitCount = 0;
            for (int i = 0; i < m_count; i++)
            {
                for (int j = 0; j < m_count; j++)
                {
                    if (closeUnitList[i] == m_closeUnitList[j])
                    {
                        sameUnitCount++;
                        break;
                    }
                }
            }
            //주변 유닛 리스트 정보가 바뀌지 않은 경우
            if (sameUnitCount == m_count) return false;
        }
        return true;
    }
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { new(1, 0), new(4, 0)},
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
        Global.OnTurnStart += grabFarthestEnemy;
    }
}
