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
            if (m_target == null) return; // Ÿ���� ���ٸ� ��ŵ
            if (m_target.TryGetComponent<UnitHook>(out UnitHook unitHook)) return; // ���� ������ �����̸� ��ŵ
            Vector2 m_moveTargetPos = transform.position;
            Vector2 m_targetPos = m_target.transform.position;
            Vector2 m_compareDir = Vector2.zero;
            //x��ǥ�� ���� ��
            if (m_targetPos.x == m_moveTargetPos.x)
            {
                //�� ���ֺ��� ���� �ִ� ���
                if(m_targetPos.y >= m_moveTargetPos.y) m_compareDir = new(0,1);
                //�� ���ֺ��� �Ʒ��� �ִ� ���
                else m_compareDir = new (0,-1);
            }
            //y��ǥ�� ���� ��
            else if(m_targetPos.y == m_moveTargetPos.y)
            {
                //�� ���ֺ��� �����ʿ� �ִ� ���
                if (m_targetPos.x >= m_moveTargetPos.x) m_compareDir = new(1, 0);
                //�� ���ֺ��� ���ʿ� �ִ� ���
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
                        //�� ��� Unit�� target ���ְ� ���� ��� ��ŵ
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
            //�ֺ� ���� ����Ʈ ������ �ٲ��� ���� ���
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
