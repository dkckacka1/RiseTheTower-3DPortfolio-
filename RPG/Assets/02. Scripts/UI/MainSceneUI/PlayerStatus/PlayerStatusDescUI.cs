using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using RPG.Character.Status;
using RPG.Character.Equipment;

namespace RPG.Main.UI.StatusUI
{
    public class PlayerStatusDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI attackStatusText;
        [SerializeField] TextMeshProUGUI defenceStatusText;

        [Header("Ability")]
        [SerializeField] GameObject abilityPropertyObject;
        [SerializeField] AbilityDescUI weaponPrefixAbilityDescUI;
        [SerializeField] AbilityDescUI weaponSuffixAbilityDescUI;
        [SerializeField] AbilityDescUI armorPrefixAbilityDescUI;
        [SerializeField] AbilityDescUI armorSuffixAbilityDescUI;
        [SerializeField] AbilityDescUI helmetPrefixAbilityDescUI;
        [SerializeField] AbilityDescUI helmetSuffixAbilityDescUI;
        [SerializeField] AbilityDescUI pantsPrefixAbilityDescUI;
        [SerializeField] AbilityDescUI pantsSuffixAbilityDescUI;

        private void OnEnable()
        {
            ShowPlayerStatus();
        }

        public void ShowPlayerStatus()
        {
            ShowAttackStatus(GameManager.Instance.Player);
            ShowDefenceStatus(GameManager.Instance.Player);
            ShowPlayerAbility(GameManager.Instance.Player);
            Debug.Log(abilityPropertyObject.transform.parent.name);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)abilityPropertyObject.transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)abilityPropertyObject.transform.parent.transform);
        }

        private void ShowAttackStatus(PlayerStatus status)
        {
            attackStatusText.text = $"" +
                $"{MyUtility.returnSideText("공격력 : ", $"{status.AttackDamage}")}\n" +
                $"{MyUtility.returnSideText("공격범위 : ", $"{status.AttackRange}")}\n" +
                $"{MyUtility.returnSideText("공격속도 : ", $"초당 {status.AttackSpeed}회 타격")}\n" +
                $"{MyUtility.returnSideText("치명타 확률 : ", $"{status.CriticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 공격력 : ", $"공격력의 {status.CriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("적중률 : ", $"{status.AttackChance * 100}%")}";
        }

        private void ShowDefenceStatus(PlayerStatus status)
        {
            defenceStatusText.text = $"" +
                $"{MyUtility.returnSideText("체력 : ", $"{status.MaxHp}")}\n" +
                $"{MyUtility.returnSideText("방어력 : ", $"{status.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("회피율 : ", $"{status.EvasionPoint * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 회피율 : ", $"{status.EvasionCritical * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 감소율 : ", $"치명타 공격력의 {status.DecreseCriticalDamage * 100}% 감소")}\n" +
                $"{MyUtility.returnSideText("이동속도 : ", $"{status.MovementSpeed}")}";
        }

        private void ShowPlayerAbility(PlayerStatus status)
        {
            if (!status.hasAbility())
            {
                abilityPropertyObject.SetActive(false);
                return;
            }
            
            Weapon weapon = status.currentWeapon;
            if (weapon.hasPrefixAbilitySkill())
            {
                weaponPrefixAbilityDescUI.gameObject.SetActive(true);
                weaponPrefixAbilityDescUI.ShowAbility(weapon.prefix);
            }
            else
            {
                weaponPrefixAbilityDescUI.gameObject.SetActive(false);
            }

            if (weapon.hasSuffixAbilitySkill())
            {
                weaponSuffixAbilityDescUI.gameObject.SetActive(true);
                weaponSuffixAbilityDescUI.ShowAbility(weapon.suffix);
            }
            else
            {
                weaponSuffixAbilityDescUI.gameObject.SetActive(false);
            }

            Armor armor = status.currentArmor;
            if (armor.hasPrefixAbilitySkill())
            {
                armorPrefixAbilityDescUI.gameObject.SetActive(true);
                armorPrefixAbilityDescUI.ShowAbility(armor.prefix);
            }
            else
            {
                armorPrefixAbilityDescUI.gameObject.SetActive(false);
            }

            if (armor.hasSuffixAbilitySkill())
            {
                armorSuffixAbilityDescUI.gameObject.SetActive(true);
                armorSuffixAbilityDescUI.ShowAbility(armor.suffix);
            }
            else
            {
                armorSuffixAbilityDescUI.gameObject.SetActive(false);
            }

            Helmet helmet = status.currentHelmet;
            if (helmet.hasPrefixAbilitySkill())
            {
                helmetPrefixAbilityDescUI.gameObject.SetActive(true);
                helmetPrefixAbilityDescUI.ShowAbility(helmet.prefix);
            }
            else
            {
                helmetPrefixAbilityDescUI.gameObject.SetActive(false);
            }

            if (helmet.hasSuffixAbilitySkill())
            {
                helmetSuffixAbilityDescUI.gameObject.SetActive(true);
                helmetSuffixAbilityDescUI.ShowAbility(helmet.suffix);
            }
            else
            {
                helmetSuffixAbilityDescUI.gameObject.SetActive(false);
            }

            Pants pants = status.currentPants;
            if (pants.hasPrefixAbilitySkill())
            {
                pantsPrefixAbilityDescUI.gameObject.SetActive(true);
                pantsPrefixAbilityDescUI.ShowAbility(pants.prefix);
            }
            else
            {
                pantsPrefixAbilityDescUI.gameObject.SetActive(false);
            }

            if (pants.hasSuffixAbilitySkill())
            {
                pantsSuffixAbilityDescUI.gameObject.SetActive(true);
                pantsSuffixAbilityDescUI.ShowAbility(pants.suffix);
            }
            else
            {
                pantsSuffixAbilityDescUI.gameObject.SetActive(false);
            }

            abilityPropertyObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)abilityPropertyObject.transform);
        }
    }
}