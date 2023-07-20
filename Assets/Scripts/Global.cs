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
    public static Action OnTurnStartLate; // LateUpdate처럼 TurnStart보다 한턴 뒤에 부를 용도
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
        // AttackRange는 오른쪽 기준으로 작성
        Vector2[] result = new Vector2[pattern.Length];

        //dir은 누른 키의 방향 벡터
        for (int i = 0; i < pattern.Length; i++)
        {
            Vector2 pos = pattern[i];
            //방향벡터가 오른쪽이라면 오른쪽 기준으로 작성되었기 때문에 그대로 작동
            if (dir == Vector2.right) result[i] = pos;
            //방향벡터가 왼쪽일 경우 원점 대칭임
            else if (dir == Vector2.left) result[i] = new(-pos.x, -pos.y);
            //방향벡터가 위쪽일 경우 y값에 -1을 곱한 후 x와 y의 값을 바꿈
            else if (dir == Vector2.up) result[i] = new(-pos.y, pos.x);
            //방향벡터가 아래쪽일 경우 x값에 -1을 곱한 후 x와 y의 값을 바꿈
            else if (dir == Vector2.down) result[i] = new(pos.y, -pos.x);
        }

        return result;
    }
}
