#region 전투 관련

// UI에 의해 보여지는 State
public enum BattleSceneState
{
    Default,        // 기본
    Ready,          // 준비
    Battle,         // 전투
    Pause,          // 멈춤
    Defeat,         // 패배
    Win,            // 승리
    Ending          // 엔딩
}

// 컨트롤러의 현재 상태
public enum CombatState
{
    Default,        // 기본
    Actable,        // 행동 가능 상태
    Actunable,      // 행동 불가 상태 (공포 유혹 등)
    Dead,           // 사망
}

public enum AIState
{
    Default,    // 기본
    Attack,     // 공격
    Chase,      // 추적
    Dead,       // 사망
    Debuff,     // 디버프
    Idle        // 유휴
}

// 행동 트리 상태(미사용)
public enum Stats
{
    UPDATE, // 업데이트
    FAILURE,// 실패
    SUCCESS // 성공
}

// 데미지 유형
public enum DamagedType
{
    Normal,     // 기본 공격
    Ciritical,  // 치명타 공격
    MISS        // 공격 빗나감
}

// 상태이상
public enum DebuffType
{
    Defualt,        // 기본
    Stern,          // 기절
    Bloody,         // 출혈
    Paralysis,      // 마비
    Temptation,     // 유혹
    Fear,           // 공포
    Curse           // 저주

}
#endregion

#region 아이템 관련
// 장비아이템 타입
public enum EquipmentItemType
{
    Weapon = 0,     // 무기
    Armor,          // 갑옷
    Pants,          // 바지
    Helmet,         // 헬멧
}

// 장비아이템 등급
public enum TierType
{
    Normal = 0,     // 평범
    Rare,           // 희귀
    Unique,         // 고유
    Legendary       // 전설
}

// 인챈트 종류
public enum IncantType
{ 
    prefix,     // 접두
    suffix      // 접미
}

// 드랍아이템 종류
public enum DropItemType
{
    Energy,             // 에너지
    GachaItemScroll,    // 뽑기 티켓
    reinfoceScroll,     // 강화 티켓
    IncantScroll        // 인챈트 티켓
}

// 무기유형
public enum weaponHandleType
{
    OneHandedWeapon,            // 한손 무기
    TwoHandedWeapon             // 양손 무기
}
#endregion

#region 기타

// 텍스트 정렬 타입
public enum alignmentType
{
    left,           // 죄측정렬
    right,          // 우측정렬
    center,         // 가운데 정렬
}
#endregion