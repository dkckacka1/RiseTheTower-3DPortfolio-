using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 적 데이터 클래스
 */

[CreateAssetMenu(fileName = "NewEnemy", menuName = "CreateScriptableObject/CreateEnemy", order = 6)]
public class EnemyData : Data
{
    public int apperenceNum;            // 적의 외형 ID
    public string enemyName;            // 적 이름

    [Header("Health")]
    public int maxHp = 0;   // 최대 체력 수치

    [Header("Attack")]
    public int attackDamage = 0;        // 공격력
    public float attackRange = 0f;      // 공격범위
    public float attackSpeed = 0f;      // 공격속도
    public float criticalChance = 0f;   // 치명타 적중률
    public float criticalDamage = 0f;   // 치명타 피해율
    public float attackChance = 0f;     // 명중률

    [Header("Defence")]
    public int defencePoint = 0;                // 방어도
    public float evasionPoint = 0f;             // 회피율
    public float decreseCriticalDamage = 0f;    // 치명타 피해 감소율
    public float evasionCritical = 0f;          // 회피율

    [Header("Movement")]
    public float movementSpeed = 0f;    // 이동속도

    [Header("DropItem")]
    public int dropEnergy;              // 에너지 드랍양
    public List<DropTable> dropitems;   // 드랍 테이블

    [Header("Equipment")]
    public int weaponApparenceID;       // 무기 외형
    public weaponHandleType handleType; // 한손 및 양손 외형
}
