using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 바지에 붙는 인챈트 데이터
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/PantsData", order = 4)]
    public class PantsIncantData : IncantData
    {
        [Header("Attribute")]
        public int hpPoint;             // 체력 수치
        public int defencePoint;        // 방어력 수치
        public float movementSpeed;     // 이동속도
    }
}