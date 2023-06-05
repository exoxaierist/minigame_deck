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


