using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBombMamamam : UnitBase
{
    protected override void OnMove()
    {
        //�����ϴ� �Լ��� ���� ��������� �ʾ� �ּ�ó��
        //Attack(Global.fieldManager.GetRandomTile());
    }
    protected override void Attack()
    {
        for(int xPos = (int)transform.position.x - 1; xPos <= (int)transform.position.x + 1; xPos++)
        {
            for(int yPos = (int)transform.position.y - 1; yPos <= (int)transform.position.y + 1; yPos++)
            {
                //�����ϴ� �Լ�
                //����(new Vector2(xPos,yPos))
            }
        }
    }
    protected override void Awake()
    {
        base.Awake();
    }
}
