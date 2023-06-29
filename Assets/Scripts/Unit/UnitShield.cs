using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitShield : UnitBase
{
    private int attackDamage = 2;
    private AttackInfo info;
    [SerializeField]
    private Vector2[] attackRange = new Vector2[] {new Vector2(1,1), new Vector2(0,1), new Vector2(-1,-1)};
    private UnitBase getDamagedAlly()
    {
        //������ �������� ��� ã�����ΰ�?
        return null;
    }
    /// <summary>
    /// ���� �Ʊ� �������� �̵��ϴ� �Լ�
    /// </summary>
    private void moveToRandomAlly()
    {
        //���� ���� ����
        UnitBase _targetUnit = null;
        if (player == Player.Player1)
            _targetUnit = Global.unitManager.P1UnitList[Random.Range(0, Global.unitManager.P1UnitList.Count)];
        else
            _targetUnit = Global.unitManager.P2UnitList[Random.Range(0, Global.unitManager.P2UnitList.Count)];

        //Ÿ�� �������� �̵�
        Vector2 _targetPos = Vector2.zero;
        if (_targetUnit.lastMoveDir == Vector2.left)
        {
            _targetPos = _targetUnit.transform.position + new Vector3(-1, 0, 0);
            for (int i = 0; i < Global.unitManager.P1UnitList.Count; i++)
            {
                if (Global.unitManager.P1UnitList[i].transform.position == (Vector3)_targetPos)
                {
                    _targetPos = _targetUnit.transform.position + new Vector3(1, 0, 0);
                    break;
                }
            }
            for (int i = 0; i < Global.unitManager.P2UnitList.Count; i++)
            {
                if (Global.unitManager.P2UnitList[i].transform.position == (Vector3)_targetPos)
                {
                    _targetPos = _targetUnit.transform.position + new Vector3(1, 0, 0);
                    break;
                }
            }
        }
        if (_targetUnit.lastMoveDir == Vector2.right)
        {
            _targetPos = _targetUnit.transform.position + new Vector3(1, 0, 0);
            for (int i = 0; i < Global.unitManager.P1UnitList.Count; i++)
            {
                if (Global.unitManager.P1UnitList[i].transform.position == (Vector3)_targetPos)
                {
                    _targetPos = _targetUnit.transform.position + new Vector3(-1, 0, 0);
                    break;
                }
            }
            for (int i = 0; i < Global.unitManager.P2UnitList.Count; i++)
            {
                if (Global.unitManager.P2UnitList[i].transform.position == (Vector3)_targetPos)
                {
                    _targetPos = _targetUnit.transform.position + new Vector3(-1, 0, 0);
                    break;
                }
            }
        }
        if (_targetUnit.lastMoveDir == Vector2.up)
        {
            _targetPos = _targetUnit.transform.position + new Vector3(0, 1, 0);
            for (int i = 0; i < Global.unitManager.P1UnitList.Count; i++)
            {
                if (Global.unitManager.P1UnitList[i].transform.position == (Vector3)_targetPos)
                {
                    _targetPos = _targetUnit.transform.position + new Vector3(0, -1, 0);
                    break;
                }
            }
            for (int i = 0; i < Global.unitManager.P2UnitList.Count; i++)
            {
                if (Global.unitManager.P2UnitList[i].transform.position == (Vector3)_targetPos)
                {
                    _targetPos = _targetUnit.transform.position + new Vector3(0, -1, 0);
                    break;
                }
            }
        }
        if (_targetUnit.lastMoveDir == Vector2.down)
        {
            _targetPos = _targetUnit.transform.position + new Vector3(0, -1, 0);
            for (int i = 0; i < Global.unitManager.P1UnitList.Count; i++)
            {
                if (Global.unitManager.P1UnitList[i].transform.position == (Vector3)_targetPos)
                {
                    _targetPos = _targetUnit.transform.position + new Vector3(0, 1, 0);
                    break;
                }
            }
            for (int i = 0; i < Global.unitManager.P2UnitList.Count; i++)
            {
                if (Global.unitManager.P2UnitList[i].transform.position == (Vector3)_targetPos)
                {
                    _targetPos = _targetUnit.transform.position + new Vector3(0, 1, 0);
                    break;
                }
            }
        }
    }
    private void Move(Vector2 dir)
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        lastMoveDir = dir;
        dir = (invertMovement ? -dir : dir) * moveDistance;
        MoveRelative(dir);
    }
    protected override void Attack()
    {
        if (!matchMode || turnCount <= 0) return;
        turnCount--;
        foreach (var item in attackRange)
        {
            Vector2 target = transform.position * Vector2.one + lastMoveDir * item; //������ ��ġ
            Global.atkPooler.Get().Attack(target, info); //����
        }
    }
    protected override void Awake()
    {
        base.Awake();
        moveDistance = 2;
    }
}
