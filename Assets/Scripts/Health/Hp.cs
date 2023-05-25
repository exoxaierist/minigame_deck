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

    // 피격, 사망 대리자
    public Action OnDamage;
    public Action OnHeal;
    public Action OnHpChange;
    public Action OnDeath;

    //피격시, 받는 데미지를 바꾸는 대리자
    //public Func<int, int> DamageModifier;

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

        // 대리자 호출
        if (value > 0) OnHeal?.Invoke();
        else OnDamage?.Invoke();
        CheckDeath();
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
    private void CheckDeath()
    {
        if (hp <= 0) Death();
    }

    // 죽임
    private void Death()
    {
        hp = 0;
        isDead = true;
        OnDeath?.Invoke();
    }

    // HP UI오브젝트 생성
    private HpUI CreateHpBar()
    {
        print(autoParentTransform);
        GameObject instance = Instantiate(Global.assets.hpUI, autoParent?autoParentTransform:customUIParent);
        instance.transform.localPosition = hpUIOffset;
        instance.GetComponent<HpUI>().Set(maxHp,hp,hpUIType);
        return instance.GetComponent<HpUI>();
    }
}
