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
        Global.P2UpAction -= MoveUp;
        Global.P2DownAction -= MoveDown;
        Global.P2RightAction -= MoveRight;
        Global.P2LeftAction -= MoveLeft;

        Global.P1UpAction -= MoveUp;
        Global.P1DownAction -= MoveDown;
        Global.P1RightAction -= MoveRight;
        Global.P1LeftAction -= MoveLeft;
        //Global.P1SpecialAction -= MoveSpecial;
    }

    // 인풋 대리자에 추가
    protected void SubscribeToInput()
    {
        UnsubscribeToInput();
        if (player == Player.Player1)
        {
            Global.P1UpAction += MoveUp;
            Global.P1DownAction += MoveDown;
            Global.P1RightAction += MoveRight;
            Global.P1LeftAction += MoveLeft;
            //Global.P1SpecialAction += MoveSpecial;
        }
        else if (player == Player.Player2)
        {
            Global.P2UpAction += MoveUp;
            Global.P2DownAction += MoveDown;
            Global.P2RightAction += MoveRight;
            Global.P2LeftAction += MoveLeft;
            //Global.P2SpecialAction += MoveSpecial;
        }
    }

}
