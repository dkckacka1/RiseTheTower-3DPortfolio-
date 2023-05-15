using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewPants", menuName = "CreateScriptableObject/CreatePants", order = 4)]
    public class PantsData : EquipmentData
    {
        public int defencePoint;
        public int hpPoint;
        [Range(0f, 0.5f)] public float movementSpeed;
    }
}
