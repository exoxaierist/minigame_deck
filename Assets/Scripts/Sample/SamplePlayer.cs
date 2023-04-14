using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class SamplePlayer : GridObject
{
    public Player player;


    private void Awake()
    {
        SubscribeToInput();
    }


    private void SubscribeToInput()
    {
        if(player == Player.Player1)
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
            //Global.P1SpecialAction += MoveSpecial;
        }
    }

    private void MoveUp() => MoveRelative(new Vector2(0, 1));
    private void MoveDown() => MoveRelative(new Vector2(0, -1));
    private void MoveRight() => MoveRelative(new Vector2(1, 0));
    private void MoveLeft() => MoveRelative(new Vector2(-1, 0));
}
