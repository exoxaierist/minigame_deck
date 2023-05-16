using UnityEngine;

// ��ũ��Ʈ���� ���� ���۷��� �� �� �ְ� ���ִ� ������?, �ڿ� ����� �߰�
[CreateAssetMenu(fileName ="AssetCollector")]
public class AssetCollector : ScriptableObject
{
    [Header("HP�� UI")]
    public GameObject hpUI;
    public GameObject hpCounterUI;
    public Sprite hpCounterSpriteFull;
    public Sprite hpCounterSpriteEmpty;

    [Header("UI ������")]
    public GameObject p1Selector;
    public GameObject p2Selector;

    [Header("���� ����")]
    public GameObject p1ShopCard;
    public GameObject p2ShopCard;
    public GameObject shopFieldUnitUI;
}
