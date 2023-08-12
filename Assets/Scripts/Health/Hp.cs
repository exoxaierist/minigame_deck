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

    // �ǰ�, ��� �븮��
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
        // �׽�Ʈ��
        if (Input.GetKeyDown(KeyCode.Comma)) AddToHP(-3);
        if (Input.GetKeyDown(KeyCode.Period)) AddToHP(2);
    }

    // ü�� �����Ҷ� ���
    public void AddToHP(int value)
    {
        value = DamageModifier(value);//������ �ޱ� �� ó��

        if (value == 0) return;
        hp = Mathf.Clamp(hp + value, 0, maxHp);
        hpUI.SetHP(hp);

        if (CheckDeath()) return;

        // �븮�� ȣ��
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

        #region ������ ���̴� �κ�
        if (TryGetComponent(out TemporaryReduceDamage temporaryReduceDamage))
        {
            var isReduceDamage = temporaryReduceDamage.getIsReduceDamage();
            var ReduceDamage = temporaryReduceDamage.getReduceDamage();

            if (isReduceDamage)
            {
                if (value >= 0)//�� üũ
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

    // �׾����� Ȯ��
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

    // ����
    private void Death()
    {
        hp = 0;
        isDead = true;
        OnDeath?.Invoke(unit);
    }

    //�츲
    private void Revive()
    {
        ResetHP();
        OnRevive?.Invoke(unit);
    }

    // HP UI������Ʈ ����
    private HpUI CreateHpBar()
    {
        GameObject instance = Instantiate(Global.assets.hpUI, autoParent?autoParentTransform:customUIParent);
        instance.transform.localPosition = hpUIOffset;
        instance.GetComponent<HpUI>().Set(maxHp,hp,hpUIType);
        return instance.GetComponent<HpUI>();
    }
}
