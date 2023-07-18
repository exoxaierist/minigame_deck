using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMirror : UnitBase
{
    private int attackDamage = 2;
    private int moveDistanceBuffer = 1;

    private void moveInvertEnemies()
    {
        List<UnitBase> enemyUnitList = null;

        if (player == Player.Player1) enemyUnitList = Global.unitManager.P2UnitList;
        else enemyUnitList = Global.unitManager.P1UnitList;

        int listCount = enemyUnitList.Count;

        for(int i = 0; i < listCount; i++)
        {
            enemyUnitList[i].invertMovement = true;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        UnitPropertiesSet(new Vector2[] {new(3, 0)},
            new AttackInfo
            {
                damage = attackDamage,
                attacker = this,
            },
            moveDistanceBuffer);
        Global.OnTurnStart += moveInvertEnemies;
    }
}
