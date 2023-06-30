using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class RoundCounter : MonoBehaviour
{
    public Player player;
    private int score = 0;
    private int newScore = 0;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        Global.OnRoundEnd += UpdateCount;
        text.text = "0";
    }

    private void UpdateCount()
    {
        if (player == Player.Player1) newScore = Global.roundManager.p1Score;
        if (player == Player.Player2) newScore = Global.roundManager.p2Score;
        if(newScore != score)
        {
            transform.DORewind();
            transform.DOPunchPosition(new(20, 20, 0), 0.5f,18);
            score = newScore;
            text.text = score.ToString();
        }
    }
}
