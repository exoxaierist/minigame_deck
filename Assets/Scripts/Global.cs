using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//레퍼런스용
public static class Global
{
    //// 에셋 참조
    public static AssetCollector assets;

    //// 시스템
    // UI Navigation
    public static UINavigationManager uiNavManager;

    // 라운드 매니저
    public static RoundManager roundManager;

    // 턴 매니저
    public static TurnManager turnManager;

    // 상점 매니저
    public static ShopManager shopManager;

    // 유닛 매니저
    public static UnitManager unitManager;

    // 이동 관리하는 매니저
    public static MovementManager moveManager;

    // 공격 풀
    public static AttackPooler atkPooler;


    //// UI 참조들
    public static Transform uiParent;


    //// 이벤트
    // 라운드 이벤트
    public static Action OnP1Win;
    public static Action OnP2Win;
    public static Action OnRoundStart;
    public static Action OnRoundEnd;
    public static Action OnShopOpen;
    public static Action OnShopClose;

    public static Action OnP1CoinChange;
    public static Action OnP2CoinChange;

    // 턴 이벤트
    public static Action OnTurnStart;
    public static Action OnP1NoInput;
    public static Action OnP2NoInput;

    // 입력 이벤트
    public static Action OnP1Up;
    public static Action OnP1Down;
    public static Action OnP1Right;
    public static Action OnP1Left;
    public static Action OnP1Attack;
    public static Action OnP1Refresh;
    public static Action OnP1Select;
    public static Action OnP1Sell;
    public static Action OnP1Any; // 아무키나

    public static Action OnP2Up;
    public static Action OnP2Down;
    public static Action OnP2Right;
    public static Action OnP2Left;
    public static Action OnP2Attack;
    public static Action OnP2Refresh;
    public static Action OnP2Select;
    public static Action OnP2Sell;
    public static Action OnP2Any;


    //// 매치 전역 설정
    // 그리드 설정
    public static FieldManager fieldManager;
    public static float gridIncrement = 1;
    public static Vector2 globalOffset = Vector2.zero;
    public static Vector2 fieldSize;


    //// 유틸리티
    //콜리젼 마스크
    public static int collisionPlayer = 1 << 7 | 1 << 8; //플레이어만 콜리젼

    //콜리젼 확인
    public static bool CheckOverlap(Vector2 dest, int mask)
    {
        return Physics2D.OverlapCircle(dest, 0.3f, mask);
    }

    public static Vector2[] RotateAttackPattern(Vector2[] pattern, Vector2 dir)
    {
        Vector2[] result = new Vector2[pattern.Length];

        for (int i = 0; i < pattern.Length; i++)
        {
            Vector2 pos = pattern[i];
            if (dir == Vector2.right) result[i] = pos;
            else if (dir == Vector2.left) result[i] = new(-pos.x, pos.y);
            else if (dir == Vector2.up) result[i] = new(-pos.y, pos.x);
            else if (dir == Vector2.down) result[i] = new(pos.y, -pos.x);
        }

        return result;
    }
}
