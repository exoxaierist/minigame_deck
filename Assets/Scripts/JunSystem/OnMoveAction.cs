using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMoveAction : UnitBase
{
    //현재 움직인 칸 수
    int MovedGrids = 0;

    [Tooltip("MoveGrids 만큼 움직이면 함수 발동")]
    public int MoveGrids = 1;

    protected virtual void OnGridMoved() { }//조건 충족시 함수 발동
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

   /* #region Move~ 오버라이드: 끝에 OnMove() 붙임
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