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
        attackTargetList.Clear(); // 리스트 초기화

        List<UnitBase> m_targetList = Global.unitManager.DamagedUnitData;
        int m_listCount = m_targetList.Count;

        bool isTarget = false;
        for (int i = 0; i < m_listCount; i++)
        {
            if (i % 2 == 0) // 인덱스가 짝수일 경우, 즉 공격자 데이터인 경우
            {
                if (m_targetList[i].player == player) isTarget = true; // 플레이어가 같은지 확인 후 타겟 확인
            }
            else
            {
                if (isTarget) attackTargetList.Add(m_targetList[i]); // 홀수일 경우 타겟이라면 리스트에 추가
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
