using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitGoku : UnitBase
{
    //한명씩 밀려 움직이면 여러명이 소환돼요..
    //본체가 죽으면 라운드가 끝나요
    private int attackDamage = 6;
    private int moveDistanceBuffer = 2;
    //공격 범위는 오른쪽을 보고 있을 때를 기준으로 작성
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
    //    AttackInfo info = new() //공격 정보
    //    {
    //        damage = 6,
    //        attacker = this
    //    };
    //    int attackAmount = attackRange.Length;
    //    foreach (Vector2 target in Global.RotateAttackPattern(attackRange,lastMoveDir))
    //    {
    //        Global.atkPooler.Get().Attack(target, info); //공격
    //    }
    //    for(int i = 0; i < attackAmount; i++)
    //    {
    //        Vector2 target = transform.position * Vector2.one + lastMoveDir * attackRange[i]; //공격할 위치
    //        Global.atkPooler.Get().Attack(target, info); //공격
    //    }
    //}
    private void MakingClone()
    {
        Vector3[] clonePos = new Vector3[2];
        List<Vector3> stackedPos = new List<Vector3>();
        int cloneAmount = 2;
        //왼쪽일 경우
        if(lastMoveDir.x < 0)
        {
            clonePos[0] = transform.position + new Vector3(1, 1, 0);
            clonePos[1] = transform.position + new Vector3(1, -1, 0);
        }
        //오른쪽일 경우
        else if(lastMoveDir.x > 0)
        {
            clonePos[0] = transform.position + new Vector3(-1, 1, 0);
            clonePos[1] = transform.position + new Vector3(-1, -1, 0);
        }
        //위쪽일 경우
        else if(lastMoveDir.y > 0)
        {
            clonePos[0] = transform.position + new Vector3(1, -1, 0);
            clonePos[1] = transform.position + new Vector3(-1, -1, 0);
        }
        //아래쪽일 경우
        else
        {
            clonePos[0] = transform.position + new Vector3(1, 1, 0);
            clonePos[1] = transform.position + new Vector3(-1, 1, 0);
        }
        int indexCount = Global.unitManager.P1UnitList.Count;
        //싱글톤을 여러번 참조하여 생기는 리소스의 양을 줄임
        List<UnitBase> p1UnitList = Global.unitManager.P1UnitList;
        List<UnitBase> p2UnitList = Global.unitManager.P2UnitList;
        
        //P1 유닛들이 해당 자리에 있는지 탐색
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
        //싱글톤을 여러번 참조하여 생기는 리소스의 양을 줄임
        //P2 유닛들이 해당 자리에 있는지 탐색
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
                //겹친 자리와 분신이 소환될 자리가 같지 않다면 그 자리로 이동
                //겹친 자리가 없어도 마찬가지
                if (stackedPos != null || clonePos[i] != stackedPos[i])
                {
                    clone.transform.position = clonePos[i];
                }
                //겹친 자리와 분신이 소환될 자리가 같다면 다음 위치로 이동
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
