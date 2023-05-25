using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Hp : MonoBehaviour
{
    [Header("UI")]
    public bool showHpUI = true;
    public Vector2 hpUIOffset = new(0, 0.8f);
    public bool autoParent = true;
    private Transform autoParentTransform;
    public Transform customUIParent;
    public HpUIType hpUIType;
    private HpUI hpUI;

    [Header("HP")]
    [SerializeField] protected int maxHp = 10;
    [SerializeField] protected int hp;
    public bool isDead = false;
    public int GetHp() => hp;

    public UnitBase unit;

    // �ǰ�, ��� �븮��
    public Action<UnitBase> OnDamage;
    public Action<UnitBase> OnHeal;
    public Action<UnitBase> OnHpChange;
    public Action<UnitBase> OnDeath;

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

        // �븮�� ȣ��
        if (value > 0) OnHeal?.Invoke(unit);
        else OnDamage?.Invoke(unit);
        CheckDeath();
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
    private void CheckDeath()
    {
        if (hp <= 0) Death();
    }

    // ����
    private void Death()
    {
        hp = 0;
        isDead = true;
        OnDeath?.Invoke(unit);
    }

    // HP UI������Ʈ ����
    private HpUI CreateHpBar()
    {
        print(autoParentTransform);
        GameObject instance = Instantiate(Global.assets.hpUI, autoParent?autoParentTransform:customUIParent);
        instance.transform.localPosition = hpUIOffset;
        instance.GetComponent<HpUI>().Set(maxHp,hp,hpUIType);
        return instance.GetComponent<HpUI>();
    }
}
