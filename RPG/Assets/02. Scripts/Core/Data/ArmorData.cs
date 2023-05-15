using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewArmor", menuName = "CreateScriptableObject/CreateArmor", order = 2)]
    public class ArmorData : EquipmentData
    {
        public int defencePoint;
        public int hpPoint;
        [Range(0f, 0.5f)] public float movementSpeed;
        [Range(0f, 0.2f)] public float evasionPoint;
    }
}