using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class UINavigationSelector : MonoBehaviour
{
    public Player player;
    public Vector3 offset;
    public float freq = 1;
    public float amp = 0.1f;
    private Transform child;
    private Vector3 originScale;

    private UINavigatable target;

    private bool visible = true;

    private void Start()
    {
        child = transform.GetChild(0);
        if (child == null) enabled = false;
        originScale = child.transform.localScale;
        Hide();
    }

    private void Update()
    {
        if (player == Player.Player1) target = Global.uiNavManager.p1Selected;
        else if (player == Player.Player2) target = Global.uiNavManager.p2Selected;
        if (target == null) Hide();
        else if (target is ShopCard) Hide();
        else{
            Show();
            transform.position = target.transform.position;
            child.localPosition = new Vector3(0, Mathf.Sin(Time.time*freq)*amp, 0) + offset;
        }
    }
    
    private void Show()
    {
        if (visible) return;
        visible = true;
        child.localScale = originScale;
    }

    private void Hide()
    {
        if (!visible) return;
        visible = false;
        child.localScale = Vector3.zero;
    }
}
