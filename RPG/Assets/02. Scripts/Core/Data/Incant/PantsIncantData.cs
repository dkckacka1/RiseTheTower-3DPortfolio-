using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/PantsData", order = 4)]

    public class PantsIncantData : IncantData
    {
        [Header("Attribute")]
        public int hpPoint;
        public int defencePoint;
        public float movementSpeed;
    }
}