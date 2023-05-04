using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

//레퍼런스용
public static class Global
{
    //// 시스템
    // 이벤트
    public static Action OnRoundStart;

    // 입력 핸들러
    public static Action P1UpAction;
    public static Action P1DownAction;
    public static Action P1RightAction;
    public static Action P1LeftAction;
    public static Action P1SelectAction;

    public static Action P2UpAction;
    public static Action P2DownAction;
    public static Action P2RightAction;
    public static Action P2LeftAction;
    public static Action P2SelectAction;

    // UI Navigation
    public static UINavigationManager uiNavManager;

    //// 에셋 참조
    public static AssetCollector assets;


    //// 매치 전역 설정
    // 그리드 설정
    public static float gridIncrement = 1;
    public static Vector2 globalOffset = Vector2.zero;


    //// 유틸리티
    //콜리젼 확인
    public static bool CheckOverlap(Vector2 dest, LayerMask mask)
    {
        return Physics2D.OverlapCircle(dest, 0.3f, mask);
    }
}
