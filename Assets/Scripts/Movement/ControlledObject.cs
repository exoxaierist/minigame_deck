using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// �⺻������ ��ĭ�� �����̴� ��Ʈ�� ������ ������Ʈ Ŭ����
public class ControlledObject : GridObject
{
    [Header("�÷��̾�")]
    public Player player = Player.Player1;
    [Header("�浹 ���̾�")]
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

    // ��ǲ �븮�ڿ��� ����
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

    // ��ǲ �븮�ڿ� �߰�
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
