using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMoveAction : UnitBase
{
    //���� ������ ĭ ��
    int MovedGrids = 0;

    [Tooltip("MoveGrids ��ŭ �����̸� �Լ� �ߵ�")]
    public int MoveGrids = 1;

    protected virtual void OnGridMoved() { }//���� ������ �Լ� �ߵ�
    protected override void Awake()
    {
        base.Awake();
    }

    protected override void OnMove()
    {
        MovedGrids++;

        if (MovedGrids == MoveGrids)
        {
            MovedGrids = 0;
            OnGridMoved();
            Debug.Log("moved");
        }
    }
}

   /* #region Move~ �������̵�: ���� OnMove() ����
    protected override void MoveUp()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(0, 1), collisionLayer)) 
        { 
            MoveRelative(new Vector2(0, 1));
            OnMove();
        }
    }
    protected override void MoveDown()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(0, -1), collisionLayer)) 
        { 
            MoveRelative(new Vector2(0, -1));
            OnMove();
        }
    }
    protected override void MoveRight()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(1, 0), collisionLayer)) 
        { 
            MoveRelative(new Vector2(1, 0));
            OnMove();
        }
    }
    protected override void MoveLeft()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(-1, 0), collisionLayer)) 
        { 
            MoveRelative(new Vector2(-1, 0));
            OnMove();
        }
    }
    #endregion
    
*/