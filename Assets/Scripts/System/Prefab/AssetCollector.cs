using UnityEngine;

// 스크립트에서 에셋 레퍼런싱 할 수 있게 해주는 모음집?, 뒤에 맘대로 추가
[CreateAssetMenu(fileName ="AssetCollector")]
public class AssetCollector : ScriptableObject
{
    [Header("HP바 UI")]
    public GameObject hpUI;
    public GameObject hpCounterUI;
    public Sprite hpCounterSpriteFull;
    public Sprite hpCounterSpriteEmpty;
}
