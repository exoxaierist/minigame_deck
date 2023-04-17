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


    private void Start()
    {
        gridIncrement = Global.gridIncrement;
        SnapToGrid();
    }

    // 상대적으로 이동
    public void MoveRelative(Vector2 dest)
    {
        if (isMoving) return;
        isMoving = true;
        transform.position = transform.position * Vector2.one + dest;
        if (visual != null)
        {
            visual.localPosition = -dest;
            visual.DOLocalJump(Vector2.zero, 0.2f, 1, 0.1f).SetEase(Ease.OutQuad).OnComplete(() => isMoving = false);
            visual.DOShakeScale(0.13f, new Vector3(0.1f, -0.1f, 0), 20);
        }
        else { isMoving = false; }
    }

    // 애니매이션 없이 이동
    public void Teleport(Vector2 dest)
    {
        transform.position = dest;
    }

    // 그리드에 스냅
    public void SnapToGrid() => transform.position = new Vector2(Mathf.Round(transform.position.x / gridIncrement), Mathf.Round(transform.position.y / gridIncrement)) + offset + Global.globalOffset;
}
