using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBombMamamam : UnitBase
{
    protected override void OnMove()
    {
        //공격하는 함수가 아직 만들어지지 않아 주석처리
        //Attack(Global.fieldManager.GetRandomTile());
    }
    protected override void Attack()
    {
        for(int xPos = (int)transform.position.x - 1; xPos <= (int)transform.position.x + 1; xPos++)
        {
            for(int yPos = (int)transform.position.y - 1; yPos <= (int)transform.position.y + 1; yPos++)
            {
                //공격하는 함수
                //공격(new Vector2(xPos,yPos))
            }
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
