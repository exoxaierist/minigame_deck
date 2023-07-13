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
        // �� ����Ʈ�� ũ�Ⱑ �޶��� ���� �޶��� ���̹Ƿ� �Լ� ����, �� �ʱ� ���� ����
        if (allyHpList.Count != _allyHpList.Count && allyHpList.Count != 0) moveToRandomAlly();
        // �� ����Ʈ�� ũ�Ⱑ ���� ��� ���� ��
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
    /// �Ʊ��� Hp�� �޴� �Լ�
    /// </summary>
    /// <returns>�Ʊ� Hp�� ���� ����Ʈ�� ��ȯ</returns>
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
    /// ���� �Ʊ� �������� �̵��ϴ� �Լ�
    /// </summary>
    private void moveToRandomAlly()
    {
        //���� ���� ����, Ÿ�� ������ �� �����̶�� �ٽ� ��
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
        
        //Ÿ�� �������� �̵�
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

            //��ġ�� �κ��� ���ٸ� true�� ��ȯ, �ƴ϶�� false�� ��ȯ
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
            //����Ʈ �ΰ� ��� Ž�� �Ϸ�� break
            if (count >= 2) break;
            //����Ʈ �ΰ� ��� Ž�� �̿Ϸ�� �ش� ����Ʈ�� Ž���� �������� �Լ��� ���ϰ����� �Ǻ�
            if (isEnd == true) count++;
            else continue;
        }
        transform.position = _targetPos;
        //��ġ�� ���� �ִµ� ���� ����ħ
        //�Ʊ� �����̵� ���� �����̵� �ش� ��ġ �տ� ������ �ִٸ� ��ħ
        //������ ã�� �ε����� �� ���� ������ �տ� �ִ� ��� �� ������ Ž������ ����
        //�ݺ������� Ȱ���ؾ��ҵ�
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
    //        Vector2 target = transform.position * Vector2.one + lastMoveDir * item; //������ ��ġ
    //        Global.atkPooler.Get().Attack(target, info); //����
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
