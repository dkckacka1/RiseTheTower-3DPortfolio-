using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

namespace RPG.Character.Equipment
{
    public abstract class Equipment
    {
        public int reinforceCount = 0;
        public string itemName;
        public EquipmentItemType equipmentType;
        public TierType equipmentTier;
        public string description;

        public EquipmentData data;

        public Incant prefix;
        public Incant suffix;

        public Equipment(Equipment equipment)
        {
            data = equipment.data;
            itemName = equipment.itemName;
            equipmentType = equipment.equipmentType;
            equipmentTier = equipment.equipmentTier;
            description = equipment.description;

            reinforceCount = equipment.reinforceCount;
            prefix = equipment.prefix;
            suffix = equipment.suffix;
        }

        public Equipment(EquipmentData data)
        {
            this.data = data;
            itemName = data.EquipmentName;
            equipmentType = data.equipmentType;
            equipmentTier = data.equipmentTier;
            description = data.description;
        }

        public virtual void ChangeData(EquipmentData data)
        {
            RemoveAllIncant();
            reinforceCount = 0;

            this.data = data;
            itemName = data.EquipmentName;
            equipmentType = data.equipmentType;
            equipmentTier = data.equipmentTier;
            description = data.description;
        }

        #region Incant

        public void Incant(int incantID)
        {
            Incant incant = GameManager.Instance.incantDic[incantID];

            if (incant == null)
            {
                Debug.Log("잘못된 인챈트 호출");
                return;
            }

            // 인챈트 타입과 아이템 타입이 맞는지 확인
            if (this.equipmentType != incant.itemType)
            {
                Debug.Log("장비 타입과 인챈트 장비 타입이 다릅니다.");
                return;
            }

            switch (incant.incantType)
            {
                case IncantType.prefix:
                    prefix = incant;
                    break;
                case IncantType.suffix:
                    suffix = incant;
                    break;
            }
        }

        public void Incant(Incant incant)
        {
            if (incant == null)
            {
                Debug.Log("잘못된 인챈트 호출");
                return;
            }

            // 인챈트 타입과 아이템 타입이 맞는지 확인
            if (this.equipmentType != incant.itemType)
            {
                Debug.Log("장비 타입과 인챈트 장비 타입이 다릅니다.");
                return;
            }

            switch (incant.incantType)
            {
                case IncantType.prefix:
                    prefix = incant;
                    break;
                case IncantType.suffix:
                    suffix = incant;
                    break;
            }
        }

        public void RemoveAllIncant()
        {
            if (prefix != null)
            {
                prefix = null;
            }

            if (suffix != null)
            {
                suffix = null;
            }
        }

        public bool isIncant()
        {
            return (prefix != null || suffix != null);
        }
        #endregion

        public int GetPrefixIncantID()
        {
            if (prefix == null)
            {
                return -1;
            }

            return prefix.incantID;
        }

        public int GetSuffixIncantID()
        {
            if (suffix == null)
            {
                return -1;
            }

            return suffix.incantID;
        }

        public bool hasAbilitySkill()
        {
            return (hasPrefixAbilitySkill() || hasSuffixAbilitySkill());
        }

        public bool hasPrefixAbilitySkill()
        {
            if (prefix == null) return false;
            if (!prefix.isIncantAbility) return false;

            return true;
        }

        public bool hasSuffixAbilitySkill()
        {
            if (suffix == null) return false;
            if (!suffix.isIncantAbility) return false;

            return true;
        }

        public bool isReinforce()
        {
            return !(reinforceCount == 0);
        }

        public void ReinforceItem()
        {
            reinforceCount++;
        }

        public void RemoveReinforce()
        {
            reinforceCount = 0;
        }

        public override string ToString()
        {
            return
                $"장비이름 : {itemName}\n" +
                $"장비티어 : {equipmentTier}\n" +
                $"장비유형 : {equipmentType}\n" +
                $"접두인챈트 : {(prefix != null ? prefix.incantName : "없음")}\n" +
                $"접미인챈트 : {(suffix != null ? suffix.incantName : "없음")}";
        }

        public string ToStringTier()
        {
            switch (equipmentTier)
            {
                case TierType.Normal:
                    return "노말";
                case TierType.Rare:
                    return "레어";
                case TierType.Unique:
                    return "유니크";
                case TierType.Legendary:
                    return "전설";
            }

            return "";
        }

        public string ToStringEquipmentType()
        {
            switch (equipmentType)
            {
                case EquipmentItemType.Weapon:
                    return "무기";
                case EquipmentItemType.Armor:
                    return "갑옷";
                case EquipmentItemType.Pants:
                    return "바지";
                case EquipmentItemType.Helmet:
                    return "투구";
            }

            return "";
        }
    }

}