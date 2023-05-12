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
    [HideInInspector] public UINavigatable p1Selected;
    [HideInInspector] public UINavigatable p2Selected;

    [Header("Selector")]
    public Transform parent;

    [HideInInspector] public bool freezeP1 = false;
    [HideInInspector] public bool freezeP2 = false;

    private void Awake()
    {
        Global.uiNavManager = this;
    }

    private void Start()
    {
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
        if (nav.player == Player.Player1 && p1Selected == null) FocusOn(nav, nav.player);
        else if (nav.player == Player.Player2 && p2Selected == null) FocusOn(nav, nav.player);
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
            if (p1NavList.Count == 0) FocusOn(null, nav.player);
            else if (p1Selected == nav) FocusOn(p1NavList[0], nav.player);
        }
        if(nav.player == Player.Player2)
        {
            p2NavList.Remove(nav);
            if (p2NavList.Count == 0) FocusOn(null, nav.player);
            else if (p2Selected == nav) FocusOn(p2NavList[0], nav.player);
        }
        navList.Remove(nav);
        SearchNavigation();
    }

    // 각 navigatable에서 연결되는 navigatable연결
    public void SearchNavigation()
    {
        foreach (UINavigatable nav in navList) nav.SearchNavigatable();
    }

    private void FocusOn(UINavigatable nav, Player _player)
    {
        if (_player == Player.Player1)
        {
            if (p1Selected != null) p1Selected.OnFocusOut();
            p1Selected = nav;
            if (p1Selected != null) p1Selected.OnFocusIn();
        }
        if (_player == Player.Player2)
        {
            if (p2Selected != null) p2Selected.OnFocusOut();
            p2Selected = nav;
            if (p2Selected != null) p2Selected.OnFocusIn();
        }
    }

    private void OnP1MoveUp()
    {
        if (freezeP1 || p1Selected == null) return;
        if (p1Selected.up == null) return;
        FocusOn(p1Selected.up, Player.Player1);
    }
    private void OnP1MoveDown()
    {
        if (freezeP1 || p1Selected == null) return;
        if (p1Selected.down == null) return;
        FocusOn(p1Selected.down, Player.Player1);
    }
    private void OnP1MoveRight()
    {
        if (freezeP1 || p1Selected == null) return;
        if (p1Selected.right == null) return;
        FocusOn(p1Selected.right, Player.Player1);
    }
    private void OnP1MoveLeft()
    {
        if (freezeP1 || p1Selected == null) return;
        if (p1Selected.left == null) return;
        FocusOn(p1Selected.left, Player.Player1);
    }
    private void OnP1Select()
    {
        if (freezeP1 || p1Selected == null) return;
        p1Selected.OnSelect();
    }
    private void OnP2MoveUp()
    {
        if (freezeP2 || p2Selected == null) return;
        if (p2Selected.up == null) return;
        FocusOn(p2Selected.up, Player.Player2);
    }
    private void OnP2MoveDown()
    {
        if (freezeP2 || p2Selected == null) return;
        if (p2Selected.down == null) return;
        FocusOn(p2Selected.down, Player.Player2);
    }
    private void OnP2MoveRight()
    {
        if (freezeP2 || p2Selected == null) return;
        if (p2Selected.right == null) return;
        FocusOn(p2Selected.right, Player.Player2);
    }
    private void OnP2MoveLeft()
    {
        if (freezeP2 || p2Selected == null) return;
        if (p2Selected.left == null) return;
        FocusOn(p2Selected.left, Player.Player2);
    }
    private void OnP2Select()
    {
        if (freezeP2 || p2Selected == null) return;
        p2Selected.OnSelect();
    }
}
