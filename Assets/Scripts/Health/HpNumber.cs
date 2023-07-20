using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HpNumber : MonoBehaviour
{
    private int hp = 0;
    public TextMeshPro text;

    public void SetNumber(int _hp)
    {
        if (hp == _hp) return;
        hp = _hp;
        text.text = hp.ToString();
        transform.DOComplete();
        transform.DOShakePosition(0.1f, new Vector3(0.02f, 0.02f, 0), 20);
    }
}
