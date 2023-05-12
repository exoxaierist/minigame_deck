using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public float shopTime = 30; // ���� ���ӽð�
    public int shopUnitCount = 5;
    public int coinPerRound = 10;

    [HideInInspector] public float currentShopTimer;
    [HideInInspector] public ShopState state;

    private List<UnitSet> p1Shop = new();
    private List<UnitSet> p2Shop = new();

    public int p1Coins = 0;
    public int p2Coins = 0;

    private void Awake()
    {
        Global.shopManager = this;
    }

    public void OpenShop()
    {
        GetShopUnits();
        ShowShopUI();
        SetP1Coin(coinPerRound);
        SetP2Coin(coinPerRound);
        ResetUnitPosition();
        state = ShopState.Select;

        for (int i = 0; i < shopUnitCount; i++)
        {
            // p1Shop.Add(UnitManager.allUnits[Random()]);
            // p1Shop.Add(UnitManager.allUnits[Random()]);
        }
    }

    public void CloseShop()
    {
        HideShopUI();
        state = ShopState.Deactivated;
        // ������� ��
    }

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

    public void Buy(Player player, int buyIndex)
    {
        if (player == Player.Player1)
        {
            UnitSet set = new(); // ���� �޾ƾ���
            if(set.price > p2Coins) { } // �� �� ���� 
            else
            {
                ChangeP1Coin(-set.price);
            //p1deck.Add(p1Shop[buyIndex])
            }
        }
        else if (player == Player.Player2)
        {
            UnitSet set = new(); // ���� �޾ƾ���
            if (set.price > p1Coins) { } // �� �� ����
            else
            {
                ChangeP2Coin(-set.price);
            //p2deck.Add(p2Shop[buyIndex])
            }
        }
    }

    public void SetP1Coin(int coin) => p1Coins = coin;
    public void SetP2Coin(int coin) => p2Coins = coin;
    public void ChangeP1Coin(int amount) => p1Coins = Mathf.Clamp(p1Coins + amount, 0, 50);
    public void ChangeP2Coin(int amount) => p2Coins = Mathf.Clamp(p2Coins + amount, 0, 50);

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

    private void GetShopUnits()
    {

    }

    private void ShowShopUI()
    {
        // �������� ui���̰�
    }

    private void HideShopUI()
    {
        // ���� ui ����
    }

    private void ResetUnitPosition()
    {
        // ��� ���� ����ġ��
    }
}
