using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/WeaponData", order = 1)]
    public class WeaponIncantData : IncantData
    {
        [Header("Attribute")]
        public int attackDamage;
        public float attackSpeed;
        public float attackRange;
        public float movementSpeed;
        public float criticalChance;
        public float criticalDamage;
        public float attackChance;
    }
}