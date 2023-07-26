using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Core; 

/*
 * 플레이어 스탯 클래스
 */

namespace RPG.Character.Status
{
    public class PlayerStatus : Status
    {
        public Weapon currentWeapon;    // 현재 장착한 무기
        public Armor currentArmor;      // 현재 장착한 갑옷
        public Helmet currentHelmet;    // 현재 장착한 헬멧
        public Pants currentPants;      // 현재 장착한 바지

        // 현재 장비의 수치로 스탯을 세팅합니다.
        public void SetEquipment()
        {
            if (currentWeapon == null ||
                currentArmor == null ||
                currentHelmet == null ||
                currentPants == null)
            {
                Debug.LogError("장비 아이템이 없습니다.");    
                return;
            }

            AttackDamage =          currentWeapon.AttackDamage;
            AttackRange =           currentWeapon.AttackRange;
            AttackSpeed =           currentWeapon.AttackSpeed;
            CriticalChance =        currentWeapon.CriticalChance;
            CriticalDamage =        currentWeapon.CriticalDamage;
            AttackChance =          currentWeapon.AttackChance;

            MaxHp =                 currentArmor.HpPoint + currentHelmet.HpPoint + currentPants.HpPoint;
            DefencePoint =          currentArmor.DefencePoint + currentHelmet.DefencePoint + currentPants.DefencePoint;
            EvasionPoint =          currentArmor.EvasionPoint;
            DecreseCriticalDamage = currentHelmet.DecreseCriticalDamage;
            EvasionCritical =       currentHelmet.EvasionCritical;

            MovementSpeed =         currentWeapon.MovementSpeed + currentPants.MovementSpeed;
        }

        // 유저 정보를 통해서 스탯을 세팅합니다.
        public void SetPlayerStatusFromUserinfo(UserInfo userInfo)
        {
            // 유저정보에서 각 장비 ID를 가져옵니다.
            WeaponData w_data;
            ArmorData a_data;
            HelmetData h_data;
            PantsData p_data;
            GameManager.Instance.GetEquipmentData(userInfo.lastedWeaponID, out w_data);
            GameManager.Instance.GetEquipmentData(userInfo.lastedArmorID, out a_data);
            GameManager.Instance.GetEquipmentData(userInfo.lastedHelmetID, out h_data);
            GameManager.Instance.GetEquipmentData(userInfo.lastedPantsID, out p_data);


            // 각 장비에 강화 수치를 적용하고
            // 인챈트를 적용시킵니다.
            if (w_data)
            {
                Weapon weapon = new Weapon(w_data);
                weapon.reinforceCount = userInfo.weaponReinforceCount;

                if (userInfo.weaponPrefixIncantID != -1)
                {
                    Incant prefixIncant = GameManager.Instance.incantDic[userInfo.weaponPrefixIncantID];
                    weapon.Incant(prefixIncant);
                }

                if (userInfo.weaponSuffixIncantID != -1)
                {
                    Incant suffixIncant = GameManager.Instance.incantDic[userInfo.weaponSuffixIncantID];
                    weapon.Incant(suffixIncant);
                }

                this.EquipItem(weapon);
            }
            else
                Debug.LogError("Weapon is null");

            if (a_data)
            {
                Armor armor = new Armor(a_data);
                armor.reinforceCount = userInfo.armorReinforceCount;

                if (userInfo.armorPrefixIncantID != -1)
                {
                    Incant prefixIncant = GameManager.Instance.incantDic[userInfo.armorPrefixIncantID];
                    armor.Incant(prefixIncant);
                }

                if (userInfo.armorSuffixIncantID != -1)
                {
                    Incant suffixIncant = GameManager.Instance.incantDic[userInfo.armorSuffixIncantID];
                    armor.Incant(suffixIncant);
                }

                this.EquipItem(armor);
            }
            else
                Debug.LogError("Armor is null");


            if (h_data)
            {
                Helmet helmet = new Helmet(h_data);
                helmet.reinforceCount = userInfo.helmetReinforceCount;
                if (userInfo.helmetPrefixIncantID != -1)
                {
                    Incant prefixIncant = GameManager.Instance.incantDic[userInfo.helmetPrefixIncantID];
                    helmet.Incant(prefixIncant);
                }

                if (userInfo.helmetSuffixIncantID != -1)
                {
                    Incant suffixIncant = GameManager.Instance.incantDic[userInfo.helmetSuffixIncantID];
                    helmet.Incant(suffixIncant);
                }

                this.EquipItem(helmet);
            }
            else
                Debug.LogError("Helmet is null");


            if (p_data)
            {
                Pants pants = new Pants(p_data);
                pants.reinforceCount = userInfo.pantsReinforceCount;

                if (userInfo.pantsPrefixIncantID != -1)
                {
                    Incant prefixIncant = GameManager.Instance.incantDic[userInfo.pantsPrefixIncantID];
                    pants.Incant(prefixIncant);
                }

                if (userInfo.pantsSuffixIncantID != -1)
                {
                    Incant suffixIncant = GameManager.Instance.incantDic[userInfo.pantsSuffixIncantID];
                    pants.Incant(suffixIncant);
                }
                this.EquipItem(pants);
            }
            else
                Debug.LogError("Pants is null");


            // 각 장비를 세팅합니다.
            this.SetEquipment();
        }

