using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UINavigatable : MonoBehaviour
{
    public Player player = Player.Player1;
    public NavType type;

    [HideInInspector] public UINavigatable right;
    [HideInInspector] public UINavigatable left;
    [HideInInspector] public UINavigatable up;
    [HideInInspector] public UINavigatable down;

    // 버튼이 활성화될때 불러야됨
    public void Activate()
    {
        Global.uiNavManager.AddNavigatable(this);
    }

    public void Deactivate()
    {
        Global.uiNavManager.RemoveNavigatable(this);
    }

    public void SearchNavigatable()
    {
        right = SearchRight();
        left = SearchLeft();
        up = SearchUp();
        down = SearchDown();
    }

    public virtual void OnFocusIn() { }

    public virtual void OnFocusOut() { }

    public virtual void OnSelect() { }

    private UINavigatable SearchRight()
    {
        List<UINavigatable> all = Global.uiNavManager.GetAllNavigatable();
        float closestDist = 10000000;
        UINavigatable result = null;
        
        foreach (UINavigatable nav in all)
        {
            if (player == nav.player)
            {
                if (nav.transform.position.x > transform.position.x)
                {
                    Vector3 dir = nav.transform.position - transform.position;
                    float dot = Vector3.Dot(Vector3.right, dir.normalized);
                    if (dot > 0.7f && dir.magnitude < closestDist)
                    {
                        closestDist = dir.magnitude;
                        result = nav;
                    }
                }
            }
        }
        return result;
    }

    private UINavigatable SearchLeft()
    {
        List<UINavigatable> all = Global.uiNavManager.GetAllNavigatable();
        float closestDist = 10000000;
        UINavigatable result = null;

        foreach (UINavigatable nav in all)
        {
            if (player == nav.player)
            {
                if (nav.transform.position.x < transform.position.x)
                {
                    Vector3 dir = nav.transform.position - transform.position;
                    float dot = Vector3.Dot(Vector3.left, dir.normalized);
                    if (dot > 0.7f && dir.magnitude < closestDist)
                    {
                        closestDist = dir.magnitude;
                        result = nav;
                    }
                }
            }
        }
        return result;
    }

    private UINavigatable SearchUp()
    {
        List<UINavigatable> all = Global.uiNavManager.GetAllNavigatable();
        float closestDist = 10000000;
        UINavigatable result = null;

        foreach (UINavigatable nav in all)
        {
            if (player == nav.player)
            {
                if (nav.transform.position.y > transform.position.y)
                {
                    Vector3 dir = nav.transform.position - transform.position;
                    float dot = Vector3.Dot(Vector3.up, dir.normalized);
                    if (dot > 0.7f && dir.magnitude < closestDist)
                    {
                        closestDist = dir.magnitude;
                        result = nav;
                    }
                }
            }
        }
        return result;
    }

    private UINavigatable SearchDown()
    {
        List<UINavigatable> all = Global.uiNavManager.GetAllNavigatable();
        float closestDist = 10000000;
        UINavigatable result = null;

        foreach (UINavigatable nav in all)
        {
            if (player == nav.player)
            {
                if (nav.transform.position.y < transform.position.y)
                {
                    Vector3 dir = nav.transform.position - transform.position;
                    float dot = Vector3.Dot(Vector3.down, dir.normalized);
                    if (dot > 0.7f && dir.magnitude < closestDist)
                    {
                        closestDist = dir.magnitude;
                        result = nav;
                    }
                }
            }
        }
        return result;
    }
}
