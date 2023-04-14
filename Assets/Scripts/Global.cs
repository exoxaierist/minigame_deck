using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//레퍼런스 참조용
public static class Global
{
    /// <summary>
    /// 입력 핸들러
    /// </summary>
    public delegate void InputEvent();
    public static InputEvent P1UpAction;
    public static InputEvent P1DownAction;
    public static InputEvent P1RightAction;
    public static InputEvent P1LeftAction;
    public static InputEvent P1SpecialAction;

    public static InputEvent P2UpAction;
    public static InputEvent P2DownAction;
    public static InputEvent P2RightAction;
    public static InputEvent P2LeftAction;
    public static InputEvent P2SpecialAction;


    /// <summary>
    /// 매치 전역 설정
    /// </summary>

    // 그리드 설정
    public static float gridIncrement = 1;
    public static Vector2 globalOffset = Vector2.zero;
}
