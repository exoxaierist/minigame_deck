using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFieldUnitUI : UINavigatable
{
    private bool placeMode = false;
    
    [HideInInspector] public ShopFieldUnitPlacer placer;

    private void Start()
    {
        Global.OnShopClose += RemoveSelf;
    }

    public override void OnSelect()
    {
        if (!placeMode) EnablePlaceMode();
    }

    public override void OnSell()
    {
        DisablePlaceMode();
        Global.shopManager.Sell( player, Global.unitManager.GetMyIndex(placer.GetComponent<UnitBase>(), player)); ;
        placer.RemoveSelf();
        RemoveSelf();
    }

    private void OnDestroy()
    {
        Deactivate();
    }

    private void EnablePlaceMode()
    {
        if (placeMode) return;
        placeMode = true;
        if (player == Player.Player1)
        {
            Global.uiNavManager.freezeP1 = true;
            Global.OnP1Select += Confirm;
        }
        else if (player == Player.Player2)
        {
            Global.uiNavManager.freezeP2 = true;
            Global.OnP2Select += Confirm;
        }
        placer.EnableMovement(player);
    }

    private void DisablePlaceMode()
    {
        if (!placeMode) return;
        placeMode = false;
        if (player == Player.Player1)
        {
            Global.uiNavManager.freezeP1 = false;
            Global.OnP1Select -= Confirm;
        }
        else if (player == Player.Player2)
        {
            Global.uiNavManager.freezeP2 = false;
            Global.OnP2Select -= Confirm;
        }
        Global.uiNavManager.SearchNavigation();
        placer.DisableMovement();
    }

    // 옮기는중 선택키 눌렀을때
    private void Confirm()
    {
        DisablePlaceMode();
    }

    private void RemoveSelf()
    {
        Global.uiNavManager.freezeP1 = false;
        Global.uiNavManager.freezeP2 = false;
        Global.OnShopClose -= RemoveSelf;
        Deactivate();
        Destroy(gameObject);
    }
}
