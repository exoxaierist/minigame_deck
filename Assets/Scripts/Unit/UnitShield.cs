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
        //���� ���� ����
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
        
        //Ÿ�� �������� �̵�
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
        //��ġ�� ���� �ִµ� ���� ����ħ
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
