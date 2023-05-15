using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewWeapon", menuName = "CreateScriptableObject/CreateWeapon", order = 1)]
    public class WeaponData : EquipmentData
    {
        public int weaponApparenceID;

        public int attackDamage;
        [Range(1, 2.5f)] public float attackSpeed;
        [Range(1, 5f)] public float attackRange;
        [Range(1, 5f)] public float movementSpeed;
        [Range(0, 1f)] public float criticalChance;
        [Range(0, 1f)] public float criticalDamage;
        [Range(0.6f, 1.2f)] public float attackChance;

        public weaponHandleType weaponHandleType = weaponHandleType.OneHandedWeapon; 
    }
}
