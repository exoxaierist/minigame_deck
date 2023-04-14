using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//���۷��� ������
public static class Global
{
    /// <summary>
    /// �Է� �ڵ鷯
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
    /// ��ġ ���� ����
    /// </summary>

    // �׸��� ����
    public static float gridIncrement = 1;
    public static Vector2 globalOffset = Vector2.zero;
}
