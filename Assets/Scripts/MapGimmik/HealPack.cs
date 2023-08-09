using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPack : MonoBehaviour
{
    private int turnTimer = 0;
    private int nowTurn = 0;

    private GameObject healPack = null;

    private void randomSpawn()
    {
        Vector2 targetPos = new Vector2();
        do
        {
            targetPos = Global.fieldManager.GetRandomTile();
        }
        while (Physics2D.OverlapBox(targetPos, new Vector2(1, 1), 1) != null);
        transform.position = targetPos;
        healPack.SetActive(true);

        nowTurn = 0;
        turnTimer = 0;
    }
    private void getRandomTurn()
    {
        turnTimer = Random.Range(5, 8);
    }
    private void healPackActive()
    {
        if (healPack.activeSelf == true) return;
        if (turnTimer == 0) getRandomTurn();
        if (turnTimer <= nowTurn) randomSpawn();
        nowTurn++;
    }
    private void findObject()
    {
        healPack = transform.Find("HealPack").gameObject;
        healPack.SetActive(false);
    }
    private void healPackCollisionCheck()
    {
        Collider2D collision = Physics2D.OverlapBox(transform.position, new Vector2(0.8f, 0.8f), 1);
        if(collision != null)
        {
            healPack.SetActive(false);
            collision.gameObject.GetComponent<Hp>().AddToHP(2);
        }
    }
    private void Update()
    {
        healPackCollisionCheck();
    }
    private void Awake()
    {
        findObject();
    }
    private void Start()
    {
        Global.OnTurnStart += healPackActive;
    }
}
