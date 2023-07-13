using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ʵ� ũ�� ����, ���� ��ƿ��Ƽ��
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
        Camera.main.transform.position = new(Global.fieldSize.x / 2 + 0.5f, Global.fieldSize.y / 2 + 0.5f, -1);
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
        // ���� �Ϸ�
        int fieldSizeX = (int)fieldSize.x;
        int fieldSizeY = (int)fieldSize.y;

        int RandomX = Random.Range(1, fieldSizeX + 1);
        int RandomY = Random.Range(1, fieldSizeY + 1);
        return new(RandomX, RandomY);
    }
}
