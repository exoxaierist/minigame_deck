using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class UINavigationManager : MonoBehaviour
{
    private List<UINavigatable> navList = new();

    public List<UINavigatable> GetAllNavigatable() { return navList; }
    [HideInInspector] public List<UINavigatable> currentSelected = new();

    [Header("Selector")]
    public GameObject p1Selector;
    public GameObject p2Selector;
    public Transform parent;
    [Header("Entry Button")]
    public UINavigatable p1StartNav;
    public UINavigatable p2StartNav;

    private void Awake()
    {
        Global.uiNavManager = this;
        navList = FindObjectsOfType<UINavigatable>().ToList();
        if (navList.Count == 0 || p1StartNav == null || p2StartNav == null) { print("UINavigationManager 비활성화"); this.enabled = false; return; }
        foreach (UINavigatable nav in navList) nav.SearchNavigatable();
        currentSelected.Add(p1StartNav);
        currentSelected.Add(p2StartNav);

        Global.OnP1Up += OnP1MoveUp;
        Global.OnP1Down += OnP1MoveDown;
        Global.OnP1Right += OnP1MoveRight;
        Global.OnP1Left += OnP1MoveLeft;
        Global.OnP1Select += OnP1Select;

        Global.OnP2Up += OnP2MoveUp;
        Global.OnP2Down += OnP2MoveDown;
        Global.OnP2Right += OnP2MoveRight;
        Global.OnP2Left += OnP2MoveLeft;
        Global.OnP2Select += OnP2Select;

        p1Selector = Instantiate(p1Selector);
        p2Selector = Instantiate(p2Selector);
        p1Selector.transform.SetParent(parent);
        p2Selector.transform.SetParent(parent);
    }

    private void Start()
    {
        SetP1Selector();
        SetP2Selector();
    }

    private void OnP1MoveUp()
    {
        if (currentSelected[0].up == null) return;
        currentSelected[0].OnFocusOut();
        currentSelected[0] = currentSelected[0].up;
        currentSelected[0].OnFocusIn();
        SetP1Selector();
    }
    private void OnP1MoveDown()
    {
        if (currentSelected[0].down == null) return;
        currentSelected[0].OnFocusOut();
        currentSelected[0] = currentSelected[0].down;
        currentSelected[0].OnFocusIn();
        SetP1Selector();
    }
    private void OnP1MoveRight()
    {
        if (currentSelected[0].right == null) return;
        currentSelected[0].OnFocusOut();
        currentSelected[0] = currentSelected[0].right;
        currentSelected[0].OnFocusIn();
        SetP1Selector();
    }
    private void OnP1MoveLeft()
    {
        if (currentSelected[0].left == null) return;
        currentSelected[0].OnFocusOut();
        currentSelected[0] = currentSelected[0].left;
        currentSelected[0].OnFocusIn();
        SetP1Selector();
    }
    private void OnP1Select()
    {
        currentSelected[0].OnSelect();
    }
    private void OnP2MoveUp()
    {
        if (currentSelected[1].up == null) return;
        currentSelected[1].OnFocusOut();
        currentSelected[1] = currentSelected[1].up;
        currentSelected[1].OnFocusIn();
        SetP2Selector();
    }
    private void OnP2MoveDown()
    {
        if (currentSelected[1].down == null) return;
        currentSelected[1].OnFocusOut();
        currentSelected[1] = currentSelected[1].down;
        currentSelected[1].OnFocusIn();
        SetP2Selector();
    }
    private void OnP2MoveRight()
    {
        if (currentSelected[1].right == null) return;
        currentSelected[1].OnFocusOut();
        currentSelected[1] = currentSelected[1].right;
        currentSelected[1].OnFocusIn();
        SetP2Selector();
    }
    private void OnP2MoveLeft()
    {
        if (currentSelected[1].left == null) return;
        currentSelected[1].OnFocusOut();
        currentSelected[1] = currentSelected[1].left;
        currentSelected[1].OnFocusIn();
        SetP2Selector();
    }
    private void OnP2Select()
    {
        currentSelected[1].OnSelect();
    }
    private void SetP1Selector()
    {
        p1Selector.transform.position = currentSelected[0].transform.position;
    }
    private void SetP2Selector()
    {
        p2Selector.transform.position = currentSelected[1].transform.position;
    }
}
