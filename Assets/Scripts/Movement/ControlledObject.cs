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
    public LayerMask collisionLayer = 0;
    [HideInInspector] public bool canMove = true;

    protected virtual void Awake()
    {
        SubscribeToInput();
    }

    protected virtual void MoveUp()
    {
        if (canMove) MoveRelative(new(0, 1), collisionLayer.value);
    }
    protected virtual void MoveDown()
    {
        if (canMove) MoveRelative(new(0, -1), collisionLayer.value);
    }
    protected virtual void MoveRight()
    {
        if (canMove) MoveRelative(new(1, 0), collisionLayer.value);
    }
    protected virtual void MoveLeft()
    {
        if (canMove) MoveRelative(new(-1, 0),collisionLayer.value);
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
    public void SubscribeToInput()
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
