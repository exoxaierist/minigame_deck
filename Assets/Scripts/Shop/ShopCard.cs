using DG.Tweening;
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
    private UnitSet unit;
    private int selfIndex = -1;
    private Vector3 placedPosition;

    private void Start()
    {
        Global.OnShopClose += RemoveCard;
    }

    // 상점카드 이미지, 텍스트 등 세팅
    public void SetCard(UnitSet _unit, Player _player, int index)
    {
        shop = Global.shopManager;
        image.sprite = _unit.image1;
        nameArea.text = _unit.name;
        healthArea.text = _unit.health.ToString();
        powerArea.text = _unit.power.ToString();
        priceArea.text = _unit.price.ToString();
        descArea.text = _unit.desc;
        unit = _unit;
        player = _player;
        selfIndex = index;
        transform.DOComplete();
        if (player == Player.Player1)
        {
            placedPosition = new(-800, 250 - (150 * index), 0);
        }
        else if (player == Player.Player2)
        {
            placedPosition = new(800, 250 - (150 * index), 0);
        }
        transform.localPosition = new(placedPosition.x * 1.2f, placedPosition.y, 0);
        transform.DOLocalMoveX(placedPosition.x, 0.4f).SetDelay(0.04f * selfIndex).OnComplete(() => Activate());
    }

    // 카드 해제 (상점 종료)
    public void RemoveCard()
    {
        Deactivate();
        transform.DOComplete();
        transform.DOLocalMoveX(1200 * (player == Player.Player1 ? -1 : 1), 0.4f).SetDelay(0.08f * selfIndex).OnComplete(() => Delete());
    }

    private void Delete()
    {
        Global.OnShopClose -= RemoveCard;
        Destroy(this.gameObject);
    }

    // Navigatable 상속
    public override void OnSelect()
    {
        if(shop.state != ShopState.Closed)
        {
            if (!shop.Buy(player, selfIndex))
            {
                CannotBuy();
            }
        }
    }

    public override void OnFocusIn()
    {
        transform.DOComplete();
        transform.DOLocalMoveX(placedPosition.x*0.97f,0.1f);
    }

    public override void OnFocusOut()
    {
        transform.DOComplete();
        transform.DOLocalMoveX(placedPosition.x, 0.1f);
    }

    private void CannotBuy()
    {
        transform.DORewind();
        transform.DOShakeRotation(2f, 3, 20);
        transform.DOShakePosition(0.5f, 5, 20);
    }
}
