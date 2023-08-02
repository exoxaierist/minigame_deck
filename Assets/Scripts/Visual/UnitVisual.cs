using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(YSort))]
public class UnitVisual : MonoBehaviour
{
    public SpriteRenderer spr;
    private YSort sorter;
    private UnitBase unit;

    public bool ignoreUnit = false;
    public Color damageColor;

    private void Awake()
    {
        spr = GetComponent<SpriteRenderer>();
        sorter = GetComponent<YSort>();
        sorter.updateEveryFrame = true;

        if (ignoreUnit) return;
        unit = transform.parent.GetComponent<UnitBase>();
        unit.hp.OnDamage += OnDamage;
        unit.hp.OnDeath += OnDeath;
        unit.hp.OnRevive += OnRevive;

        unit.visual = transform;
    }

    private void Update()
    {
        if (ignoreUnit) return;
        if (unit.lastMoveDir == Vector2.left) spr.flipX = true;
        else if (unit.lastMoveDir == Vector2.right) spr.flipX = false;
    }

    private void OnDamage(UnitBase _)
    {
        transform.DOComplete();
        transform.DOShakeRotation(0.3f, new Vector3(0, 0, 8), 25);
        transform.DOShakePosition(0.2f, new Vector3(0.05f, 0.05f, 0),25);
        spr.DOComplete();
        spr.color = damageColor;
        spr.DOColor(Color.white, 0.5f);
    }

    private void OnDeath(UnitBase _)
    {
        spr.DOFade(0.5f, 0.2f);
    }
    private void OnRevive(UnitBase _)
    {
        spr.DOFade(1f, 1f);
    }
}
