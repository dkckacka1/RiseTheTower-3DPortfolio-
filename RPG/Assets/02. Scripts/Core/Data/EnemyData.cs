using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "CreateScriptableObject/CreateEnemy", order = 6)]
public class EnemyData : Data
{
    public int apperenceNum;
    public string enemyName;

    [Header("Health")]
    public int maxHp = 0;

    [Header("Attack")]
    public int attackDamage = 0;
    public float attackRange = 0f;
    public float attackSpeed = 0f;
    public float criticalChance = 0f;
    public float criticalDamage = 0f;
    public float attackChance = 0f;

    [Header("Defence")]
    public int defencePoint = 0;
    public float evasionPoint = 0f;
    public float decreseCriticalDamage = 0f;
    public float evasionCritical = 0f;

    [Header("Movement")]
    public float movementSpeed = 0f;

    [Header("DropItem")]
    public int dropEnergy;
    public List<DropTable> dropitems;

    [Header("Equipment")]
    public int weaponApparenceID;
    public weaponHandleType handleType;
}
