using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 전장 필드 크기 관리, 관련 유틸리티등
public class FieldManager : MonoBehaviour
{
    private void Awake()
    {
        Global.fieldManager = this;
    }

    public Vector2 GetEmptyTileP1()
    {

        return new(0, 0);
    }

    public Vector2 GetEmptyTileP2()
    {

        return new(0, 0);
    }
}
