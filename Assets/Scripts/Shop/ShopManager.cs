using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public float shopTime = 10; // 상점 지속시간
    public int shopUnitCount = 5; // 한번에 보여주는 유닛수
    public int coinPerRound = 10; // 라운드당 코인

    [HideInInspector] public float currentShopTimer;
    [HideInInspector] public ShopState state;
    [SerializeField] private Image shopTimerImg;

    private List<UnitSet> p1Shop = new();
    private List<UnitSet> p2Shop = new();

    public UnitDataHolder unitData;

    public int p1Coins = 0;
    public int p2Coins = 0;

    private void Awake()
    {
        Global.shopManager = this;
        try
        {
            shopTimerImg = GameObject.FindGameObjectWithTag("TIMER").GetComponent<Image>();
        }
        catch
        {
            enabled = false;
        }
    }

    // 상점모드 오픈
    public void OpenShop()
    {
        print("Shop Open");
        GetShopUnits();
        CreateShopCards();
        ShowShopUI();
        SetP1Coin(coinPerRound);
        SetP2Coin(coinPerRound);
        state = ShopState.Select;
        Global.OnShopOpen?.Invoke();
        StartShopTimer();
    }

    // 상점 종료
    public void CloseShop()
    {
        HideShopUI();
        state = ShopState.Closed;
        // 상점모드 끔
        Global.OnShopClose?.Invoke();
        //float temp = 0;
        //DOTween.To(() => temp, x => temp = x, 1, 0.01f).OnComplete(() => Global.OnShopClose?.Invoke());
    }

    // 유닛 판매
    public void Sell(Player player, int sellIndex)
    {
        if (player == Player.Player1)
        {
            ChangeP1Coin(Mathf.CeilToInt(Global.unitManager.P1UnitList[sellIndex].unitInfo.price*0.5f));
            Global.unitManager.P1UnitList.RemoveAt(sellIndex);
        }
        else if (player == Player.Player2)
        {
            ChangeP2Coin(Mathf.CeilToInt(Global.unitManager.P2UnitList[sellIndex].unitInfo.price * 0.5f));
            Global.unitManager.P2UnitList.RemoveAt(sellIndex);
        }
    }

    // 유닛 구매
    public bool Buy(Player player, int buyIndex)
    {
        if (player == Player.Player1)
        {
            if (Global.unitManager.P1UnitList.Count >= 5) return false;
            UnitSet set = p1Shop[buyIndex];
            if(set.price > p1Coins) return false;
            else
            {
                ChangeP1Coin(-set.price);
                UnitBase instance = Instantiate(set.fieldObject).GetComponent<UnitBase>();
                instance.player = player;
                instance.gameObject.GetComponent<ShopFieldUnitPlacer>().OnShopOpen();
                Global.unitManager.AddToP1Units(instance);
                instance.transform.position = Global.fieldManager.GetEmptyTileP1();
                instance.SetUnitInfo(set);
                return true;
            }
        }
        else if (player == Player.Player2)
        {
            if (Global.unitManager.P2UnitList.Count >= 5) return false;
            UnitSet set = p2Shop[buyIndex];
            if (set.price > p2Coins)
            {
                return false;
            }
            else
            {
                ChangeP2Coin(-set.price);
                UnitBase instance = Instantiate(set.fieldObject).GetComponent<UnitBase>();
                instance.player = player;
                instance.gameObject.GetComponent<ShopFieldUnitPlacer>().OnShopOpen();
                Global.unitManager.AddToP2Units(instance);
                instance.transform.position = Global.fieldManager.GetEmptyTileP2();
                instance.SetUnitInfo(set);
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
            shopTimerImg.fillAmount = 1 - currentShopTimer / shopTime;
            currentShopTimer -= Time.deltaTime;
            yield return null;
        }
        CloseShop();
        Global.roundManager.StartRound();
    }

    // 상점에 띄울 유닛 선택
    private void GetShopUnits()
    {
        p1Shop.Clear();
        for (int i = 0; i < shopUnitCount; i++)
        {
            p1Shop.Add(unitData.unitList[Random.Range(0, unitData.unitList.Count)]);
            p2Shop.Add(unitData.unitList[Random.Range(0, unitData.unitList.Count)]);
        }
    }

    private void ShowShopUI()
    {
        // 상점관련 ui보이게

    }

    private void HideShopUI()
    {
        // 상점 ui 지움
    }

    private void CreateShopCards()
    {
        for (int p = 0; p < 2; p++)
        {
            for (int i = 0; i < p1Shop.Count; i++)
            {
                UnitSet unit = p==0?p1Shop[i]:p2Shop[i];
                ShopCard instance = Instantiate(p==0?Global.assets.p1ShopCard:Global.assets.p2ShopCard, Global.uiParent).GetComponent<ShopCard>();
                instance.SetCard(unit, p == 0 ? Player.Player1 : Player.Player2,i);
            }
        }
    }
}
