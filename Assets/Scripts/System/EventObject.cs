using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이벤트 받아들이는 오브젝트에 할당, 이벤트 추가 가능
public class EventObject : MonoBehaviour
{
    protected void EventSubscribe()
    {
        Global.OnRoundStart += OnRoundStart;
    }

    protected virtual void OnRoundStart() { }
}
