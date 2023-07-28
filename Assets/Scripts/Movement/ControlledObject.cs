using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

// 기본적으로 한칸씩 움직이는 컨트롤 가능한 오브젝트 클래스
public class ControlledObject : GridObject
{
    [Header("플레이어")]
    public Player player = Player.Player1;
    [HideInInspector] public LayerMask collisionLayer = 0;
    [HideInInspector] public bool matchMode = true;

    protected Vector2 direction = new(1,1);

    protected virtual void Awake()
    {
        base.Awake();
        SubscribeToInput();
    }

    protected virtual void MoveUp() {}
    protected virtual void MoveDown() {}
    protected virtual void MoveRight() {}
    protected virtual void MoveLeft() {}
    protected virtual void Attack() {}

    // 인풋 대리자에서 제거
    protected void UnsubscribeToInput()
    {
        Global.OnP2Up -= MoveUp;
        Global.OnP2Down -= MoveDown;
        Global.OnP2Right -= MoveRight;
        Global.OnP2Left -= MoveLeft;
        Global.OnP2Attack -= Attack;

        Global.OnP1Up -= MoveUp;
        Global.OnP1Down -= MoveDown;
        Global.OnP1Right -= MoveRight;
        Global.OnP1Left -= MoveLeft;
        Global.OnP1Attack -= Attack;
    }

    // 인풋 대리자에 추가
    public void SubscribeToInput()
    {
        UnsubscribeToInput();
        if (player == Player.Player1)
        {
            Global.OnP1Up += MoveUp;
            Global.OnP1Down += MoveDown;
            Global.OnP1Right += MoveRight;
            Global.OnP1Left += MoveLeft;
            Global.OnP1Attack += Attack;
        }
        else if (player == Player.Player2)
        {
            Global.OnP2Up += MoveUp;
            Global.OnP2Down += MoveDown;
            Global.OnP2Right += MoveRight;
            Global.OnP2Left += MoveLeft;
            Global.OnP2Attack += Attack;
        }
    }

}
