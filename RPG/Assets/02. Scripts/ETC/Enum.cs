#region 전투 관련

// UI에 의해 보여지는 State
public enum BattleSceneState
{
    Default,
    Ready,
    Battle,
    Pause,
    Defeat,
    Win,
    Ending
}

// 컨트롤러의 현재 상태
public enum CombatState
{
    Default,
    Actable,
    Actunable,
    Dead,
}

public enum AIState
{
    Default,
    Attack,
    Chase,
    Dead,
    Debuff,
    Idle
}

// 행동 트리 상태(미사용)
public enum Stats
{
    UPDATE,
    FAILURE,
    SUCCESS
}

// 데미지 유형
public enum DamagedType
{
    Normal,
    Ciritical,
    MISS
}

// 상태이상
public enum DebuffType
{
    Defualt,
    Stern,
    Bloody,
    Paralysis,
    Temptation,
    Fear,
    Curse

}
#endregion

#region 아이템 관련
// 장비아이템 타입
public enum EquipmentItemType
{
    Weapon = 0,
    Armor,
    Pants,
    Helmet,
}

// 장비아이템 등급
public enum TierType
{
    Normal = 0,
    Rare,
    Unique,
    Legendary
}

public enum IncantType
{ 
    prefix,
    suffix
}
public enum DropItemType
{
    Energy,
    GachaItemScroll,
    reinfoceScroll,
    IncantScroll
}

public enum weaponHandleType
{
    OneHandedWeapon,
    TwoHandedWeapon
}
#endregion

#region 기타

public enum alignmentType
{
    left,
    right,
    center,
    justified,
    flush
}


#endregion