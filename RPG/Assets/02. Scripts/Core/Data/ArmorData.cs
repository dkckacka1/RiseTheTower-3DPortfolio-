using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 갑옷 아이템 데이터 클래스
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewArmor", menuName = "CreateScriptableObject/CreateArmor", order = 2)]
    public class ArmorData : EquipmentData
    {
        public int defencePoint;                        // 방어도 수치
        public int hpPoint;                             // 체력 수치
        [Range(0f, 0.5f)] public float movementSpeed;   // 이동속도
        [Range(0f, 0.2f)] public float evasionPoint;    // 회피 수치
    }
}