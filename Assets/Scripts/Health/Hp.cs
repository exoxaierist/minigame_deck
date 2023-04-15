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

    [Header("HP")]
    [SerializeField] protected int maxHp = 10;
    [SerializeField] protected int hp;

    private HpUI hpUI;

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

    public void AddToHP(int value)
    {
        hp = Mathf.Clamp(hp + value, 0, maxHp);
        hpUI.SetHP(hp);
    }

    private void CheckDeath()
    {
        if (hp <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        hp = 0;
    }

    private HpUI CreateHpBar()
    {
        GameObject instance = Instantiate(Global.assets.hpUI, customUIParent==null?transform:customUIParent);
        instance.transform.localPosition = hpUIOffset;
        instance.GetComponent<HpUI>().Set(maxHp,hp,hpUIType);
        return instance.GetComponent<HpUI>();
    }
}
