using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpCounterUI : MonoBehaviour
{
    private SpriteRenderer spr;
    private Sprite emptySprite;
    private Sprite fullSprite;
    private Vector3 origin;

    private void OnEnable()
    {
        spr = GetComponent<SpriteRenderer>();
        emptySprite = Global.assets.hpCounterSpriteEmpty;
        fullSprite = Global.assets.hpCounterSpriteFull;
        SetFullSprite();
    }

    private void SetEmptySprite() => spr.sprite = emptySprite;
    public void SetEmpty() => SetEmpty(0);
    public void SetEmpty(float delay)
    {
        Invoke(nameof(SetEmptySprite), delay);
        transform.DOShakePosition(0.1f, new Vector3(0.07f, 0.07f, 0),30).SetDelay(delay);
        transform.DOShakeRotation(0.1f, new Vector3(0, 0, 20), 30).OnComplete(() => ToOrigin()).SetDelay(delay);
    }

    private void SetFullSprite() => spr.sprite = fullSprite;
    public void SetFull() => SetFull(0);
    public void SetFull(float delay)
    {
        this.Invoke(nameof(SetFullSprite), delay);
        transform.DOPunchPosition(new Vector3(0, 0.1f, 0), 0.2f, 15).SetDelay(delay);
        transform.DOPunchScale(new Vector3(0.1f, 0.1f, 0), 0.2f).OnComplete(() => ToOrigin()).SetDelay(delay);
    }

    public void SetOrigin()
    {
        origin = transform.localPosition;
    }

    private void ToOrigin()
    {
        transform.localPosition = origin;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;
    }
}
