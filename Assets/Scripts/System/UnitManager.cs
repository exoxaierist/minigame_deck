using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //유닛 생성을 어떻게 할지
    //클릭으로 소환한다면 격자를 화면에 그릴지
    //소환 함수를 이쪽에 코드를 만들지
    //인접의 기준이 인접만 혹은 3X3
    public static UnitManager Instance;
    public List<UnitBase> P1UnitList;
    public List<UnitBase> P2UnitList;
    //public List<UnitBase> AllUnitList; // 모든 유닛의 리스트, 사용하지 않음

    public List<UnitBase> DamagedUnitData; // 짝수 인덱스는 공격자 UnitBase 홀수 인덱스는 피격자 UnitBase

    //플레이어마다 죽은 유닛들의 리스트
    public List<UnitBase> P1DeadUnitList;
    public List<UnitBase> P2DeadUnitList;

    //[HideInInspector]
    //public List<int> P1AllyCountPerUnit; // P1의 유닛이 인덱스마다 근처 아군의 수를 저장, 사용하지 않음
    //[HideInInspector]
    //public List<int> P2AllyCountPerUnit; // P2의 유닛이 인덱스마다 근처 아군의 수를 저장, 사용하지 않음
    //[HideInInspector]
    //public List<int> P1EnemyCountPerUnit; // P1의 유닛이 인덱스마다 근처 적군의 수를 저장, 사용하지 않음
    //[HideInInspector]
    //public List<int> P2EnemyCountPerUnit; // P2의 유닛이 인덱스마다 근처 적군의 수를 저장, 사용하지 않음

    public Action OnP1Death;
    public Action OnP2Death;

    public void DamagedListReset()
    {
        DamagedUnitData.Clear();
    }
    public void DeadUnitListReset()
    {
        P1DeadUnitList.Clear();
        P2DeadUnitList.Clear();
    }
    // p1 유닛리스트에 유닛 추가
    public void AddToP1Units(UnitBase unit)
    {
        if (P1UnitList.Contains(unit)) return;
        P1UnitList.Add(unit);
        unit.hp.OnDeath -= CheckP1Win;
        unit.hp.OnDeath += CheckP2Win;
    }

    // p2 유닛리스트에 유닛 추가
    public void AddToP2Units(UnitBase unit)
    {
        if (P2UnitList.Contains(unit)) return;
        P2UnitList.Add(unit);
        unit.hp.OnDeath -= CheckP2Win;
        unit.hp.OnDeath += CheckP1Win;
    }

    // 모든 유닛 hp나 상태같은거 리셋
    public void ResetAllUnits()
    {

    }
    /// <summary>
    /// 유닛들을 살리는 함수
    /// </summary>
    /// <param name="_units">인수값은 UnitBase의 리스트 형태</param>
    public void ReviveUnits(List<UnitBase> _units)
    {
        int count = _units.Count;
        for(int i = 0; i < count; i++)
        {
            _units[i].hp.OnRevive(_units[i]);
            //if (_units[i].TryGetComponent<Hp>(out Hp hp)) hp.ResetHP();
            //else Debug.LogError("Can't Get Hp Component From UnitBase");
        }
    }
    
    // p1 이겼는지 확인, p2유닛 죽을때마다 발동
    public void CheckP1Win(UnitBase unit)
    {
        if (!Global.roundManager.roundActive) return;
        foreach (UnitBase unitBase in P2UnitList)
        {
            if (!unitBase.hp.isDead) return;
        }
        Global.OnP1Win?.Invoke();
    }

    // p2 이겼는지 확인, p1유닛 죽을때마다 발동
    public void CheckP2Win(UnitBase unit)
    {
        if (!Global.roundManager.roundActive) return;
        foreach (UnitBase unitBase in P1UnitList)
        {
            if (!unitBase.hp.isDead) return;
        }
        Global.OnP2Win?.Invoke();
    }

    // 인접한 아군 유닛들 반환(int 인덱스)
    // 인접한 적군 유닛들 반환(int 인덱스)
    // 가장 가까운 아군 유닛 반환(int 인덱스)

    /// <summary>
    /// 가까운 아군들을 반환하는 함수
    /// </summary>
    /// <param name="_index">함수를 부르는 유닛의 인덱스, _index는 GetMyIndex 사용</param>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>가까운 아군들 Type = List<UnitBase></returns>
    public List<UnitBase> GetCloseAllies(int _index, Player _player)
    {
        List<UnitBase> closeUnitList = new List<UnitBase>();
        Vector3 standardPos = Vector3.zero;
        switch (_player)
        {
            case Player.Player1:
                standardPos = P1UnitList[_index].transform.position;
                for (int i = 0; i < P1UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    float distance = Vector3.Magnitude(standardPos - P1UnitList[i].transform.position);
                    if (distance < 1.1f) closeUnitList.Add(P1UnitList[i]);
                }
                //P1AllyCountPerUnit[_index] = closeUnitList.Count;
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                for (int i = 0; i < P2UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    float distance = Vector3.Magnitude(standardPos - P2UnitList[i].transform.position);
                    if (distance < 1.1f) closeUnitList.Add(P2UnitList[i]);
                }
                //P2AllyCountPerUnit[_index] = closeUnitList.Count;
                break;
        }
        return closeUnitList;
    }
    /// <summary>
    /// 가장 가까운 아군을 반환하는 함수
    /// </summary>
    /// <param name="_index">함수를 부르는 유닛의 인덱스, _index는 GetMyIndex 사용</param>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>가장 가까운 아군 Type = UnitBase</returns>
    public UnitBase GetClosestAlly(int _index, Player _player)
    {
        float distance = float.PositiveInfinity;
        Vector3 standardPos = Vector3.zero;
        UnitBase closestAlly = null;
        switch (_player)
        {
            case Player.Player1:
                standardPos = P1UnitList[_index].transform.position;
                for (int i = 0; i < P1UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    Vector3 targetPos = P1UnitList[i].transform.position;
                    if(distance > Vector3.Magnitude(standardPos - targetPos))
                    {
                        distance = Vector3.Magnitude(standardPos - targetPos);
                        closestAlly = P1UnitList[i];
                    }
                }
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                for (int i = 0; i < P2UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    Vector3 targetPos = P2UnitList[i].transform.position;
                    if (distance > Vector3.Magnitude(standardPos - targetPos))
                    {
                        distance = Vector3.Magnitude(standardPos - targetPos);
                        closestAlly = P2UnitList[i];
                    }
                }
                break;
        }
        return closestAlly;
    }
    public UnitBase GetFarthestEnemyInCross(int _index, Player _player)
    {
        float distance = 0f;
        Vector3 standardPos = Vector3.zero;
        UnitBase farthestEnemy = null;
        switch (_player)
        {
            case Player.Player1:
                standardPos = P1UnitList[_index].gameObject.transform.position;
                for (int i = 0; i < P2UnitList.Count; i++)
                {
                    Vector3 targetPos = P2UnitList[i].gameObject.transform.position;
                    if (standardPos.x != targetPos.x && standardPos.y != targetPos.y) continue;
                    float newDistance = Vector3.Magnitude(standardPos - targetPos);
                    if (distance < newDistance)
                    {
                        distance = newDistance;
                        farthestEnemy = P2UnitList[i];
                    }
                }
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].gameObject.transform.position;
                for (int i = 0; i < P1UnitList.Count; i++)
                {
                    Vector3 targetPos = P1UnitList[i].gameObject.transform.position;
                    if (standardPos.x != targetPos.x && standardPos.y != targetPos.y) continue;
                    float newDistance = Vector3.Magnitude(standardPos - targetPos);
                    if (distance < newDistance)
                    {
                        distance = newDistance;
                        farthestEnemy = P1UnitList[i];
                    }
                }
                break;
        }
        return farthestEnemy;
    }
    /// <summary>
    /// 가장 먼 아군을 반환하는 함수
    /// </summary>
    /// <param name="_index">함수를 부르는 유닛의 인덱스, _index는 GetMyIndex 사용</param>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>가장 먼 아군 Type = UnitBase</returns>
    public UnitBase GetFarthestAlly(int _index, Player _player)
    {
        float distance = 0f;
        Vector3 standardPos = Vector3.zero;
        UnitBase farthestAlly = null;
        switch (_player)
        {
            case Player.Player1:
                standardPos = P1UnitList[_index].transform.position;
                for (int i = 0; i < P1UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    Vector3 targetPos = P1UnitList[i].transform.position;
                    if (distance < Vector3.Magnitude(standardPos - targetPos))
                    {
                        distance = Vector3.Magnitude(standardPos - targetPos);
                        farthestAlly = P1UnitList[i];
                    }
                }
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                for (int i = 0; i < P2UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    Vector3 targetPos = P2UnitList[i].transform.position;
                    if (distance < Vector3.Magnitude(standardPos - targetPos))
                    {
                        distance = Vector3.Magnitude(standardPos - targetPos);
                        farthestAlly = P2UnitList[i];
                    }
                }
                break;
        }
        return farthestAlly;
    }
    /// <summary>
    /// 가까운 적군들을 반환하는 함수
    /// </summary>
    /// <param name="_index">함수를 부르는 유닛의 인덱스, _index는 GetMyIndex 사용</param>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>가까운 적군들 Type = List<UnitBase></returns>
    public List<UnitBase> GetCloseEnemies(int _index, Player _player)
    {
        List<UnitBase> closeUnitList = new List<UnitBase>();
        Vector3 standardPos = Vector3.zero;
        switch (_player)
        {
            case Player.Player1:
                Vector3 targetPos = P1UnitList[_index].transform.position;
                for (int i = 0; i < P2UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    float distance = Vector3.Magnitude(standardPos - P2UnitList[i].transform.position);
                    if (distance < 1.1f) closeUnitList.Add(P2UnitList[i]);
                }
                //P1EnemyCountPerUnit[_index] = closeUnitList.Count;
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                for (int i = 0; i < P1UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    float distance = Vector3.Magnitude(standardPos - P1UnitList[i].transform.position);
                    if (distance < 1.1f) closeUnitList.Add(P1UnitList[i]);
                }
                //P2EnemyCountPerUnit[_index] = closeUnitList.Count;
                break;
        }
        return closeUnitList;
    }
    /// <summary>
    /// 가장 가까운 적군을 반환하는 함수
    /// </summary>
    /// <param name="_index">함수를 부르는 유닛의 인덱스, _index는 GetMyIndex 사용</param>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>가장 가까운 적군 Type = UnitBase</returns>
    public UnitBase GetClosestEnemy(int _index, Player _player)
    {
        Vector3 standardPos = Vector3.zero;
        float distance = float.PositiveInfinity;
        UnitBase closestAlly = null;
        switch (_player)
        {
            case Player.Player1:
                standardPos = P1UnitList[_index].transform.position;
                for (int i = 0; i < P2UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    Vector3 targetPos = P2UnitList[i].transform.position;
                    if (distance > Vector3.Magnitude(standardPos - targetPos))
                    {
                        distance = Vector3.Magnitude(standardPos - targetPos);
                        closestAlly = P2UnitList[i];
                    }
                }
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                for (int i = 0; i < P1UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    Vector3 targetPos = P1UnitList[i].transform.position;
                    if (distance > Vector3.Magnitude(standardPos - targetPos))
                    {
                        distance = Vector3.Magnitude(standardPos - targetPos);
                        closestAlly = P1UnitList[i];
                    }
                }
                break;
        }
        return closestAlly;
    }
    /// <summary>
    /// 가장 먼 적군을 반환하는 함수
    /// </summary>
    /// <param name="_index">함수를 부르는 유닛의 인덱스, _index는 GetMyIndex 사용</param>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>가장 먼 적군 Type = UnitBase</returns>
    public UnitBase GetFarthestEnemy(int _index, Player _player)
    {
        float distance = 0f;
        Vector3 standardPos = Vector3.zero;
        UnitBase farthestEnemy = null;
        switch (_player)
        {
            case Player.Player1:
                standardPos = P1UnitList[_index].transform.position;
                for (int i = 0; i < P1UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    Vector3 targetPos = P1UnitList[i].transform.position;
                    if (distance < Vector3.Magnitude(standardPos - targetPos))
                    {
                        distance = Vector3.Magnitude(standardPos - targetPos);
                        farthestEnemy = P1UnitList[i];
                    }
                }
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                for (int i = 0; i < P2UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    Vector3 targetPos = P2UnitList[i].transform.position;
                    if (distance < Vector3.Magnitude(standardPos - targetPos))
                    {
                        distance = Vector3.Magnitude(standardPos - targetPos);
                        farthestEnemy = P2UnitList[i];
                    }
                }
                break;
        }
        return farthestEnemy;
    }
    /// <summary>
    /// 가장 가까운 유닛을 반환해주는 함수
    /// </summary>
    /// <param name="_index">함수를 부르는 유닛의 인덱스, _index는 GetMyIndex 사용</param>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>가장 가까운 유닛 Type = UnitBase</returns>
    public UnitBase GetClosestUnit(int _index, Player _player)
    {
        Vector3 standardPos = Vector3.zero;
        float distance = float.PositiveInfinity;
        UnitBase closestUnit = null;
        switch (_player)
        {
            case Player.Player1:
                standardPos = P1UnitList[_index].transform.position;
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                break;
        }
        for (int i = 0; i < P1UnitList.Count; i++)
        {
            if (P1UnitList[i] == P1UnitList[_index]) continue;
            Vector3 targetPos = P1UnitList[i].transform.position;
            if (distance > Vector3.Magnitude(standardPos - targetPos))
            {
                distance = Vector3.Magnitude(standardPos - targetPos);
                closestUnit = P1UnitList[i];
            }
        }
        for (int i = 0; i < P2UnitList.Count; i++)
        {
            if (P2UnitList[i] == P2UnitList[_index]) continue;
            Vector3 targetPos = P2UnitList[i].transform.position;
            if (distance > Vector3.Magnitude(standardPos - targetPos))
            {
                distance = Vector3.Magnitude(standardPos - targetPos);
                closestUnit = P2UnitList[i];
            }
        }
        return closestUnit;
    }
    /// <summary>
    /// 유닛의 인덱스를 반환하는 함수
    /// </summary>
    /// <param name="_unit">함수를 부르는 유닛, this</param>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>유닛의 인덱스를 반환</returns>
    public int GetMyIndex(UnitBase _unit, Player _player)
    {
        switch(_player)
        {
            case Player.Player1:
                return P1UnitList.IndexOf(_unit);
            case Player.Player2:
                return P2UnitList.IndexOf(_unit);
        }
        return -1;
    }
    /// <summary>
    /// 유닛의 근처 적, 아군의 수를 반환해줌
    /// </summary>
    /// <param name="_index">유닛의 인덱스, 인덱스는 GetMyIndex 사용</param>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>유닛 주변에 있는 적 혹은 아군의 수 Type = List<int></returns>
    //public List<int> GetMyUnitCount(int _index, Player _player)
    //{
    //    List<int> list = new List<int>();
    //    switch(_player)
    //    {
    //        case Player.Player1:
    //            list.Add(P1AllyCountPerUnit[_index]);
    //            list.Add(P1EnemyCountPerUnit[_index]);
    //            break;
    //        case Player.Player2:
    //            list.Add(P2AllyCountPerUnit[_index]);
    //            list.Add(P2EnemyCountPerUnit[_index]);
    //            break;
    //    }
    //    return list;
        /*if(_data == AllyOrEnemy.Ally)
        {
            if(player == Player.Player1)
            {
                return P1AllyCountPerUnit[_index];
            }
            else if(player == Player.Player2)
            {
                return P2AllyCountPerUnit[_index];
            }
        }
        else if(_data == AllyOrEnemy.Enemy)
        {
            if(player == Player.Player1)
            {
                return P1EnemyCountPerUnit[_index];
            }
            else if(player == Player.Player2)
            {
                return P2EnemyCountPerUnit[_index];
            }
        }
        return -1;*/
    //}
    /// <summary>
    /// 죽은 랜덤한 아군을 반환해주는 함수, 단 그 위치에 적군이 있을 경우는 제외
    /// </summary>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>죽은 랜덤한 아군 UnitBase</returns>
    public UnitBase GetRandomDeadAlly(Player _player)
    {
        List<UnitBase> targetList = new List<UnitBase>();
        if (_player == Player.Player1) targetList = P1DeadUnitList;
        else if(_player == Player.Player2) targetList = P2DeadUnitList;

        int targetListCount = targetList.Count;
        //List<UnitBase> deadList = new List<UnitBase>();

        //int targetListCount = targetList.Count;
        //for(int i = 0; i < targetListCount; i++)
        //{
        //    if(targetList[i].TryGetComponent<Hp>(out Hp hp))
        //    {
        //        if (hp.isDead) deadList.Add(targetList[i]);
        //    }
        //    else
        //    {
        //        Debug.LogError("Can't Get Hp from UnitList");
        //    }
        //}
        // 구조 변경으로 인한 deadList 삭제

        //타겟 유닛 위에 유닛을 없을때까지 반복
        int cnt = 0; // 너무 많이 반복해서 무한 루프에 빠진 경우 탈출용 카운터
        List<UnitBase> targetedUnits = new List<UnitBase>(); // 타겟이 되었던 유닛들의 리스트
        while(true)
        {
            //타겟 리스트가 비어있는 경우
            if (targetListCount == 0) return null;

            cnt++;
            //플레이어당 죽은 유닛이 100개일 경우는 없다고 배제
            if(cnt > 200) return null;
            // 타겟 리스트에서 랜덤 타겟 유닛을 받음
            UnitBase target = targetList[UnityEngine.Random.Range(0, targetListCount)];
            // 타겟 리스트의 수보다 타겟이 된 유닛의 갯수가 같거나 크다면 null을 반환하며 종료
            if(targetedUnits.Count >= targetListCount)
            {
                Debug.LogError("Units Exist On All Of Dead Unit");
                return null;
            }
            // 타겟 유닛이 이미 전에 타겟된 적이 있다면 다음으로
            if (targetedUnits.Contains(target)) continue;
            // 아니라면 리스트에 추가
            else targetedUnits.Add(target);
            Vector3 targetPos = target.gameObject.transform.position;

            bool ListSearch(Player _player)
            {
                List<UnitBase> playerList = new List<UnitBase>();

                if (_player == Player.Player1) playerList = P1UnitList;
                else if (_player == Player.Player2) playerList = P2UnitList;

                int playerListCount = playerList.Count;

                for (int i = 0; i < playerListCount; i++)
                {
                    // 죽은 레이어인 경우
                    if (playerList[i].gameObject.layer == 9) continue;
                    // 구조 변경으로 인한 코드 간편화
                    //if (playerList[i].TryGetComponent<Hp>(out Hp hp))
                    //{
                    //    // 해당 유닛이 죽어있다면 관계 없으므로 다음 인덱스로
                    //    if (hp.isDead) continue;
                    //}
                    // 해당 유닛이 다른 유닛과 겹쳐있다면 false를 반환하여 함수 종료
                    // 다른 유닛으로 타겟을 설정해서 다시 탐색하기 위함
                    if (targetPos == playerList[i].gameObject.transform.position) return false;
                }
                // 탐색을 전부 하였는데 타겟이 겹치는 부분이 없는 경우 true를 반환하며 탐색 종료
                return true;
            }
            if (ListSearch(Player.Player1) == false) continue;
            if (ListSearch(Player.Player2) == false) continue;

            // 다른 유닛과 겹쳐 있지 않다면 target을 반환
            return target;
        }
    }
    private void singletoneCheck()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Awake()
    {
        singletoneCheck();
        Global.unitManager = this;
        Global.OnTurnStartLate += DamagedListReset;
        Global.OnRoundEnd += DeadUnitListReset;
    }
}
