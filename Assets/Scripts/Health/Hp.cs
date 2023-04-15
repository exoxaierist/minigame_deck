using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hp : MonoBehaviour
{
    [Header("UI")]
    public bool showHpUI = true;
    public Vector2 hpUIOffset = new(0, 0.8f);
    public Transform customUIParent;
    public HpUIType hpUIType;
    private HpUI hpUI;

    [Header("HP")]
    [SerializeField] protected int maxHp = 10;
    [SerializeField] protected int hp;

    // 피격, 사망 대리자
    public Action OnDamage;
    public Action OnHeal;
    public Action OnHpChange;
    public Action OnDeath;

    private void Start()
    {
        hp = maxHp;
        if (showHpUI)
        {
            hpUI = CreateHpBar();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Comma)) AddToHP(-3);
        if (Input.GetKeyDown(KeyCode.Period)) AddToHP(2);
    }

    // 체력 변경할때 사용
    public void AddToHP(int value)
    {
        if (value == 0) return;
        hp = Mathf.Clamp(hp + value, 0, maxHp);
        hpUI.SetHP(hp);

        // 대리자 호출
        if (value > 0) OnHeal?.Invoke();
        else OnDamage?.Invoke();
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (hp <= 0) Death();
    }

    private void Death()
    {
        hp = 0;
        OnDeath?.Invoke();
    }

    private HpUI CreateHpBar()
    {
        GameObject instance = Instantiate(Global.assets.hpUI, customUIParent==null?transform:customUIParent);
        instance.transform.localPosition = hpUIOffset;
        instance.GetComponent<HpUI>().Set(maxHp,hp,hpUIType);
        return instance.GetComponent<HpUI>();
    }
}
