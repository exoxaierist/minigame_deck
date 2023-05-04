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
    public KeyCode p1Select = KeyCode.LeftShift;
    [Header("2P")]
    public KeyCode p2Up = KeyCode.UpArrow;
    public KeyCode p2Down = KeyCode.DownArrow;
    public KeyCode p2Right = KeyCode.RightArrow;
    public KeyCode p2Left = KeyCode.LeftArrow;
    public KeyCode p2Select = KeyCode.Return;

    private void Update()
    {
        // 1P
        if (enableP1)
        {
            if (Input.GetKeyDown(p1Up)) Global.OnP1Up?.Invoke();
            if (Input.GetKeyDown(p1Down)) Global.OnP1Down?.Invoke();
            if (Input.GetKeyDown(p1Right)) Global.OnP1Right?.Invoke();
            if (Input.GetKeyDown(p1Left)) Global.OnP1Left?.Invoke();
            if (Input.GetKeyDown(p1Select)) Global.OnP1Select?.Invoke();
            if (Input.GetKeyDown(p1Up) && Input.GetKeyDown(p1Down) && Input.GetKeyDown(p1Right) && Input.GetKeyDown(p1Left) && Input.GetKeyDown(p1Select)) Global.OnP1Any?.Invoke();
        }
        // 2P
        if (enableP2)
        {
            if (Input.GetKeyDown(p2Up)) Global.OnP2Up?.Invoke();
            if (Input.GetKeyDown(p2Down)) Global.OnP2Down?.Invoke();
            if (Input.GetKeyDown(p2Right)) Global.OnP2Right?.Invoke();
            if (Input.GetKeyDown(p2Left)) Global.OnP2Left?.Invoke();
            if (Input.GetKeyDown(p2Select)) Global.OnP2Select?.Invoke();
            if (Input.GetKeyDown(p2Up) && Input.GetKeyDown(p2Down) && Input.GetKeyDown(p2Right) && Input.GetKeyDown(p2Left) && Input.GetKeyDown(p2Select)) Global.OnP2Any?.Invoke();
        }
    }
}
