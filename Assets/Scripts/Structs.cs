using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AttackInfo{
    public int damage;
    public UnitBase attacker;
}

[Serializable]
public struct UnitSet
{
    public string id; // ���� ���� id
    public string name; // ���� �̸�
    public int tier; // ���� Ƽ��
    public Sprite image; // ���� �̹���
    public int health; // ���� ü��
    public int power; // ���� ���ݷ�
    public int price; // ���� ����
    public string desc; // ���� �����
    public GameObject fieldObject;
}

public struct MoveInfo
{
    public GridObject obj;
    public Vector2 dir;
    public int count;
    public bool finished;
}

public interface IReceiveAttack
{
    public void ReceivePayload(AttackInfo info)
    {

    }
}
