using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    //���� ������ ��� ����
    //Ŭ������ ��ȯ�Ѵٸ� ���ڸ� ȭ�鿡 �׸���
    //��ȯ �Լ��� ���ʿ� �ڵ带 ������
    //������ ������ ������ Ȥ�� 3X3
    public static UnitManager Instance;
    public List<UnitBase> P1UnitList;
    public List<UnitBase> P2UnitList;
    //public List<UnitBase> AllUnitList; // ��� ������ ����Ʈ, ������� ����

    public List<UnitBase> DamagedUnitData; // ¦�� �ε����� ������ UnitBase Ȧ�� �ε����� �ǰ��� UnitBase

    //�÷��̾�� ���� ���ֵ��� ����Ʈ
    public List<UnitBase> P1DeadUnitList;
    public List<UnitBase> P2DeadUnitList;

    //[HideInInspector]
    //public List<int> P1AllyCountPerUnit; // P1�� ������ �ε������� ��ó �Ʊ��� ���� ����, ������� ����
    //[HideInInspector]
    //public List<int> P2AllyCountPerUnit; // P2�� ������ �ε������� ��ó �Ʊ��� ���� ����, ������� ����
    //[HideInInspector]
    //public List<int> P1EnemyCountPerUnit; // P1�� ������ �ε������� ��ó ������ ���� ����, ������� ����
    //[HideInInspector]
    //public List<int> P2EnemyCountPerUnit; // P2�� ������ �ε������� ��ó ������ ���� ����, ������� ����

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
    // p1 ���ָ���Ʈ�� ���� �߰�
    public void AddToP1Units(UnitBase unit)
    {
        if (P1UnitList.Contains(unit)) return;
        P1UnitList.Add(unit);
        unit.hp.OnDeath -= CheckP1Win;
        unit.hp.OnDeath += CheckP2Win;
    }

    // p2 ���ָ���Ʈ�� ���� �߰�
    public void AddToP2Units(UnitBase unit)
    {
        if (P2UnitList.Contains(unit)) return;
        P2UnitList.Add(unit);
        unit.hp.OnDeath -= CheckP2Win;
        unit.hp.OnDeath += CheckP1Win;
    }

    // ��� ���� hp�� ���°����� ����
    public void ResetAllUnits()
    {

    }
    /// <summary>
    /// ���ֵ��� �츮�� �Լ�
    /// </summary>
    /// <param name="_units">�μ����� UnitBase�� ����Ʈ ����</param>
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
    
    // p1 �̰���� Ȯ��, p2���� ���������� �ߵ�
    public void CheckP1Win(UnitBase unit)
    {
        if (!Global.roundManager.roundActive) return;
        foreach (UnitBase unitBase in P2UnitList)
        {
            if (!unitBase.hp.isDead) return;
        }
        Global.OnP1Win?.Invoke();
    }

    // p2 �̰���� Ȯ��, p1���� ���������� �ߵ�
    public void CheckP2Win(UnitBase unit)
    {
        if (!Global.roundManager.roundActive) return;
        foreach (UnitBase unitBase in P1UnitList)
        {
            if (!unitBase.hp.isDead) return;
        }
        Global.OnP2Win?.Invoke();
    }

    // ������ �Ʊ� ���ֵ� ��ȯ(int �ε���)
    // ������ ���� ���ֵ� ��ȯ(int �ε���)
    // ���� ����� �Ʊ� ���� ��ȯ(int �ε���)

    /// <summary>
    /// ����� �Ʊ����� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_index">�Լ��� �θ��� ������ �ε���, _index�� GetMyIndex ���</param>
    /// <param name="_player">������ ������ �÷��̾�, player</param>
    /// <returns>����� �Ʊ��� Type = List<UnitBase></returns>
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
    /// ���� ����� �Ʊ��� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_index">�Լ��� �θ��� ������ �ε���, _index�� GetMyIndex ���</param>
    /// <param name="_player">������ ������ �÷��̾�, player</param>
    /// <returns>���� ����� �Ʊ� Type = UnitBase</returns>
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
    /// ���� �� �Ʊ��� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_index">�Լ��� �θ��� ������ �ε���, _index�� GetMyIndex ���</param>
    /// <param name="_player">������ ������ �÷��̾�, player</param>
    /// <returns>���� �� �Ʊ� Type = UnitBase</returns>
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
    /// ����� �������� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_index">�Լ��� �θ��� ������ �ε���, _index�� GetMyIndex ���</param>
    /// <param name="_player">������ ������ �÷��̾�, player</param>
    /// <returns>����� ������ Type = List<UnitBase></returns>
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
    /// ���� ����� ������ ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_index">�Լ��� �θ��� ������ �ε���, _index�� GetMyIndex ���</param>
    /// <param name="_player">������ ������ �÷��̾�, player</param>
    /// <returns>���� ����� ���� Type = UnitBase</returns>
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
    /// ���� �� ������ ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_index">�Լ��� �θ��� ������ �ε���, _index�� GetMyIndex ���</param>
    /// <param name="_player">������ ������ �÷��̾�, player</param>
    /// <returns>���� �� ���� Type = UnitBase</returns>
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
    /// ���� ����� ������ ��ȯ���ִ� �Լ�
    /// </summary>
    /// <param name="_index">�Լ��� �θ��� ������ �ε���, _index�� GetMyIndex ���</param>
    /// <param name="_player">������ ������ �÷��̾�, player</param>
    /// <returns>���� ����� ���� Type = UnitBase</returns>
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
    /// ������ �ε����� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="_unit">�Լ��� �θ��� ����, this</param>
    /// <param name="_player">������ ������ �÷��̾�, player</param>
    /// <returns>������ �ε����� ��ȯ</returns>
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
    /// ������ ��ó ��, �Ʊ��� ���� ��ȯ����
    /// </summary>
    /// <param name="_index">������ �ε���, �ε����� GetMyIndex ���</param>
    /// <param name="_player">������ ������ �÷��̾�, player</param>
    /// <returns>���� �ֺ��� �ִ� �� Ȥ�� �Ʊ��� �� Type = List<int></returns>
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
    /// ���� ������ �Ʊ��� ��ȯ���ִ� �Լ�, �� �� ��ġ�� ������ ���� ���� ����
    /// </summary>
    /// <param name="_player">������ ������ �÷��̾�, player</param>
    /// <returns>���� ������ �Ʊ� UnitBase</returns>
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
        // ���� �������� ���� deadList ����

        //Ÿ�� ���� ���� ������ ���������� �ݺ�
        int cnt = 0; // �ʹ� ���� �ݺ��ؼ� ���� ������ ���� ��� Ż��� ī����
        List<UnitBase> targetedUnits = new List<UnitBase>(); // Ÿ���� �Ǿ��� ���ֵ��� ����Ʈ
        while(true)
        {
            //Ÿ�� ����Ʈ�� ����ִ� ���
            if (targetListCount == 0) return null;

            cnt++;
            //�÷��̾�� ���� ������ 100���� ���� ���ٰ� ����
            if(cnt > 200) return null;
            // Ÿ�� ����Ʈ���� ���� Ÿ�� ������ ����
            UnitBase target = targetList[UnityEngine.Random.Range(0, targetListCount)];
            // Ÿ�� ����Ʈ�� ������ Ÿ���� �� ������ ������ ���ų� ũ�ٸ� null�� ��ȯ�ϸ� ����
            if(targetedUnits.Count >= targetListCount)
            {
                Debug.LogError("Units Exist On All Of Dead Unit");
                return null;
            }
            // Ÿ�� ������ �̹� ���� Ÿ�ٵ� ���� �ִٸ� ��������
            if (targetedUnits.Contains(target)) continue;
            // �ƴ϶�� ����Ʈ�� �߰�
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
                    // ���� ���̾��� ���
                    if (playerList[i].gameObject.layer == 9) continue;
                    // ���� �������� ���� �ڵ� ����ȭ
                    //if (playerList[i].TryGetComponent<Hp>(out Hp hp))
                    //{
                    //    // �ش� ������ �׾��ִٸ� ���� �����Ƿ� ���� �ε�����
                    //    if (hp.isDead) continue;
                    //}
                    // �ش� ������ �ٸ� ���ְ� �����ִٸ� false�� ��ȯ�Ͽ� �Լ� ����
                    // �ٸ� �������� Ÿ���� �����ؼ� �ٽ� Ž���ϱ� ����
                    if (targetPos == playerList[i].gameObject.transform.position) return false;
                }
                // Ž���� ���� �Ͽ��µ� Ÿ���� ��ġ�� �κ��� ���� ��� true�� ��ȯ�ϸ� Ž�� ����
                return true;
            }
            if (ListSearch(Player.Player1) == false) continue;
            if (ListSearch(Player.Player2) == false) continue;

            // �ٸ� ���ְ� ���� ���� �ʴٸ� target�� ��ȯ
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
