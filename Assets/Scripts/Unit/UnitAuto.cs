using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitAuto : UnitBase
{
    private int attackDamage = 1;
    private int moveDistanceBuffer = 0;
    List<UnitBase> attackTargetList = new List<UnitBase>();

    private void additionalAttack()
    {
        attackTargetList.Clear(); // ����Ʈ �ʱ�ȭ

        List<UnitBase> m_targetList = Global.unitManager.DamagedUnitData;
        int m_listCount = m_targetList.Count;

        bool isTarget = false;
        for (int i = 0; i < m_listCount; i++)
        {
            if (i % 2 == 0) // �ε����� ¦���� ���, �� ������ �������� ���
            {
                if (m_targetList[i].player == player) isTarget = true; // �÷��̾ ������ Ȯ�� �� Ÿ�� Ȯ��
            }
            else
            {
                if (isTarget) attackTargetList.Add(m_targetList[i]); // Ȧ���� ��� Ÿ���̶�� ����Ʈ�� �߰�
                isTarget = false;
            }
        }

        m_listCount = attackTargetList.Count;
        if (m_listCount == 0) return;
        Debug.Log("autoAttack");
        int targetIndex = Random.Range(0, m_listCount);
        targetPosAttack(attackTargetList[targetIndex].gameObject.transform);
    }
    private void targetPosAttack(Transform _targetTransform)
    {
        AttackInfo info = attackInfo;
        if (additionalDamage != 0) attackInfo.damage += additionalDamage;
        Global.atkPooler.Get().Attack(_targetTransform.position, attackInfo);
        attackInfo = info;
    }
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
        Global.OnTurnStart += additionalAttack;
    }
}
