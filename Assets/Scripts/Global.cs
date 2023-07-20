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

    // ���� �Ŵ���
    public static RoundManager roundManager;

    // �� �Ŵ���
    public static TurnManager turnManager;

    // ���� �Ŵ���
    public static ShopManager shopManager;

    // ���� �Ŵ���
    public static UnitManager unitManager;

    // �̵� �����ϴ� �Ŵ���
    public static MovementManager moveManager;

    // ���� Ǯ
    public static AttackPooler atkPooler;


    //// UI ������
    public static Transform uiParent;


    //// �̺�Ʈ
    // ���� �̺�Ʈ
    public static Action OnP1Win;
    public static Action OnP2Win;
    public static Action OnRoundStart;
    public static Action OnRoundEnd;
    public static Action OnShopOpen;
    public static Action OnShopClose;

    public static Action OnP1CoinChange;
    public static Action OnP2CoinChange;

    // �� �̺�Ʈ
    public static Action OnTurnStart;
    public static Action OnTurnStartLate; // LateUpdateó�� TurnStart���� ���� �ڿ� �θ� �뵵
    public static Action OnP1NoInput;
    public static Action OnP2NoInput;

    // �Է� �̺�Ʈ
    public static Action OnP1Up;
    public static Action OnP1Down;
    public static Action OnP1Right;
    public static Action OnP1Left;
    public static Action OnP1Attack;
    public static Action OnP1Refresh;
    public static Action OnP1Select;
    public static Action OnP1Sell;
    public static Action OnP1Any; // �ƹ�Ű��

    public static Action OnP2Up;
    public static Action OnP2Down;
    public static Action OnP2Right;
    public static Action OnP2Left;
    public static Action OnP2Attack;
    public static Action OnP2Refresh;
    public static Action OnP2Select;
    public static Action OnP2Sell;
    public static Action OnP2Any;


    //// ��ġ ���� ����
    // �׸��� ����
    public static FieldManager fieldManager;
    public static float gridIncrement = 1;
    public static Vector2 globalOffset = Vector2.zero;
    public static Vector2 fieldSize;


    //// ��ƿ��Ƽ
    //�ݸ��� ����ũ
    public static int collisionPlayer = 1 << 7 | 1 << 8; //�÷��̾ �ݸ���

    //�ݸ��� Ȯ��
    public static bool CheckOverlap(Vector2 dest, int mask)
    {
        return Physics2D.OverlapCircle(dest, 0.3f, mask);
    }

    public static Vector2[] RotateAttackPattern(Vector2[] pattern, Vector2 dir)
    {
        // AttackRange�� ������ �������� �ۼ�
        Vector2[] result = new Vector2[pattern.Length];

        //dir�� ���� Ű�� ���� ����
        for (int i = 0; i < pattern.Length; i++)
        {
            Vector2 pos = pattern[i];
            //���⺤�Ͱ� �������̶�� ������ �������� �ۼ��Ǿ��� ������ �״�� �۵�
            if (dir == Vector2.right) result[i] = pos;
            //���⺤�Ͱ� ������ ��� ���� ��Ī��
            else if (dir == Vector2.left) result[i] = new(-pos.x, -pos.y);
            //���⺤�Ͱ� ������ ��� y���� -1�� ���� �� x�� y�� ���� �ٲ�
            else if (dir == Vector2.up) result[i] = new(-pos.y, pos.x);
            //���⺤�Ͱ� �Ʒ����� ��� x���� -1�� ���� �� x�� y�� ���� �ٲ�
            else if (dir == Vector2.down) result[i] = new(pos.y, -pos.x);
        }

        return result;
    }
}
