using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFieldUnitPlacer : ControlledObject
{
    private bool moveEnabled = false;
    private Vector3 origin = new(0,0,1);
    private ShopFieldUnitUI shopFieldUnitUI;
    private UnitBase unit;

    protected override void Awake()
    {
        // do nothing
    }

    private void Start()
    {
        Global.OnShopOpen += OnShopOpen;
        Global.OnShopClose += OnShopClose;
        unit = GetComponent<UnitBase>();
        player = unit.player;
        visual = unit.visual;
        unit.SubscribeToInput();
    }

    private void Update()
    {
        if (origin.z != 0) origin = transform.position;
        if (moveEnabled && Global.shopManager.state != ShopState.Closed) MatchFieldUIPosition();
    }

    protected override void MoveUp() => Move(Vector2.up);
    protected override void MoveDown() => Move(Vector2.down);
    protected override void MoveRight() => Move(Vector2.right);
    protected override void MoveLeft() => Move(Vector2.left);

    private void Move(Vector2 dir)
    {
        if (!moveEnabled) return;
        MoveRelative(dir);
    }

    public void OnShopOpen()
    {
        ReturnToOrigin();
        unit = GetComponent<UnitBase>();
        player = unit.player;
        collisionLayer = unit.collisionLayer;
        unit.matchMode = false;
        // create shopfieldunitui
        if (shopFieldUnitUI != null) Destroy(shopFieldUnitUI);
        shopFieldUnitUI = Instantiate(Global.assets.shopFieldUnitUI,Global.uiParent).GetComponent<ShopFieldUnitUI>();
        shopFieldUnitUI.placer = this;
        shopFieldUnitUI.player = player;
    }

    public void OnShopClose()
    {
        unit.matchMode = true;
        DisableMovement();
        Destroy(shopFieldUnitUI);
    }

    public void RemoveSelf()
    {
        UnsubscribeToInput();
        Global.OnShopOpen -= OnShopOpen;
        Global.OnShopClose -= OnShopClose;

        Destroy(gameObject);
    }

    public void EnableMovement(Player _player)
    {
        moveEnabled = true;
        player = _player;
        origin = transform.position;
        SubscribeToInput();
    }

    public void DisableMovement() { UnsubscribeToInput(); moveEnabled = false; origin = transform.position;}

    public void ReturnToOrigin()
    {
        float temp = 0;
        DOTween.To(() => temp, x => temp = x, 1, (6 - transform.position.y) * 0.04f).OnComplete(() => { 
            MoveRelativeAbsolute(origin - transform.position);
            StartCoroutine(DelayedActivate());
        });
    }

    private void MatchFieldUIPosition()
    {
        shopFieldUnitUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        shopFieldUnitUI.Activate();
    }

    private IEnumerator DelayedActivate()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        MatchFieldUIPosition();
    }
}
