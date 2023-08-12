using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Hp : MonoBehaviour
{
    [HideInInspector] public bool showHpUI = true;
    [HideInInspector] public Vector2 hpUIOffset = new(0, 0.8f);
    [HideInInspector] public bool autoParent = true;
    private Transform autoParentTransform;
    [HideInInspector] public Transform customUIParent;
    [HideInInspector] public HpUIType hpUIType = HpUIType.Number;
    private HpUI hpUI;

    [HideInInspector] public int maxHp = 10;
    [HideInInspector] public int hp;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isDamaged = false;
    public int GetHp() => hp;
    public int GetMaxHP() => maxHp;

    [HideInInspector] public UnitBase unit;

    // 피격, 사망 대리자
    public Action<UnitBase> OnDamage;
    public Action<UnitBase> OnHeal;
    public Action<UnitBase> OnHpChange;
    public Action<UnitBase> OnDeath;
    public Action<UnitBase> OnRevive;

    private void Start()
    {
        hp = maxHp;
        if (autoParent && TryGetComponent(out GridObject gridobj)) autoParentTransform = gridobj.visual;
        if (showHpUI) hpUI = CreateHpBar();
        
    }

    private void Update()
    {
        // 테스트용
        if (Input.GetKeyDown(KeyCode.Comma)) AddToHP(-3);
        if (Input.GetKeyDown(KeyCode.Period)) AddToHP(2);
    }

    // 체력 변경할때 사용
    public void AddToHP(int value)
    {
        value = DamageModifier(value);//데미지 받기 전 처리

        if (value == 0) return;
        hp = Mathf.Clamp(hp + value, 0, maxHp);
        hpUI.SetHP(hp);

        if (CheckDeath()) return;

        // 대리자 호출
        if (value > 0) OnHeal?.Invoke(unit);
        else
        {
            OnDamage?.Invoke(unit);
            isDamaged = true;
        }
        CheckDeath();
    }

    public void ResetHP()
    {
        hp = maxHp;
        isDead = false;
        hpUI.SetHP(hp);
    }

    protected int DamageModifier(int _value)
    {
        var value = _value;

        #region 데미지 줄이는 부분
        if (TryGetComponent(out TemporaryReduceDamage temporaryReduceDamage))
        {
            var isReduceDamage = temporaryReduceDamage.getIsReduceDamage();
            var ReduceDamage = temporaryReduceDamage.getReduceDamage();

            if (isReduceDamage)
            {
                if (value >= 0)//힐 체크
                {
                    return value;
                }

                int x = value + ReduceDamage;

                if (x >= 0)
                {
                    value = 0;
                }
                else
                {
                    value = x;
                }
            }
        }
        #endregion
        return value;
    }

    // 죽었는지 확인
    private bool CheckDeath()
    {
        if (hp <= 0)
        {
            Death();
            return true;
        }
        else 
        { 
            return false; 
        }
    }

    // 죽임
    private void Death()
    {
        hp = 0;
        isDead = true;
        OnDeath?.Invoke(unit);
    }

    //살림
    private void Revive()
    {
        ResetHP();
        OnRevive?.Invoke(unit);
    }

    // HP UI오브젝트 생성
    private HpUI CreateHpBar()
    {
        GameObject instance = Instantiate(Global.assets.hpUI, autoParent?autoParentTransform:customUIParent);
        instance.transform.localPosition = hpUIOffset;
        instance.GetComponent<HpUI>().Set(maxHp,hp,hpUIType);
        return instance.GetComponent<HpUI>();
    }
}
