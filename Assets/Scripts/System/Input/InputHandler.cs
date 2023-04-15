using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어의 인풋에 대한 대리자를 관리
[DefaultExecutionOrder(-50)]
public class InputHandler : MonoBehaviour
{
    [Header("플레이어 활성화")]
    public bool enableP1 = true;
    public bool enableP2 = true;
    [Header("키지정")]
    [Header("1P")]
    public KeyCode p1Up = KeyCode.W;
    public KeyCode p1Down = KeyCode.S;
    public KeyCode p1Right = KeyCode.D;
    public KeyCode p1Left = KeyCode.A;
    public KeyCode p1Special = KeyCode.LeftControl;
    [Header("2P")]
    public KeyCode p2Up = KeyCode.UpArrow;
    public KeyCode p2Down = KeyCode.DownArrow;
    public KeyCode p2Right = KeyCode.RightArrow;
    public KeyCode p2Left = KeyCode.LeftArrow;
    public KeyCode p2Special = KeyCode.RightShift;

    private void Update()
    {
        // 1P
        if (enableP1)
        {
            if (Input.GetKeyDown(p1Up)) Global.P1UpAction();
            if (Input.GetKeyDown(p1Down)) Global.P1DownAction();
            if (Input.GetKeyDown(p1Right)) Global.P1RightAction();
            if (Input.GetKeyDown(p1Left)) Global.P1LeftAction();
            if (Input.GetKeyDown(p1Special)) Global.P1SpecialAction();  
        }
        // 2P
        if (enableP2)
        {
            if (Input.GetKeyDown(p2Up)) Global.P2UpAction();
            if (Input.GetKeyDown(p2Down)) Global.P2DownAction();
            if (Input.GetKeyDown(p2Right)) Global.P2RightAction();
            if (Input.GetKeyDown(p2Left)) Global.P2LeftAction();
            if (Input.GetKeyDown(p2Special)) Global.P2SpecialAction();
        }
    }
}
