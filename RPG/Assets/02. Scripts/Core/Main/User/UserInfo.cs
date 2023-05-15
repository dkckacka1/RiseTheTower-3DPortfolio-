using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

namespace RPG.Core
{
    public class UserInfo
    {
        public int itemReinforceTicket; // 장비 강화권
        public int itemIncantTicket; // 장비 인챈트권
        public int itemGachaTicket; // 장비 뽑기권
        public int risingTopCount;
        public int energy;

        // Weapon
        public int lastedWeaponID;
        public int weaponReinforceCount;
        public int weaponPrefixIncantID;
        public int weaponSuffixIncantID;
        // Armor
        public int lastedArmorID;
        public int armorReinforceCount;
        public int armorPrefixIncantID;
        public int armorSuffixIncantID;
        // Helmet
        public int lastedHelmetID;
        public int helmetReinforceCount;
        public int helmetPrefixIncantID;
        public int helmetSuffixIncantID;

        // Pants
        public int lastedPantsID;
        public int pantsReinforceCount;
        public int pantsPrefixIncantID;
        public int pantsSuffixIncantID;

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

        public override string ToString()
        {
            string str =
                $"itemReinforceCount : {this.itemReinforceTicket}\n" +
                $"itemIncantCount : {this.itemIncantTicket}\n" +
                $"itemGachaTicket : {this.itemGachaTicket}\n" +
                $"risingTopCount : {this.risingTopCount}\n" +
                $"Energy : {this.energy}\n" +
                $"lastedWeapon : {this.lastedWeaponID}\n" +
                $"weaponReinforceCount : {this.weaponReinforceCount}\n" +
                $"weaponPreifxIncantID : {this.weaponPrefixIncantID}\n" +
                $"weaponSuffixIncantID : {this.weaponSuffixIncantID}\n" +
                $"lastedArmor : {this.lastedArmorID}\n" +
                $"armorReinforceCount : {this.armorReinforceCount}\n" +
                $"armorPrefixIncantID : {this.armorPrefixIncantID}\n" +
                $"armorSuffixIncantID : {this.armorSuffixIncantID}\n" +
                $"lastedHelmet : {this.lastedHelmetID}\n" +
                $"helmetReinforceCount : {this.helmetReinforceCount}\n" +
                $"helmetPrefixIncantID : {this.helmetPrefixIncantID}\n" +
                $"helmetSuffixIncantID : {this.helmetSuffixIncantID}\n" +
                $"lastedPants : {this.lastedPantsID}\n" +
                $"pantsReinforceCount : {this.pantsReinforceCount}\n" +
                $"pantsPrefixIncantID : {this.pantsPrefixIncantID}\n" +
                $"pantsSuffixIncantID : {this.pantsSuffixIncantID}\n";

            return str;
        }
    }
}