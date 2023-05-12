using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopFieldUnit : UINavigatable
{
    private bool placeMode = false;
    public override void OnSelect()
    {
        
    }

    private void EnablePlaceMode()
    {
        if (placeMode) return;
        placeMode = true;
        if (player == Player.Player1) Global.uiNavManager.freezeP1 = true;
        else if (player == Player.Player2) Global.uiNavManager.freezeP2 = true;
    }

    private void DisablePlaceMode()
    {

    }
}
