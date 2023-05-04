using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// 기본적으로 한칸씩 움직이는 컨트롤 가능한 오브젝트 클래스
public class ControlledObject : GridObject
{
    [Header("플레이어")]
    public Player player = Player.Player1;
    [Header("충돌 레이어")]
    public LayerMask collisionLayer;

    protected virtual void Awake()
    {
        SubscribeToInput();
    }

    protected virtual void MoveUp()
    {
        if(!Global.CheckOverlap(transform.position*Vector2.one + new Vector2(0,1),collisionLayer)) MoveRelative(new Vector2(0, 1));
    }
    protected virtual void MoveDown()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(0, -1), collisionLayer)) MoveRelative(new Vector2(0, -1));
    }
    protected virtual void MoveRight()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(1, 0), collisionLayer)) MoveRelative(new Vector2(1, 0));
    }
    protected virtual void MoveLeft()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(-1,0), collisionLayer)) MoveRelative(new Vector2(-1, 0));
    }

    // 인풋 대리자에서 제거
    protected void UnsubscribeToInput()
    {
        Global.OnP2Up -= MoveUp;
        Global.OnP2Down -= MoveDown;
        Global.OnP2Right -= MoveRight;
        Global.OnP2Left -= MoveLeft;

        Global.OnP1Up -= MoveUp;
        Global.OnP1Down -= MoveDown;
        Global.OnP1Right -= MoveRight;
        Global.OnP1Left -= MoveLeft;
        //Global.P1SpecialAction -= MoveSpecial;
    }

    // 인풋 대리자에 추가
    protected void SubscribeToInput()
    {
        UnsubscribeToInput();
        if (player == Player.Player1)
        {
            Global.OnP1Up += MoveUp;
            Global.OnP1Down += MoveDown;
            Global.OnP1Right += MoveRight;
            Global.OnP1Left += MoveLeft;
            //Global.P1SpecialAction += MoveSpecial;
        }
        else if (player == Player.Player2)
        {
            Global.OnP2Up += MoveUp;
            Global.OnP2Down += MoveDown;
            Global.OnP2Right += MoveRight;
            Global.OnP2Left += MoveLeft;
            //Global.P2SpecialAction += MoveSpecial;
        }
    }

}
