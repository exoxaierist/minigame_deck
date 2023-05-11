using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveGrid : UnitBase
{
    int MovedGrids = 0;
    public int MoveGrids = 1;
    protected virtual void OnGridMove() { }
    #region
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
    void OnMove()
    {
        MovedGrids++;

        if (MovedGrids == MoveGrids)
        {
            MovedGrids = 0;
            OnGridMove();
        }
    }
}
