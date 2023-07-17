using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : 주석은 여기서부터

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/ArmorData", order = 2)]
    public class ArmorIncantData : IncantData
    {
        [Header("Attribute")]
        public int hpPoint;
        public int defencePoint;
        public float movementSpeed;
        public float evasionPoint;
    }
}