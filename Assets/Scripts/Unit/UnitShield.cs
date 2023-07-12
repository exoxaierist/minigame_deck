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
        //랜덤 유닛 고르기
        UnitBase _targetUnit = null;
        while(true)
        {
            if (player == Player.Player1)
                _targetUnit = Global.unitManager.P1UnitList[Random.Range(0, Global.unitManager.P1UnitList.Count)];
            else
                _targetUnit = Global.unitManager.P2UnitList[Random.Range(0, Global.unitManager.P2UnitList.Count)];
            if (_targetUnit == this) continue;
            else break;
        }
        
        //타겟 유닛으로 이동
        Vector2 _targetPos = Vector2.zero;
        Vector3 dirVec = _targetUnit.lastMoveDir;
        _targetPos = _targetUnit.transform.position + dirVec;
        for (int i = 0; i < Global.unitManager.P1UnitList.Count; i++)
        {
            UnitBase targetP1Unit = Global.unitManager.P1UnitList[i];
            if (targetP1Unit == this) { }
            else if (targetP1Unit.transform.position == (Vector3)_targetPos)
            {
                _targetPos = _targetUnit.transform.position + dirVec;
            }
        }
        for (int i = 0; i < Global.unitManager.P2UnitList.Count; i++)
        {
            UnitBase targetP2Unit = Global.unitManager.P2UnitList[i];
            if (targetP2Unit == this) { }
            else if (targetP2Unit.transform.position == (Vector3)_targetPos)
            {
                _targetPos = _targetUnit.transform.position + dirVec;
            }
        }
        transform.position = _targetPos;
        //겹치는 버그 있는데 아직 못고침
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
