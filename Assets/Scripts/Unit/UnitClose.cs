using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;

public class UnitClose : UnitBase
{
    public List<UnitBase> ValidUnits = new List<UnitBase>();
    public List<UnitBase> AllyUnits = new List<UnitBase>();
    [SerializeField]
    private List<UnitBase> lastAllyUnits = new List<UnitBase>();
    public List<UnitBase> EnemyUnits = new List<UnitBase>();
    [SerializeField]
    private List<UnitBase> lastEnemyUnits = new List<UnitBase>();

    /// <summary>
    /// 근처에 있는 유닛을 반환해주는 함수
    /// </summary>
    private void getCloseUnit()
    {
        ValidUnits = new List<UnitBase>();
        Collider2D[] hit = Physics2D.OverlapBoxAll(transform.position, new Vector2(2.9f, 2.9f), 0);
        if (hit == null) return;
        foreach(Collider2D collider in hit)
        {
            if (collider.gameObject == this.gameObject) continue;
            ValidUnits.Add(collider.GetComponent<UnitBase>());
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(2.9f, 2.9f, 0));
    }
    /// <summary>
    /// 주변에 아군이 있는지 확인하는 함수
    /// </summary>
    /// <returns>주변에 아군이 있는지 true/false</returns>
    public bool GetCloseAllys()
    {
        if(AllyUnits.Count > 0) lastAllyUnits = AllyUnits.GetRange(0, AllyUnits.Count);
        getCloseUnit();
        AllyUnits = new List<UnitBase>();
        List<UnitBase> targetList = new List<UnitBase>();
        if(UnitPool.Instance.P1Unit.Contains(this))
        {
            targetList = UnitPool.Instance.P1Unit;
        }
        else
        {
            targetList = UnitPool.Instance.P2Unit;
        }
        foreach(UnitBase unitBase in ValidUnits)
        {
            if(targetList.Contains(unitBase))
            {
                AllyUnits.Add(unitBase);
            }
        }
        if (AllyUnits.Count > 0) return true;
        else return false;
    }
    /// <summary>
    /// 주변에 적군이 있는지 확인하는 함수
    /// </summary>
    /// <returns>주변에 적군이 있는지 true/false</returns>
    public bool GetCloseEnemys()
    {
        if(EnemyUnits.Count > 0) lastEnemyUnits = EnemyUnits.GetRange(0, EnemyUnits.Count);
        getCloseUnit();
        EnemyUnits = new List<UnitBase>();
        List<UnitBase> targetList = new List<UnitBase>();
        if (UnitPool.Instance.P1Unit.Contains(this))
        {
            targetList = UnitPool.Instance.P2Unit;
        }
        else
        {
            targetList = UnitPool.Instance.P1Unit;
        }
        foreach (UnitBase unitBase in ValidUnits)
        {
            if (targetList.Contains(unitBase))
            {
                EnemyUnits.Add(unitBase);
            }
        }
        if (EnemyUnits.Count > 0) return true;
        else return false;
    }
    /// <summary>
    /// 아군과 떨어졌는지 확인하는 함수
    /// </summary>
    /// <returns>떨어졌는지 true/false</returns>
    public bool SeperatedFromAlly()
    {
        GetCloseAllys();
        foreach(UnitBase unitBase in lastAllyUnits)
        {
            if (AllyUnits.Contains(unitBase)) continue;
            else return true;
        }
        return false;
    }
    /// <summary>
    /// 적군과 떨어졌는지 확인하는 함수
    /// </summary>
    /// <returns>떨어졌는지 true/false</returns>
    public bool SeperatedFromEnemy()
    {
        GetCloseEnemys();
        foreach (UnitBase unitBase in lastEnemyUnits)
        {
            if (EnemyUnits.Contains(unitBase)) continue;
            else return true;
        }
        return false;
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
