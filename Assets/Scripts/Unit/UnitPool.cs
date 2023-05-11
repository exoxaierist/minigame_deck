using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPool : MonoBehaviour
{
    //���� ������ ��� ����
    //Ŭ������ ��ȯ�Ѵٸ� ���ڸ� ȭ�鿡 �׸���
    //��ȯ �Լ��� ���ʿ� �ڵ带 ������
    //������ ������ ������ Ȥ�� 3X3
    public static UnitPool Instance;
    [SerializeField]
    public List<UnitBase> P1Unit;
    [SerializeField]
    public List<UnitBase> P2Unit;

    // ������ �Ʊ� ���ֵ� ��ȯ(int �ε���)
    // ������ ���� ���ֵ� ��ȯ(int �ε���)
    // ���� ����� �Ʊ� ���� ��ȯ(int �ε���)

    private void singletoneCheck()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Awake()
    {
        singletoneCheck();
    }
}
