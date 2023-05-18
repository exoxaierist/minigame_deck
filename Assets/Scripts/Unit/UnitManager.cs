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

    // 인접한 아군 유닛들 반환(int 인덱스)
    // 인접한 적군 유닛들 반환(int 인덱스)
    // 가장 가까운 아군 유닛 반환(int 인덱스)

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
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                for (int i = 0; i < P2UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    float distance = Vector3.Magnitude(standardPos - P2UnitList[i].transform.position);
                    if (distance < 1.1f) closeUnitList.Add(P2UnitList[i]);
                }
                break;
        }
        return closeUnitList;
    }
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
                break;
            case Player.Player2:
                standardPos = P2UnitList[_index].transform.position;
                for (int i = 0; i < P1UnitList.Count; i++)
                {
                    if (_index == i) continue;
                    float distance = Vector3.Magnitude(standardPos - P1UnitList[i].transform.position);
                    if (distance < 1.1f) closeUnitList.Add(P1UnitList[i]);
                }
                break;
        }
        return closeUnitList;
    }
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
    }
}
