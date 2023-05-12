using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(AudioSource))]
public class NewButton : UINavigatable, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Image image;
    private AudioSource audioSrc;
    [Header("Sprite Swap")]
    public Sprite defaultSprite;
    public Sprite hoverSprite;
    public Sprite pressedSprite;

    [Header("Audio Clip")]
    public AudioClip hoverClip;
    public AudioClip pressClip;

    private float pressedDuration = 0.1f;
    private bool hover = false;

    [Header("UnityEvent")]
    public UnityEvent buttonEvent;
    private Coroutine pressedCoroutine;


    private void Awake()
    {
        image = GetComponent<Image>();
        audioSrc = GetComponent<AudioSource>();
        audioSrc.playOnAwake = false;
        audioSrc.bypassListenerEffects = true;
        audioSrc.bypassReverbZones = true;
        Activate();
    }

    private void OnDestroy()
    {
        Deactivate();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit();
    }

    public override void OnFocusIn()
    {
        OnHoverEnter();
    }

    public override void OnFocusOut()
    {
        OnHoverExit();
    }

    public override void OnSelect()
    {
        OnClick();
    }

    public void OnClick()
    {
        if (pressedCoroutine != null) return;
        buttonEvent.Invoke();
        pressedCoroutine = StartCoroutine(SpritePressed());
        audioSrc.clip = pressClip;
        audioSrc.Play();
    }

    public void OnHoverEnter()
    {
        hover = true;
        if (pressedCoroutine != null) return;
        image.sprite = hoverSprite;
        audioSrc.clip = hoverClip;
        audioSrc.Play();
    }

    public void OnHoverExit()
    {
        hover = false;
        if (pressedCoroutine != null) return;
        image.sprite = defaultSprite;
    }

    private IEnumerator SpritePressed()
    {
        image.sprite = pressedSprite;
        yield return new WaitForSecondsRealtime(pressedDuration);
        if (hover) image.sprite = hoverSprite;
        else image.sprite = defaultSprite;
        pressedCoroutine = null;
    }
}
