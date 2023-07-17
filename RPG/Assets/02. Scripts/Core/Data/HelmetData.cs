using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 헬멧 아이템 데이터 클래스
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewHelmet", menuName = "CreateScriptableObject/CreateHelmet", order = 3)]
    public class HelmetData : EquipmentData
    {
        public int defencePoint;                                // 방어도 수치
        public int hpPoint;                                     // 체력 수치
        [Range(0f, 0.2f)] public float decreseCriticalDamage;   // 치명타 데이미 감소 
        [Range(0f, 0.2f)] public float evasionCritical;         // 치명타 회피율
    }
}
