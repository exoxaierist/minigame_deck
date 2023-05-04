using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITurnTimer : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        if (!TryGetComponent(out image)) this.enabled = false;
    }

    private void Update()
    {
        image.fillAmount = Global.turnManager.GetTurnTimer() / Global.turnManager.turnDuration;
    }
}
