// 컨트롤 오브젝트의 컨트롤 주체
public enum Player
{
    None,       // 컨트롤 X
    Player1,    // 1P 컨트롤 (wasd)
    Player2,    // 2P 컨트롤 (방향키)
    All,
}

// HP바의 종류
public enum HpUIType
{
    Counter,
    Slider,
}

// Navigatable 이 붙은 버튼의 종류
public enum NavType
{
    Button,
    ShopCard,
    FieldUnit,
}

// 상점 모드
public enum ShopState
{
    Deactivated,
    Select,
    Place,
    Upgrade,
}