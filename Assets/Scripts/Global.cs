using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//���۷�����
public static class Global
{
    //// ���� ����
    public static AssetCollector assets;

    //// �ý���
    // UI Navigation
    public static UINavigationManager uiNavManager;

    // �� �Ŵ���
    public static TurnManager turnManager;

    //// �̺�Ʈ
    // ���� �̺�Ʈ
    public static Action OnRoundStart;

    // �� �̺�Ʈ
    public static Action OnTurnStart;
    public static Action OnP1NoInput;
    public static Action OnP2NoInput;

    // �Է� �̺�Ʈ
    public static Action OnP1Up;
    public static Action OnP1Down;
    public static Action OnP1Right;
    public static Action OnP1Left;
    public static Action OnP1Select; // ����
    public static Action OnP1Any; // �ƹ�Ű��

    public static Action OnP2Up;
    public static Action OnP2Down;
    public static Action OnP2Right;
    public static Action OnP2Left;
    public static Action OnP2Select; // ����
    public static Action OnP2Any; // �ƹ�Ű��


    //// ��ġ ���� ����
    // �׸��� ����
    public static float gridIncrement = 1;
    public static Vector2 globalOffset = Vector2.zero;


    //// ��ƿ��Ƽ
    //�ݸ��� Ȯ��
    public static bool CheckOverlap(Vector2 dest, LayerMask mask)
    {
        return Physics2D.OverlapCircle(dest, 0.3f, mask);
    }
}
