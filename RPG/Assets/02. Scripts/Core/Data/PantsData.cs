using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 바지 아이템 데이터 클래스
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewPants", menuName = "CreateScriptableObject/CreatePants", order = 4)]
    public class PantsData : EquipmentData
    {
        public int defencePoint;                        // 방어도 수치
        public int hpPoint;                             // 체력 수치
        [Range(0f, 0.5f)] public float movementSpeed;   // 이동속도 수치
    }
}
