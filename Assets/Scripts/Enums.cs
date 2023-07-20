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
    Number,
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
    Closed,
    Select,
    Place,
    Upgrade,
}

// 게임 모드
public enum GameState
{
    Pause, // 일시정지?
    Fight, // 대전모드
    Shop, // 상점모드
    Result, // 결과창
}

public enum AllyOrEnemy
{
    Ally,
    Enemy,
}