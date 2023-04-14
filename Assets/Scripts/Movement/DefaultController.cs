using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// �⺻������ ��ĭ�� �����̴� ��Ʈ�� ������ ������Ʈ Ŭ����
public class DefaultController : GridObject
{
    public Player player = Player.Player1;
    public LayerMask collisionLayer;

    private void Awake()
    {
        SubscribeToInput();
    }

    // ��Ʈ�� ���� �븮�� ����
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

    private void MoveUp()
    {
        if(!Global.CheckOverlap(transform.position*Vector2.one + new Vector2(0,1),collisionLayer)) MoveRelative(new Vector2(0, 1));
    }
    private void MoveDown()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(0, -1), collisionLayer)) MoveRelative(new Vector2(0, -1));
    }
    private void MoveRight()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(1, 0), collisionLayer)) MoveRelative(new Vector2(1, 0));
    }
    private void MoveLeft()
    {
        if (!Global.CheckOverlap(transform.position * Vector2.one + new Vector2(-1,0), collisionLayer)) MoveRelative(new Vector2(-1, 0));
    }
}
