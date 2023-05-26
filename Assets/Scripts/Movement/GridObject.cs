using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 그리드에 고정되며 움직이는 모든 오브젝트의 상위클래스
public class GridObject : EventObject
{
    [HideInInspector] public Vector2 offset = Vector2.zero;
    private float gridIncrement;

    protected bool isMoving = false;

    [Header("모션입힐 오브젝트")]
    public Transform visual;

    private bool stopMoveCheck = false;

    protected virtual void Awake()
    {
        gridIncrement = Global.gridIncrement;
        Global.OnTurnStart += EndTurn;
        SnapToGrid();
    }

    private void EndTurn() { if (isMoving) stopMoveCheck = true; }

    // 상대적으로 이동
    public void MoveRelative(Vector2 dest) => MoveRelative(dest, 0);

    public void MoveRelative(Vector2 dest, int collisionMask)
    {
        if (isMoving) return;
        isMoving = true;
        if (Global.CheckOverlap(transform.position * Vector2.one + dest, collisionMask)) { StartCoroutine(MoveRelativeRecheck(dest, collisionMask)); return; }
        transform.position = transform.position * Vector2.one + dest;
        if (visual != null)
        {
            visual.localPosition = -dest;
            visual.DOComplete();
            visual.DOLocalJump(Vector2.zero, 0.2f, 1, 0.1f).SetEase(Ease.OutQuad).OnComplete(() => isMoving = false);
            visual.DOShakeScale(0.13f, new Vector3(0.1f, -0.1f, 0), 20);
        }
        OnMove(); //움직일때 이벤트 발동
        isMoving = false;
    }

    private IEnumerator MoveRelativeRecheck(Vector2 dest, int collisionMask)
    {
        if (!stopMoveCheck)
        {
            yield return new WaitForEndOfFrame();
            MoveRelative(dest, collisionMask);
        }
        stopMoveCheck = false;
    }

    // 애니매이션 없이 이동
    public void Teleport(Vector2 dest)
    {
        transform.position = dest;
    }

    // 그리드에 스냅
    public void SnapToGrid() => transform.position = new Vector2(Mathf.Round(transform.position.x / gridIncrement), Mathf.Round(transform.position.y / gridIncrement)) + offset + Global.globalOffset;

    protected virtual void OnMove() { }
}
