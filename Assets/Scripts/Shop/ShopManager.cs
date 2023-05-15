using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public float shopTime = 30; // ���� ���ӽð�
    public int shopUnitCount = 5; // �ѹ��� �����ִ� ���ּ�
    public int coinPerRound = 10; // ����� ����

    [HideInInspector] public float currentShopTimer;
    [HideInInspector] public ShopState state;

    private List<UnitSet> p1Shop = new();
    private List<UnitSet> p2Shop = new();

    public UnitDataHolder unitData;

    public int p1Coins = 0;
    public int p2Coins = 0;

    private void Awake()
    {
        Global.shopManager = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (state != ShopState.Closed) CloseShop();
            else OpenShop();
        }
    }

    // ������� ����
    public void OpenShop()
    {
        print("Shop Open");
        GetShopUnits();
        CreateShopCards();
        ShowShopUI();
        SetP1Coin(coinPerRound);
        SetP2Coin(coinPerRound);
        state = ShopState.Select;

        for (int i = 0; i < shopUnitCount; i++)
        {
            // p1Shop.Add(UnitManager.allUnits[Random()]);
            // p1Shop.Add(UnitManager.allUnits[Random()]);
        }
        Global.OnShopOpen?.Invoke();
    }

    // ���� ����
    public void CloseShop()
    {
        HideShopUI();
        state = ShopState.Closed;
        // ������� ��
        float temp = 0;
        DOTween.To(() => temp, x => temp = x, 1, 0.5f).OnComplete(() => Global.OnShopClose?.Invoke());
    }

    // ���� �Ǹ�
    public void Sell(Player player, int sellIndex)
    {
        if (player == Player.Player1)
        {
            //�� ȯ��
            //p1deck.removeat(sellIndex);
        }
        else if (player == Player.Player2)
        {
            //�� ȯ��
            //p2deck.removeat(sellIndex);
        }
    }

    // ���� ����
    public bool Buy(Player player, int buyIndex)
    {
        if (player == Player.Player1)
        {
            //���� ����������� return false
            UnitSet set = p1Shop[buyIndex];
            if(set.price > p1Coins) 
            {
                return false;
            } // �� �� ���� 
            else
            {
                ChangeP1Coin(-set.price);
                //p1deck.Add(p1Shop[buyIndex])
                return true;
            }
        }
        else if (player == Player.Player2)
        {
            //���� ����������� return false
            UnitSet set = p2Shop[buyIndex];
            if (set.price > p2Coins) 
            {
                return false;
            } // �� �� ����
            else
            {
                ChangeP2Coin(-set.price);
                //p2deck.Add(p2Shop[buyIndex])
                return true;
            }
        }
        return false;
    }

    public void SetP1Coin(int coin) { p1Coins = coin; Global.OnP1CoinChange?.Invoke(); }
    public void SetP2Coin(int coin) { p2Coins = coin; Global.OnP2CoinChange?.Invoke(); }
    public void ChangeP1Coin(int amount) => SetP1Coin(Mathf.Clamp(p1Coins + amount, 0, 50));
    public void ChangeP2Coin(int amount) => SetP2Coin(Mathf.Clamp(p2Coins + amount, 0, 50));

    private void StartShopTimer()
    {
        StartCoroutine(ShopTimerCoroutine());
    }

    private IEnumerator ShopTimerCoroutine()
    {
        currentShopTimer = shopTime;
        while(currentShopTimer > 0)
        {
            currentShopTimer -= Time.deltaTime;
            yield return null;
        }
        CloseShop();
    }

    // ������ ��� ���� ����
    private void GetShopUnits()
    {
        p1Shop.Clear();
        for (int i = 0; i < shopUnitCount; i++)
        {
            p1Shop.Add(unitData.unitList[Random.Range(0, unitData.unitList.Count - 1)]);
            p2Shop.Add(unitData.unitList[Random.Range(0, unitData.unitList.Count - 1)]);
        }
    }

    private void ShowShopUI()
    {
        // �������� ui���̰�

    }

    private void HideShopUI()
    {
        // ���� ui ����
    }

    private void CreateShopCards()
    {
        for (int p = 0; p < 2; p++)
        {
            for (int i = 0; i < p1Shop.Count; i++)
            {
                UnitSet unit = p==0?p1Shop[i]:p2Shop[i];
                ShopCard instance = Instantiate(Global.assets.shopCard, Global.uiParent).GetComponent<ShopCard>();
                instance.SetCard(unit, p == 0 ? Player.Player1 : Player.Player2,i);
            }
        }
    }
}