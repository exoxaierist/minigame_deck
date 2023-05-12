using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopCard : UINavigatable
{
    [SerializeField] private Image image;
    [SerializeField] private TextMeshProUGUI nameArea;
    [SerializeField] private TextMeshProUGUI healthArea;
    [SerializeField] private TextMeshProUGUI powerArea;
    [SerializeField] private TextMeshProUGUI priceArea;
    [SerializeField] private TextMeshProUGUI descArea;

    private ShopManager shop;
    private int selfIndex = -1;

    public void SetCard(UnitSet unit, Player _player, int index)
    {
        shop = Global.shopManager;
        image.sprite = unit.image;
        nameArea.text = unit.name;
        healthArea.text = unit.health.ToString();
        powerArea.text = unit.power.ToString();
        priceArea.text = unit.price.ToString();
        descArea.text = unit.desc;
        player = _player;
        selfIndex = index;
        Activate();
    }

    public void RemoveCard()
    {
        Deactivate();
    }

    public override void OnSelect()
    {
        if(shop.state == ShopState.Select)
        {
            shop.Buy(player, selfIndex);
        }
    }
}
