using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShield : UnitBase
{
    private int attackDamage = 2;
    private int moveDistanceBuffer = 2;
    [SerializeField]
    private List<int> allyHpList = new List<int>();
    //[SerializeField]
    //private Vector2[] attackRange = new Vector2[] {new Vector2(1,1), new Vector2(0,1), new Vector2(-1,-1)};
    private void moveIfAllyDamaged()
    {
        List<int> _allyHpList = getAlliesHp();
        // 두 리스트의 크기가 달라진 경우는 달라진 것이므로 함수 실행, 단 초기 상태 제외
        if (allyHpList.Count != _allyHpList.Count && allyHpList.Count != 0) moveToRandomAlly();
        // 두 리스트의 크기가 같은 경우 값을 비교
        else
        {
            int listCount = allyHpList.Count;
            int equalCount = 0;
            for (int i = 0; i < listCount; i++)
            {
                if (allyHpList[i] == _allyHpList[i]) equalCount++;
            }
            if (equalCount != listCount) moveToRandomAlly();
        }
        allyHpList = _allyHpList;
    }
    /// <summary>
    /// 아군의 Hp를 받는 함수
    /// </summary>
    /// <returns>아군 Hp를 담은 리스트를 반환</returns>
    private List<int> getAlliesHp()
    {
        List<UnitBase> targetList = new List<UnitBase>();
        List<int> _allyHpList = new List<int>();
        if (player == Player.Player1) targetList = Global.unitManager.P1UnitList;
        else if (player == Player.Player2) targetList = Global.unitManager.P2UnitList;

        int count = targetList.Count;
        for (int i = 0; i < count; i++)
        {
            _allyHpList.Add(targetList[i].hp.GetHp());
        }
        return _allyHpList;
    }
    /// <summary>
    /// 랜덤 아군 유닛으로 이동하는 함수
    /// </summary>
    private void moveToRandomAlly()
    {
        //랜덤 유닛 고르기, 타겟 유닛이 이 유닛이라면 다시 고름
        UnitBase targetUnit = null;
        while(true)
        {
            if (player == Player.Player1)
                targetUnit = Global.unitManager.P1UnitList[Random.Range(0, Global.unitManager.P1UnitList.Count)];
            else
                targetUnit = Global.unitManager.P2UnitList[Random.Range(0, Global.unitManager.P2UnitList.Count)];
            if (targetUnit == this) continue;
            else break;
        }
        
        //타겟 유닛으로 이동
        Vector2 _targetPos = Vector2.zero;
        Vector3 dirVec = targetUnit.lastMoveDir;
        _targetPos = targetUnit.transform.position + dirVec;

        List<UnitBase> targetList = new List<UnitBase>();
        int targetListCount = 0;
        int count = 0;
        while(true)
        {
            if(count % 2 == 0) targetList = Global.unitManager.P1UnitList;
            else targetList = Global.unitManager.P2UnitList;
            targetListCount = targetList.Count;

            //겹치는 부분이 없다면 true를 반환, 아니라면 false를 반환
            bool listSearch()
            {
                for (int i = 0; i < targetListCount; i++)
                {
                    UnitBase unit = targetList[i];
                    if (unit == this) continue;
                    else if (unit.transform.position == (Vector3)_targetPos)
                    {
                        _targetPos = unit.transform.position + dirVec;
                        return false;
                    }
                }
                return true;
            }
            bool isEnd = listSearch();
            //리스트 두개 모두 탐색 완료시 break
            if (count >= 2) break;
            //리스트 두개 모두 탐색 미완료시 해당 리스트의 탐색이 끝났는지 함수의 리턴값으로 판별
            if (isEnd == true) count++;
            else continue;
        }
        transform.position = _targetPos;
        //겹치는 버그 있는데 아직 못고침
        //아군 유닛이든 적군 유닛이든 해당 위치 앞에 유닛이 있다면 겹침
        //이유를 찾음 인덱스가 더 낮은 유닛이 앞에 있는 경우 그 유닛은 탐색하지 않음
        //반복문으로 활용해야할듯
    }
    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        lastMoveDir = dir;
        dir = (invertMovement ? -dir : dir) * moveDistance;
        MoveRelative(dir);
    }
    //protected override void Attack()
    //{
    //    if (!matchMode || turnCount <= 0) return;
    //    turnCount--;
    //    foreach (var item in attackRange)
    //    {
    //        Vector2 target = transform.position * Vector2.one + lastMoveDir * item; //공격할 위치
    //        Global.atkPooler.Get().Attack(target, info); //공격
    //    }
    //}
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] { new (1, 1), new (0, 1), new (-1, -1) },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
        Global.OnTurnStart += moveIfAllyDamaged;
    }
}
