using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIParent : MonoBehaviour
{
    private void Awake()
    {
        Global.uiParent = transform;
    }
}
