using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class UnitOnDamgeEvent : MonoBehaviour 
{
    UnityEvent event ; 
    void Start() { 
        if (event == null) {
            event = new UnityEvent();
        }
    }
}
