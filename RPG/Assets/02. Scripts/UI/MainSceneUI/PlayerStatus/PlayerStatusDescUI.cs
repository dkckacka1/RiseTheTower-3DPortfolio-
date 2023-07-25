using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using RPG.Character.Status;
using RPG.Character.Equipment;

/*
 * 플레이어 캐릭터의 스텟을 볼수 있는 창 UI 클래스
 */

namespace RPG.Main.UI.StatusUI
{
    public class PlayerStatusDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI attackStatusText;      // 공격 수치 텍스트
        [SerializeField] TextMeshProUGUI defenceStatusText;     // 방어 수치 텍스트

        [Header("Ability")]
        [SerializeField] GameObject abilityPropertyObject;              // 인챈트 효과의 부모 오브젝트
        [SerializeField] AbilityDescUI weaponPrefixAbilityDescUI;       // 무기 접두 인챈트 효과 설명 UI
        [SerializeField] AbilityDescUI weaponSuffixAbilityDescUI;       // 무기 접미 인챈트 효과 설명 UI
        [SerializeField] AbilityDescUI armorPrefixAbilityDescUI;        // 갑옷 접두 인챈트 효과 설명 UI
        [SerializeField] AbilityDescUI armorSuffixAbilityDescUI;        // 갑옷 접미 인챈트 효과 설명 UI
        [SerializeField] AbilityDescUI helmetPrefixAbilityDescUI;       // 헬멧 접두 인챈트 효과 설명 UI
        [SerializeField] AbilityDescUI helmetSuffixAbilityDescUI;       // 헬멧 접미 인챈트 효과 설명 UI
        [SerializeField] AbilityDescUI pantsPrefixAbilityDescUI;        // 바지 접두 인챈트 효과 설명 UI
        [SerializeField] AbilityDescUI pantsSuffixAbilityDescUI;        // 바지 접미 인챈트 효과 설명 UI

        // 창이 활성화되면 플레이어 캐릭터의 스텟을 보여줍니다.
        private void OnEnable()
        {
            ShowPlayerStatus();
        }

        public void ShowPlayerStatus()
        {
            // 공격, 방어, 인챈트 설명을 보여주고 레이아웃을 재구성합니다.
            ShowAttackStatus(GameManager.Instance.Player);
            ShowDefenceStatus(GameManager.Instance.Player);
            ShowPlayerAbility(GameManager.Instance.Player);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)abilityPropertyObject.transform);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)abilityPropertyObject.transform.parent.transform);
        }

        // 공격 스탯을 보여줍니다.
        // 스탯의 라벨과 값은 양사이드에 위치시킵니다.
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

        // 방어 스탯을 보여줍니다.
        // 스탯의 라벨과 값은 양사이드에 위치시킵니다.
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

        // 플레이어 장비의 인챈트의 효과를 보여줍니다.
        private void ShowPlayerAbility(PlayerStatus status)
        {
            if (!status.hasAbility())
                // 모든 장비의 효과가 없다면 효과 설명 UI의 부모 오브젝트를 비활성화 합니다.
            {
                abilityPropertyObject.SetActive(false);
                return;
            }
            
            // 각 장비의 인챈트 여부, 인챈트의 효과 여부를 확인해서 설명을 표시합니다.
            // 마지막으로 레이아웃을 재구성합니다.

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