using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 투구에 붙은 인챈트 데이터
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/HelmetData", order = 3)]
    public class HelmetIncantData : IncantData
    {
        [Header("Attribute")]
        public int hpPoint;                 // 체력 수치
        public int defencePoint;            // 방어 수치
        public float decreseCriticalDamage; // 치명타 데미지 감소율
        public float evasionCritical;       // 치명타 회피율
    }
}