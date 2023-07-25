using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;

/*
 * 장비아이템 클래스
 */

namespace RPG.Character.Equipment
{
    public abstract class Equipment
    {
        public int reinforceCount = 0;              // 장비의 강화 수치
        public string itemName;                     // 장비아이템 이름
        public EquipmentItemType equipmentType;     // 장비아이템 타입
        public TierType equipmentTier;              // 장비아이템 등급
        public string description;                  // 장비아이템 설명

        public EquipmentData data;                  // 장비아이템 데이터

        public Incant prefix;       // 접두 인챈트
        public Incant suffix;       // 접미 인챈트

        // 장비아이템으로 만든 생성자 로비에서 전투로 넘어갈때 캐릭터 정보를 통해 전투캐릭터로 만들때 사용
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

        // 장비아이템 데이터로 만든 생성자 유저데이터에서 로비캐릭터를 만들때 사용
        public Equipment(EquipmentData data)
        {
            this.data = data;
            itemName = data.EquipmentName;
            equipmentType = data.equipmentType;
            equipmentTier = data.equipmentTier;
            description = data.description;
        }

        // 장비아이템 데이터를 교체합니다.
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
        // 인챈트 ID로 인챈트를 찾아 장비아이템에 부여합니다.
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

        // 인챈트 클래스로 장비아이템에 인챈트합니다.
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

        // 모든 인챈트를 제거합니다.
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

        // 인챈트가 있는지 여부
        public bool isIncant()
        {
            return (prefix != null || suffix != null);
        }
        #endregion

        // 접두 인챈트ID를 찾습니다.
        public int GetPrefixIncantID()
        {
            if (prefix == null)
            {
                return -1;
            }

            return prefix.incantID;
        }

        // 접미 인챈트ID를 찾습니다.
        public int GetSuffixIncantID()
        {
            if (suffix == null)
            {
                return -1;
            }

            return suffix.incantID;
        }

        // 인챈트에 특수한 효과가 붙는지 여부
        public bool hasAbilitySkill()
        {
            return (hasPrefixAbilitySkill() || hasSuffixAbilitySkill());
        }

        // 접두 인챈트에 효과가 붙는지 여부
        public bool hasPrefixAbilitySkill()
        {
            if (prefix == null) return false;
            if (!prefix.isIncantAbility) return false;

            return true;
        }

        // 접미 인챈트에 효과가 붙는지 여부
        public bool hasSuffixAbilitySkill()
        {
            if (suffix == null) return false;
            if (!suffix.isIncantAbility) return false;

            return true;
        }

        // 강화가 되어있는지 여부
        public bool isReinforce()
        {
            return !(reinforceCount == 0);
        }

        // 장비를 강화합니다.
        public void ReinforceItem()
        {
            reinforceCount++;
        }

        // 강화 수치를 제거합니다.
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

        // 장비 등급 문자열을 리턴합니다.
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

        // 장비 타입 문자열을 리턴합니다.
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