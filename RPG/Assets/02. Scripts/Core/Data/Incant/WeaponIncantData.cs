using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 무기에 붙는 인챈트 데이터
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/WeaponData", order = 1)]
    public class WeaponIncantData : IncantData
    {
        [Header("Attribute")]
        public int attackDamage;        // 공격력 수치
        public float attackSpeed;       // 공격 속도
        public float attackRange;       // 공격 범위
        public float movementSpeed;     // 이동 속도
        public float criticalChance;    // 치명타 확률
        public float criticalDamage;    // 치명타 피해율
        public float attackChance;      // 명중률
    }
}