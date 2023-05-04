using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UINavigationSelector : MonoBehaviour
{
    public Vector3 offset;
    public float freq = 1;
    public float amp = 0.1f;
    private Transform child;

    private void Start()
    {
        child = transform.GetChild(0);
        if (child == null) this.enabled = false;
    }

    private void Update()
    {
        child.localPosition = new Vector3(0, Mathf.Sin(Time.time*freq)*amp, 0) + offset;
    }
}
