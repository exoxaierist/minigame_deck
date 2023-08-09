using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Spike : MonoBehaviour
{
    private GameObject spikeHole = null;
    private GameObject spike = null;
    private int nowTurn = 0;
    private void moveRandomPos()
    {
        Vector2 targetPos = new Vector2();
        do
        {
            targetPos = Global.fieldManager.GetRandomTile();
        }
        while (Physics2D.OverlapBox(targetPos, new Vector2(1, 1), 1) != null);

        spikeHole.transform.position = targetPos;
    }
    private void spikeOut()
    {
        spike.SetActive(true);
        AttackInfo info = new AttackInfo()
        {
            attacker = null,
            damage = 1,
        };
        Global.atkPooler.Get().Attack(spikeHole.transform.position * Vector2.one, info);
    }
    private void spikeIn()
    {
        spike.SetActive(false);
    }
    private void spikeActive()
    {
        if(nowTurn < 1) spikeOut();
        else if(nowTurn < 2) spikeIn();
        nowTurn++;
        if(nowTurn >= 2) nowTurn = 0;
    }
    private void findObject()
    {
        spikeHole = this.gameObject;
        spike = this.transform.Find("Spike").gameObject;
        spikeIn();
    }
    private void Awake()
    {
        findObject();
    }
    private void Start()
    {
        Global.OnTurnStartLate += spikeActive;
        Global.OnRoundStart += moveRandomPos;
    }
}
