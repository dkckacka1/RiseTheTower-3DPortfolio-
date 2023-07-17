using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

/*
 *  게임의 유저 데이터 클래스입니다.
 */

namespace RPG.Core
{
    public class UserInfo
    {
        public int itemReinforceTicket;     // 장비 강화권
        public int itemIncantTicket;        // 장비 인챈트권
        public int itemGachaTicket;         //// 장비 뽑기권
        public int risingTopCount;          //
        public int energy;

        // Weapon
        public int lastedWeaponID;          // 마지막으로 착용한 무기 ID
        public int weaponReinforceCount;    // 무기 강화 수치
        public int weaponPrefixIncantID;    // 무기 접두 인챈트 ID
        public int weaponSuffixIncantID;    // 무기 접미 인챈트 ID

        // Armor                               
        public int lastedArmorID;           // 마지막으로 착용한 갑옷 ID
        public int armorReinforceCount;     // 갑옷 강화 수치
        public int armorPrefixIncantID;     // 갑옷 접두 인챈트 ID
        public int armorSuffixIncantID;     // 갑옷 접미 인챈트 ID

        // Helmet                              
        public int lastedHelmetID;          // 마지막으로 착용한 헬멧 ID
        public int helmetReinforceCount;    // 헬멧 강화 수치
        public int helmetPrefixIncantID;    // 헬멧 접두 인챈트 ID
        public int helmetSuffixIncantID;    // 헬멧 접미 인챈트 ID

        // Pants                               
        public int lastedPantsID;           // 마지막으로 착용한 바지 ID
        public int pantsReinforceCount;     // 바지 강화 수치
        public int pantsPrefixIncantID;     // 바지 접두 인챈트 ID
        public int pantsSuffixIncantID;     // 바지 접미 인챈트 ID

        // 캐릭터 정보를 토대로 유저정보를 업데이트 합니다.
        public void UpdateUserinfoFromStatus(PlayerStatus status)
        {
            lastedWeaponID = status.currentWeapon.data.ID;
            weaponReinforceCount = status.currentWeapon.reinforceCount;
            weaponPrefixIncantID = status.currentWeapon.GetPrefixIncantID();
            weaponSuffixIncantID = status.currentWeapon.GetSuffixIncantID();

            lastedArmorID = status.currentArmor.data.ID;
            armorReinforceCount = status.currentArmor.reinforceCount;
            armorPrefixIncantID = status.currentArmor.GetPrefixIncantID();
            armorSuffixIncantID = status.currentArmor.GetSuffixIncantID();

            lastedHelmetID = status.currentHelmet.data.ID;
            helmetReinforceCount = status.currentHelmet.reinforceCount;
            helmetPrefixIncantID = status.currentHelmet.GetPrefixIncantID();
            helmetSuffixIncantID = status.currentHelmet.GetSuffixIncantID();
                
            lastedPantsID = status.currentPants.data.ID;
            pantsReinforceCount = status.currentPants.reinforceCount;
            pantsPrefixIncantID = status.currentPants.GetPrefixIncantID();
            pantsSuffixIncantID = status.currentPants.GetSuffixIncantID();
        }
    }
}