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

    // 턴 매니저
    public static TurnManager turnManager;

    // 상점 매니저
    public static ShopManager shopManager;


    //// UI 참조들
    public static Transform uiParent;


    //// 이벤트
    // 라운드 이벤트
    public static Action OnRoundStart;
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
    public static Action OnP1Select; // 공격
    public static Action OnP1Any; // 아무키나

    public static Action OnP2Up;
    public static Action OnP2Down;
    public static Action OnP2Right;
    public static Action OnP2Left;
    public static Action OnP2Select; // 공격
    public static Action OnP2Any; // 아무키나


    //// 매치 전역 설정
    // 그리드 설정
    public static float gridIncrement = 1;
    public static Vector2 globalOffset = Vector2.zero;


    //// 유틸리티
    //콜리젼 확인
    public static bool CheckOverlap(Vector2 dest, int mask)
    {
        return Physics2D.OverlapCircle(dest, 0.3f, mask);
    }
}
