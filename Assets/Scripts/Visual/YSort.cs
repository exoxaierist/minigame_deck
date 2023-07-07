using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YSort : MonoBehaviour
{
    public bool updateEveryFrame = false;
    public bool updateOnAwake = true;
    public int offset = 0;
    private SpriteRenderer spr;

    private void Awake()
    {
        if (!TryGetComponent(out spr)) { this.enabled = false; return; }
        if (updateOnAwake) Sort();
    }

    private void Update()
    {
        if (updateEveryFrame) Sort();
    }

    public void Sort()
    {
        spr.sortingOrder = -(int)transform.position.y*10 + offset;
    }
}
