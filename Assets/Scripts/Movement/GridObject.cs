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


    private void Start()
    {
        gridIncrement = Global.gridIncrement;
        SnapToGrid();
    }

    // ��������� �̵�
    public void MoveRelative(Vector2 dest) => MoveRelative(dest, 0);

    public void MoveRelative(Vector2 dest, LayerMask collisionMask)
    {
        if (Global.CheckOverlap(transform.position * Vector2.one + dest, collisionMask)) { StartCoroutine(MoveRelativeRecheck(dest, collisionMask)); return; }
        if (isMoving) return;
        isMoving = true;
        transform.position = transform.position * Vector2.one + dest;
        if (visual != null)
        {
            visual.localPosition = -dest;
            visual.DOComplete();
            visual.DOLocalJump(Vector2.zero, 0.2f, 1, 0.1f).SetEase(Ease.OutQuad).OnComplete(() => isMoving = false);
            visual.DOShakeScale(0.13f, new Vector3(0.1f, -0.1f, 0), 20);
        }
        else { isMoving = false; }
    }

    private IEnumerator MoveRelativeRecheck(Vector2 dest, LayerMask collisionMask)
    {
        yield return new WaitForEndOfFrame();
        MoveRelative(dest, collisionMask);
    }

    // �ִϸ��̼� ���� �̵�
    public void Teleport(Vector2 dest)
    {
        transform.position = dest;
    }

    // �׸��忡 ����
    public void SnapToGrid() => transform.position = new Vector2(Mathf.Round(transform.position.x / gridIncrement), Mathf.Round(transform.position.y / gridIncrement)) + offset + Global.globalOffset;
}
