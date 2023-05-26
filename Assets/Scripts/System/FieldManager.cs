using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 전장 필드 크기 관리, 관련 유틸리티등
public class FieldManager : MonoBehaviour
{
    public Vector2 fieldSize = new(14, 10);

    private void Awake()
    {
        Global.fieldManager = this;
        Global.fieldSize = fieldSize;
    }

    private void Start()
    {
        CenterCamera();
    }

    public void CenterCamera()
    {
        Camera.main.transform.position = new(Global.fieldSize.x / 2, Global.fieldSize.y / 2, -1);
    }

    public Vector2 GetEmptyTileP1()
    {
        for (int x = 1; x <= Mathf.Floor(Global.fieldSize.x/2); x++)
        {
            for (int y = 1; y <= (int)Global.fieldSize.y; y++)
            {
                if (!Physics2D.OverlapCircle(new(x, y), 0.3f)) return (new(x, y));
            }
        }
        return new(0, 0);
    }

    public Vector2 GetEmptyTileP2()
    {
        for (int x = (int)Mathf.Ceil(Global.fieldSize.x/2); x <= Global.fieldSize.x; x++)
        {
            for (int y = 1; y <= (int)Global.fieldSize.y; y++)
            {
                if (!Physics2D.OverlapCircle(new(x, y), 0.3f)) return (new(x, y));
            }
        }
        return new(0, 0);
    }

    public Vector2 GetRandomTile()
    {
        return new(0, 0);
    }
}
