using System;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// �׸��忡 �����Ǹ� �����̴� ��� ������Ʈ�� ����Ŭ����
public class GridObject : EventObject
{
    [HideInInspector] public Vector2 offset = Vector2.zero;
    private float gridIncrement;

    protected bool isMoving = false;

    [Header("������� ������Ʈ")]
    public Transform visual;

    private bool stopMoveCheck = false;
    private Vector2 moveDirection;
    private int moveLeft;

    protected virtual void Awake()
    {
        gridIncrement = Global.gridIncrement;
        Global.OnTurnStart += EndTurn;
        SnapToGrid();
    }

    private void EndTurn() { if (isMoving) stopMoveCheck = true; }

    // ��������� �̵�
    public void MoveRelative(Vector2 dest)
    {
        moveDirection = dest.normalized;
        moveLeft = (int)dest.magnitude;
        Global.moveManager.Move(MoveCheck(10));
    }
    
    // �ڷ�ƾ �ƴ�
    private IEnumerator MoveCheck(int step)
    {
        yield return null;
        isMoving = true;
        Vector2 originPos = transform.position;
        Vector2 destPos = transform.position;
        while (step > 0 && moveLeft > 0)
        {
            print(Global.CheckOverlap(destPos + moveDirection, Global.collisionPlayer));
            while (moveLeft>0 && !Global.CheckOverlap(destPos + moveDirection, Global.collisionPlayer))
            {
                destPos += moveDirection;
                moveLeft--;
                transform.position = destPos;
                Physics2D.SyncTransforms();
            }
            
            step--;
            yield return null;
        }
        if (visual != null)
        {
            visual.localPosition = originPos - transform.position*Vector2.one;
            visual.DOComplete();
            visual.DOLocalJump(Vector2.zero, 0.2f, 1, 0.1f).SetEase(Ease.OutQuad).OnComplete(() => isMoving = false);
            visual.DOShakeScale(0.13f, new Vector3(0.1f, -0.1f, 0), 20);
        }
        OnMove();
        isMoving = false;
    } 

    // �ִϸ��̼� ���� �̵�
    public void Teleport(Vector2 dest)
    {
        transform.position = dest;
    }

    // �׸��忡 ����
    public void SnapToGrid() => transform.position = new Vector2(Mathf.Round(transform.position.x / gridIncrement), Mathf.Round(transform.position.y / gridIncrement)) + offset + Global.globalOffset;

    protected virtual void OnMove() { }
}
