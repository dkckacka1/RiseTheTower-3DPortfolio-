using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 무기 아이템 데이터 클래스
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "CreateScriptableObject/CreateWeapon", order = 1)]
    public class WeaponData : EquipmentData
    {
        public int weaponApparenceID;   // 무기 외형 ID

        public int attackDamage;                            // 공격력 수치
        [Range(1, 2.5f)] public float attackSpeed;          // 공격 속도
        [Range(1, 5f)] public float attackRange;            // 공격 범위
        [Range(1, 5f)] public float movementSpeed;          // 이동 속도
        [Range(0, 1f)] public float criticalChance;         // 치명타 확률
        [Range(0, 1f)] public float criticalDamage;         // 치명타 피해율
        [Range(0.6f, 1.2f)] public float attackChance;      // 명중률

        public weaponHandleType weaponHandleType = weaponHandleType.OneHandedWeapon; // 무기 타입 (한손검인지 양손검인지)
    }
}
