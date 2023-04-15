using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// AssetCollector 오브젝트를 Global에 지정
public class AssetHolder : MonoBehaviour
{
    public AssetCollector collector;

    private void Awake()
    {
        Global.assets = collector;
    }
}