        // 로비에서의 플레이어 스탯 정보로 전투 스텟을 세팅할 때 사용합니다.
        public void SetPlayerStatusFromStatus(PlayerStatus status, CharacterAppearance ap = null)
        {
            currentWeapon = new Weapon(status.currentWeapon);
            currentArmor = new Armor(status.currentArmor);
            currentHelmet = new Helmet(status.currentHelmet);
            currentPants = new Pants(status.currentPants);


            if (ap != null)
            {
                ap. EquipWeapon(currentWeapon.weaponApparenceID, currentWeapon.handleType);
            }

            SetPlayerDefaultStatus(status);
        }

        // 각 스탯 수치를 갱신합니다.
        public void SetPlayerDefaultStatus(PlayerStatus status)
        {
            MaxHp = status.MaxHp;

            AttackDamage = status.AttackDamage;
            AttackRange = status.AttackRange;
            AttackSpeed = status.AttackSpeed;
            CriticalChance = status.CriticalChance;
            CriticalDamage = status.CriticalDamage;
            AttackChance = status.AttackChance;

            DefencePoint = status.DefencePoint;
            EvasionPoint = status.EvasionPoint;
            DecreseCriticalDamage = status.DecreseCriticalDamage;
            EvasionCritical = status.EvasionCritical;

            MovementSpeed = status.MovementSpeed;
        }

        // 인챈트에 따로 효과가 있는지 확인 합니다.
        public bool hasAbility()
        {
            //Debug.Log($"" +
            //    $"currentWeapon.hasAbilitySkill() : {currentWeapon.hasAbilitySkill()}\n" + 
            //    $"currentArmor.hasAbilitySkill() : {currentArmor.hasAbilitySkill()}\n" + 
            //    $"currentHelmet.hasAbilitySkill() : {currentHelmet.hasAbilitySkill()}\n" + 
            //    $"currentPants.hasAbilitySkill() : {currentPants.hasAbilitySkill()}");
            return currentWeapon.hasAbilitySkill() || currentArmor.hasAbilitySkill() || currentHelmet.hasAbilitySkill() || currentPants.hasAbilitySkill();
        }

        #region 장비_장착
        // 각 장비를 변경합니다.
        public void EquipItem(Weapon weapon)
        {
            currentWeapon = weapon;
        }

        public void EquipItem(Armor armor)
        {
            currentArmor = armor;
        }

        public void EquipItem(Helmet helmet)
        {
            currentHelmet = helmet;
        }

        public void EquipItem(Pants pants)
        {
            currentPants = pants;
        }
        #endregion
    }
}