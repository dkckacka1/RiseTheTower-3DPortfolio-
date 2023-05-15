using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewHelmet", menuName = "CreateScriptableObject/CreateHelmet", order = 3)]
    public class HelmetData : EquipmentData
    {
        public int defencePoint;
        public int hpPoint;
        [Range(0f, 0.2f)] public float decreseCriticalDamage;
        [Range(0f, 0.2f)] public float evasionCritical;
    }
}
