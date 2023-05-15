using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class EquipmentData : Data
    {
        public string EquipmentName;
        public EquipmentItemType equipmentType;
        public TierType equipmentTier;
        public Sprite equipmentSprite;
        [Space()]
        [TextArea()]
        public string description;
    }

}