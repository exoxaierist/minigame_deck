using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �ʵ� ũ�� ����, ���� ��ƿ��Ƽ��
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
