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

    private void Awake()
    {
        SubscribeToInput();
    }

    // 컨트롤 종속 대리자 설정
    private void SubscribeToInput()
    {
        if (player == Player.None)
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
        else if(player == Player.Player1)
        {
            Global.P2UpAction -= MoveUp;
            Global.P2DownAction -= MoveDown;
            Global.P2RightAction -= MoveRight;
            Global.P2LeftAction -= MoveLeft;

            Global.P1UpAction += MoveUp;
            Global.P1DownAction += MoveDown;
            Global.P1RightAction += MoveRight;
            Global.P1LeftAction += MoveLeft;
            //Global.P1SpecialAction += MoveSpecial;
        }
        else if(player==Player.Player2)
        {
            Global.P1UpAction -= MoveUp;
            Global.P1DownAction -= MoveDown;
            Global.P1RightAction -= MoveRight;
            Global.P1LeftAction -= MoveLeft;

            Global.P2UpAction += MoveUp;
            Global.P2DownAction += MoveDown;
            Global.P2RightAction += MoveRight;
            Global.P2LeftAction += MoveLeft;
            //Global.P2SpecialAction += MoveSpecial;
        }
    }

    protected void MoveUp()
    {
        if(!Global.CheckOverlap(transform.position*Vector2.one + new Vector2(0,1),collisionLayer)) MoveRelative(new Vector2(0, 1));
    }
    protected void MoveDown()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(0, -1), collisionLayer)) MoveRelative(new Vector2(0, -1));
    }
    protected void MoveRight()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(1, 0), collisionLayer)) MoveRelative(new Vector2(1, 0));
    }
    protected void MoveLeft()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(-1,0), collisionLayer)) MoveRelative(new Vector2(-1, 0));
    }
}
