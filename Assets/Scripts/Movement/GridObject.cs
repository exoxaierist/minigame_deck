using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    [HideInInspector] public Vector2 offset = Vector2.zero;
    private float gridIncrement;

    protected bool isMoving = false;

    [SerializeField] protected Transform visual;


    private void Start()
    {
        gridIncrement = Global.gridIncrement;
        SnapToGrid();
    }


    public void MoveRelative(Vector2 dest)
    {
        if (isMoving) return;
        isMoving = true;
        transform.DOJump(transform.position * Vector2.one + dest, 0.2f, 1, 0.1f).SetEase(Ease.OutQuad).OnComplete(() => isMoving = false);
        if (visual != null) visual.DOShakeScale(0.13f, new Vector3(0.1f, -0.1f, 0), 20);
    }

    public void SnapToGrid() => transform.position = new Vector2(Mathf.Round(transform.position.x / gridIncrement), Mathf.Round(transform.position.y / gridIncrement)) + offset + Global.globalOffset;
}
