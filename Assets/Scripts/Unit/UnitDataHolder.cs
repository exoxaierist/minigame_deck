using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="UnitManager")]
public class UnitDataHolder : ScriptableObject
{
    public List<UnitSet> unitList;

    public UnitSet GetUnitSet(string _id)
    {
        foreach (UnitSet set in unitList)
        {
            if (set.id == _id) return set;
        }
        return unitList[0];
    }
}

[Serializable]
public struct UnitSet
{
    public string id; // À¯´Ö °íÀ¯ id
    public string name; // À¯´Ö ÀÌ¸§
    public Sprite image; // À¯´Ö ÀÌ¹ÌÁö
    public int health; // À¯´Ö Ã¼·Â
    public int power; // À¯´Ö °ø°Ý·Â
    public int price; // À¯´Ö °¡°Ý
    public string desc; // À¯´Ö ¼³¸í±Û
    public GameObject fieldObject;
}
