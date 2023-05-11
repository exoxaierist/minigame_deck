using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public float shopTime = 30; // 상점 지속시간
    public int shopUnitCount = 5;
    public int coinPerRound = 10;

    [HideInInspector] public float currentShopTimer;

    private List<UnitBase> p1Shop = new();
    private List<UnitBase> p2Shop = new();

    public int p1Coins = 0;
    public int p2Coins = 0;

    public void OpenShop()
    {
        ShowShopUI();
        SetP1Coin(coinPerRound);
        SetP2Coin(coinPerRound);
        ResetUnitPosition();

        for (int i = 0; i < shopUnitCount; i++)
        {
            // p1Shop.Add(UnitManager.allUnits[Random()]);
            // p1Shop.Add(UnitManager.allUnits[Random()]);
        }
    }

    public void CloseShop()
    {
        HideShopUI();
        // 상점모드 끝
    }

    public void Sell(Player player, int sellIndex)
    {
        if (player == Player.Player1)
        {
            //돈 환급
            //p1deck.removeat(sellIndex);
        }
        else if (player == Player.Player2)
        {
            //돈 환급
            //p2deck.removeat(sellIndex);
        }
    }

    public void Buy(Player player, int buyIndex)
    {
        if (player == Player.Player1)
        {
            // if 돈없으면{}
            // else
            // 돈 --
            //p1deck.Add(p1Shop[buyIndex])
        }
        else if (player == Player.Player2)
        {
            // 돈 --
            //p2deck.Add(p2Shop[buyIndex])
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

    private void ShowShopUI()
    {
        // 상점관련 ui보이게
    }

    private void HideShopUI()
    {
        // 상점 ui 지움
    }

    private void ResetUnitPosition()
    {
        // 모든 유닛 원위치로
    }
}
