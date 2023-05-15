using UnityEngine;

[System.Serializable]
public struct EnemySpawnStruct
{
    public int enemyID;
}

[System.Serializable]
public struct DropTable
{
    public DropItemType itemType;
    public int percent;
}

[System.Serializable]
public struct DamageTextMaterial
{
    public DamagedType type;
    public Material material;
}

[System.Serializable]
public struct LootingImage
{
    public DropItemType type;
    public Sprite sprite;
}