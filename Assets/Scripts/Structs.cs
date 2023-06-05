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
    public string id; // À¯´Ö °íÀ¯ id
    public string name; // À¯´Ö ÀÌ¸§
    public int tier; // À¯´Ö Æ¼¾î
    public Sprite image; // À¯´Ö ÀÌ¹ÌÁö
    public int health; // À¯´Ö Ã¼·Â
    public int power; // À¯´Ö °ø°Ý·Â
    public int price; // À¯´Ö °¡°Ý
    public string desc; // À¯´Ö ¼³¸í±Û
    public GameObject fieldObject;
}

public interface IReceiveAttack
{
    public void ReceivePayload(AttackInfo info)
    {

    }
}
