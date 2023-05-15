using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/HelmetData", order = 3)]
    public class HelmetIncantData : IncantData
    {
        [Header("Attribute")]
        public int hpPoint;
        public int defencePoint;
        public float decreseCriticalDamage;
        public float evasionCritical;
    }
}