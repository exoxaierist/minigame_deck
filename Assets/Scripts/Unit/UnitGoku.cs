using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGoku : UnitBase
{
    //�Ѹ� �з� �����̸� �������� ��ȯ�ſ�..
    //��ü�� ������ ���尡 ������
    private int attackDamage = 6;
    private int moveDistanceBuffer = 2;
    //���� ������ �������� ���� ���� ���� �������� �ۼ�
    //[SerializeField]
    //private Vector2[] attackRange = new Vector2[] { new(1, 1), new(1, 0), new(1, -1) };
    [SerializeField]
    GameObject gokuClonePrefab;
    
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
    //    AttackInfo info = new() //���� ����
    //    {
    //        damage = 6,
    //        attacker = this
    //    };
    //    int attackAmount = attackRange.Length;
    //    foreach (Vector2 target in Global.RotateAttackPattern(attackRange,lastMoveDir))
    //    {
    //        Global.atkPooler.Get().Attack(target, info); //����
    //    }
    //    for(int i = 0; i < attackAmount; i++)
    //    {
    //        Vector2 target = transform.position * Vector2.one + lastMoveDir * attackRange[i]; //������ ��ġ
    //        Global.atkPooler.Get().Attack(target, info); //����
    //    }
    //}
    private void MakingClone()
    {
        Vector3[] clonePos = new Vector3[2];
        List<Vector3> stackedPos = new List<Vector3>();
        int cloneAmount = 2;
        //������ ���
        if(lastMoveDir.x < 0)
        {
            clonePos[0] = transform.position + new Vector3(1, 1, 0);
            clonePos[1] = transform.position + new Vector3(1, -1, 0);
        }
        //�������� ���
        else if(lastMoveDir.x > 0)
        {
            clonePos[0] = transform.position + new Vector3(-1, 1, 0);
            clonePos[1] = transform.position + new Vector3(-1, -1, 0);
        }
        //������ ���
        else if(lastMoveDir.y > 0)
        {
            clonePos[0] = transform.position + new Vector3(1, -1, 0);
            clonePos[1] = transform.position + new Vector3(-1, -1, 0);
        }
        //�Ʒ����� ���
        else
        {
            clonePos[0] = transform.position + new Vector3(1, 1, 0);
            clonePos[1] = transform.position + new Vector3(-1, 1, 0);
        }
        int indexCount = Global.unitManager.P1UnitList.Count;
        //�̱����� ������ �����Ͽ� ����� ���ҽ��� ���� ����
        List<UnitBase> p1UnitList = Global.unitManager.P1UnitList;
        List<UnitBase> p2UnitList = Global.unitManager.P2UnitList;
        
        //P1 ���ֵ��� �ش� �ڸ��� �ִ��� Ž��
        for (int i = 0; i < indexCount; i++)
        {
            Vector3 unitPos = p1UnitList[i].transform.position;
            if (unitPos == clonePos[0])
            {
                cloneAmount--;
                stackedPos.Add(unitPos);
            }
            if (unitPos == clonePos[1])
            {
                cloneAmount--;
                stackedPos.Add(unitPos);
            }
        }
        indexCount = Global.unitManager.P2UnitList.Count;
        //�̱����� ������ �����Ͽ� ����� ���ҽ��� ���� ����
        //P2 ���ֵ��� �ش� �ڸ��� �ִ��� Ž��
        for (int i = 0; i < indexCount; i++)
        {
            Vector3 unitPos = p2UnitList[i].transform.position;
            if (unitPos == clonePos[0])
            {
                cloneAmount--;
                stackedPos.Add(unitPos);
            }
            if (unitPos == clonePos[1])
            {
                cloneAmount--;
                stackedPos.Add(unitPos);
            }
        }
        if(cloneAmount > 0)
        {
            for(int i = 0; i < cloneAmount; i++)
            {
                GameObject clone = Instantiate(gokuClonePrefab);
                if (clone.TryGetComponent<UnitGokuClone>(out UnitGokuClone cloneScript))
                {
                    cloneScript.Summon(player);
                }
                else Debug.LogError("Can't Get UnitGokuClone Script From Clone");
                //��ģ �ڸ��� �н��� ��ȯ�� �ڸ��� ���� �ʴٸ� �� �ڸ��� �̵�
                //��ģ �ڸ��� ��� ��������
                if (stackedPos != null || clonePos[i] != stackedPos[i])
                {
                    clone.transform.position = clonePos[i];
                }
                //��ģ �ڸ��� �н��� ��ȯ�� �ڸ��� ���ٸ� ���� ��ġ�� �̵�
                else
                {
                    clone.transform.position = clonePos[i + 1];
                }
            }
        }
    }
    protected override void Awake()
    {
        base.Awake();
        Global.OnRoundStart += MakingClone;
        UnitPropertiesSet(new Vector2[] { new(1, 1), new(1, 0), new(1, -1) },
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
    }
}
