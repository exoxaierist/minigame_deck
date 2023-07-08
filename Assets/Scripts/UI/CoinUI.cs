using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public Player player;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(player == Player.Player1)
        {
            Global.OnP1CoinChange += Refresh;
        }
        else if(player == Player.Player2)
        {
            Global.OnP2CoinChange += Refresh;
        }
    }

    private void Refresh()
    {
        text.text = (player==Player.Player1?Global.shopManager.p1Coins.ToString():Global.shopManager.p2Coins.ToString());
    }
}
