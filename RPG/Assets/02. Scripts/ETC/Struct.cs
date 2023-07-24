using UnityEngine;

/*
 * 게임 내 사용되는 구조체를 모아놓은 클래스
 */

// 적군 스폰 정보
[System.Serializable]
public struct EnemySpawnStruct
{
    public int enemyID; // 적 ID
}

// 적 드랍 테이블
[System.Serializable]
public struct DropTable
{
    public DropItemType itemType;   // 드랍 아이템 타입
    public int percent; // 드랍확률
}

// 전투 텍스트 정보
[System.Serializable]
public struct DamageTextMaterial
{
    public DamagedType type;    // 텍스트 타입
    public Material material;   // 마테리얼
}

// 루팅아이템
[System.Serializable]
public struct LootingImage
{
    public DropItemType type;   // 루팅 아이템 타입
    public Sprite sprite;       // 루팅 아이템 이미지
}