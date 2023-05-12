using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class UINavigationManager : MonoBehaviour
{
    private List<UINavigatable> navList = new(); // 화면에있는 버튼 모두 가져옴
    private List<UINavigatable> p1NavList = new();
    private List<UINavigatable> p2NavList = new();

    public List<UINavigatable> GetAllNavigatable() { return navList; }
    [HideInInspector] public List<UINavigatable> currentSelected = new();

    [Header("Selector")]
    public GameObject p1Selector;
    public GameObject p2Selector;
    public Transform parent;

    [HideInInspector] public bool freezeP1 = false;
    [HideInInspector] public bool freezeP2 = false;

    private void Awake()
    {
        Global.uiNavManager = this;
    }

    private void Start()
    {
        if (navList.Count == 0) { print("UINavigationManager 비활성화"); this.enabled = false; return; }

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
    }

    public void AddNavigatable(UINavigatable nav)
    {
        if (navList.Contains(nav)) return;
        if (nav.player == Player.Player1 && currentSelected[0] == null) FocusOn(nav, 0);
        else if (nav.player == Player.Player2 && currentSelected[1] == null) FocusOn(nav, 1);
        if (nav.player == Player.Player1) p1NavList.Add(nav);
        else if (nav.player == Player.Player2) p2NavList.Add(nav);
        navList.Add(nav);
        SearchNavigation();
    }

    public void RemoveNavigatable(UINavigatable nav)
    {
        if (!navList.Contains(nav)) return;
        if (nav.player == Player.Player1)
        {
            p1NavList.Remove(nav);
            if (p1NavList.Count == 0) FocusOn(null, 0);
            else if (currentSelected[0] == nav) FocusOn(p1NavList[0], 0);
        }
        if(nav.player == Player.Player2)
        {
            p2NavList.Remove(nav);
            if (p2NavList.Count == 0) FocusOn(null, 1);
            else if (currentSelected[1] == nav) FocusOn(p2NavList[0], 1);
        }
        navList.Remove(nav);
        SearchNavigation();
    }

    // 각 navigatable에서 연결되는 navigatable연결
    private void SearchNavigation()
    {
        foreach (UINavigatable nav in navList) nav.SearchNavigatable();
    }

    private void FocusOn(UINavigatable nav, int player)
    {
        if (currentSelected[player] != null) currentSelected[player].OnFocusOut();
        currentSelected[player] = nav;
        if (currentSelected[player] != null) currentSelected[player].OnFocusIn();
    }

    private void OnP1MoveUp()
    {
        if (freezeP1 || currentSelected[0].up == null) return;
        FocusOn(currentSelected[0].up, 0);
    }
    private void OnP1MoveDown()
    {
        if (freezeP1 || currentSelected[0].down == null) return;
        FocusOn(currentSelected[0].down, 0);
    }
    private void OnP1MoveRight()
    {
        if (freezeP1 || currentSelected[0].right == null) return;
        FocusOn(currentSelected[0].right, 0);
    }
    private void OnP1MoveLeft()
    {
        if (freezeP1 || currentSelected[0].left == null) return;
        FocusOn(currentSelected[0].left, 0);
    }
    private void OnP1Select()
    {
        if (freezeP1) return;
        currentSelected[0].OnSelect();
    }
    private void OnP2MoveUp()
    {
        if (freezeP2 || currentSelected[1].up == null) return;
        FocusOn(currentSelected[1].up, 1);
    }
    private void OnP2MoveDown()
    {
        if (freezeP2 || currentSelected[1].down == null) return;
        FocusOn(currentSelected[1].down, 1);
    }
    private void OnP2MoveRight()
    {
        if (freezeP2 || currentSelected[1].right == null) return;
        FocusOn(currentSelected[1].right, 1);
    }
    private void OnP2MoveLeft()
    {
        if (freezeP2 || currentSelected[1].left == null) return;
        FocusOn(currentSelected[1].left, 1);
    }
    private void OnP2Select()
    {
        if (freezeP2) return;
        currentSelected[1].OnSelect();
    }
}
