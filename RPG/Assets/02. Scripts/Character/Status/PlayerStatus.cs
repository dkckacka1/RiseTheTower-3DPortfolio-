using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Core; 

namespace RPG.Character.Status
{
    public class PlayerStatus : Status
    {
        public Weapon currentWeapon;
        public Armor currentArmor;
        public Helmet currentHelmet;
        public Pants currentPants;

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

        public void SetPlayerStatusFromUserinfo(UserInfo userInfo)
        {
            // 1.장비 장착
            WeaponData w_data;
            ArmorData a_data;
            HelmetData h_data;
            PantsData p_data;
            GameManager.Instance.GetEquipmentData(userInfo.lastedWeaponID, out w_data);
            GameManager.Instance.GetEquipmentData(userInfo.lastedArmorID, out a_data);
            GameManager.Instance.GetEquipmentData(userInfo.lastedHelmetID, out h_data);
            GameManager.Instance.GetEquipmentData(userInfo.lastedPantsID, out p_data);

            // 1-1. 장비에 강화 수치 적용
            // 1-2. 장비에 인챈트 적용
            // 1-3. 장비 아이템 업데이트

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


            // 2.장비에 따른 스테이터스 변화해주기
            this.SetEquipment();
        }

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