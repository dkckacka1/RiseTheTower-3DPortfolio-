using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 갑옷에 붙는 인챈트 데이터
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/ArmorData", order = 2)]
    public class ArmorIncantData : IncantData
    {
        [Header("Attribute")]
        public int hpPoint;         // 체력 포인트
        public int defencePoint;    // 방어 포인트
        public float movementSpeed; // 이동속도
        public float evasionPoint;  // 회피율
    }
}