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
        
    }

    private void Start()
    {
        Global.OnShopOpen += OnShopOpen;
        Global.OnShopClose += OnShopClose;
        unit = GetComponent<UnitBase>();
        unit.SubscribeToInput();
    }

    private void Update()
    {
        if (origin.z != 0) origin = transform.position;
        if (moveEnabled && Global.shopManager.state != ShopState.Closed) MatchFieldUIPosition();
    }

    public void OnShopOpen()
    {
        unit = GetComponent<UnitBase>();
        player = unit.player;
        collisionLayer = unit.collisionLayer;
        unit.canMove = false;
        // create shopfieldunitui
        if (shopFieldUnitUI != null) Destroy(shopFieldUnitUI);
        shopFieldUnitUI = Instantiate(Global.assets.shopFieldUnitUI,Global.uiParent).GetComponent<ShopFieldUnitUI>();
        shopFieldUnitUI.placer = this;
        shopFieldUnitUI.player = player;
        shopFieldUnitUI.Activate();

        ReturnToOrigin();
        MatchFieldUIPosition();
    }

    public void OnShopClose()
    {
        unit.canMove = true;
        DisableMovement();
        Destroy(shopFieldUnitUI);
    }

    public void EnableMovement(Player _player)
    {
        moveEnabled = true;
        player = _player;
        origin = transform.position;
        SubscribeToInput();
    }

    public void DisableMovement() { UnsubscribeToInput(); moveEnabled = false; origin = transform.position; }

    public void ReturnToOrigin()
    {
        float temp = 0;
        DOTween.To(() => temp, x => temp = x, 1, (6 - transform.position.y) * 0.04f).OnComplete(() => { MoveRelative(origin - transform.position); MatchFieldUIPosition(); });;
    }

    private void MatchFieldUIPosition()
    {
        shopFieldUnitUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
    }
}
