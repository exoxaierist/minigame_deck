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
    public List<UnitBase> AllUnitList;

    [HideInInspector]
    public List<int> P1AllyCountPerUnit; // P1의 유닛이 인덱스마다 근처 아군의 수를 저장
    [HideInInspector]
    public List<int> P2AllyCountPerUnit; // P2의 유닛이 인덱스마다 근처 아군의 수를 저장
    [HideInInspector]
    public List<int> P1EnemyCountPerUnit; // P1의 유닛이 인덱스마다 근처 적군의 수를 저장
    [HideInInspector]
    public List<int> P2EnemyCountPerUnit; // P2의 유닛이 인덱스마다 근처 적군의 수를 저장

    public Action OnP1Death;
    public Action OnP2Death;

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
                P1AllyCountPerUnit[_index] = closeUnitList.Count;
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                for (int i = 0; i < P2UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    float distance = Vector3.Magnitude(standardPos - P2UnitList[i].transform.position);
                    if (distance < 1.1f) closeUnitList.Add(P2UnitList[i]);
                }
                P2AllyCountPerUnit[_index] = closeUnitList.Count;
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
                P1EnemyCountPerUnit[_index] = closeUnitList.Count;
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                for (int i = 0; i < P1UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    float distance = Vector3.Magnitude(standardPos - P1UnitList[i].transform.position);
                    if (distance < 1.1f) closeUnitList.Add(P1UnitList[i]);
                }
                P2EnemyCountPerUnit[_index] = closeUnitList.Count;
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
    /// <param name="_data">근처에서 찾을 유닛의 정보, 적군인지 아군인지 Type = AllyOrEnemy</param>
    /// <param name="_index">유닛의 인덱스, 인덱스는 GetMyIndex 사용</param>
    /// <param name="_player">유닛을 소유한 플레이어, player</param>
    /// <returns>유닛 주변에 있는 적 혹은 아군의 수 Type = List<int></returns>
    public List<int> GetMyUnitCount(int _index, Player _player)
    {
        List<int> list = new List<int>();
        switch(_player)
        {
            case Player.Player1:
                list.Add(P1AllyCountPerUnit[_index]);
                list.Add(P1EnemyCountPerUnit[_index]);
                break;
            case Player.Player2:
                list.Add(P2AllyCountPerUnit[_index]);
                list.Add(P2EnemyCountPerUnit[_index]);
                break;
        }
        return list;
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
    }
}
