// ��Ʈ�� ������Ʈ�� ��Ʈ�� ��ü
public enum Player
{
    None,       // ��Ʈ�� X
    Player1,    // 1P ��Ʈ�� (wasd)
    Player2,    // 2P ��Ʈ�� (����Ű)
    All,
}

// HP���� ����
public enum HpUIType
{
    Counter,
    Slider,
}

// Navigatable �� ���� ��ư�� ����
public enum NavType
{
    Button,
    ShopCard,
    FieldUnit,
}

// ���� ���
public enum ShopState
{
    Deactivated,
    Select,
    Place,
    Upgrade,
}