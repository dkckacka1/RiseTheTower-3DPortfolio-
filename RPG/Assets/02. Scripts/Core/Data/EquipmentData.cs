using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 장비아이템 데이터 
 */

namespace RPG.Character.Equipment
{
    public class EquipmentData : Data
    {
        public string EquipmentName;            // 장비 이름
        public EquipmentItemType equipmentType; // 장비 타입
        public TierType equipmentTier;          // 장비 등급
        public Sprite equipmentSprite;          // 장비 이미지
        [Space()]
        [TextArea()]
        public string description;              // 장비 설명
    }

}