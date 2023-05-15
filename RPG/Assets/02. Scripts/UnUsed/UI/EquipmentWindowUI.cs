using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using RPG.Character.Status;
using RPG.Character.Equipment;

namespace RPG.UnUsed
{ 
    public class EquipmentWindowUI : MonoBehaviour
    {
        public Equipment choiceItem;

        [Header("StatusText")]
        [SerializeField] TextMeshProUGUI equipmentName;
        [SerializeField] TextMeshProUGUI equipmentDesc;
        [SerializeField] TextMeshProUGUI equipmentStatus;
        [SerializeField] TextMeshProUGUI equipmentReinforce;

        [Header("Ability")]
        [SerializeField] EquipmentIncantAbilityUI prefixIncantUI;
        [SerializeField] EquipmentIncantAbilityUI suffixIncantUI;
        [SerializeField] TextMeshProUGUI incantAbilityDescText;

        [Header("IncantColor")]
        [SerializeField] Color suffixColor; // 접두 표현 컬러
        [SerializeField] Color prefixColor; // 접미 표현 컬러

        [Header("ReinfoceColor")]
        [SerializeField] Color defaultColor;
        [SerializeField] Color reinfoce10;
        [SerializeField] Color reinfoce20;
        [SerializeField] Color reinfoce30;
        [SerializeField] Color reinfoce40;

        private void OnEnable()
        {
            ShowWeapon();
        }

        public void Init()
        {
            ShowWeapon();
        }

        #region ButtonPlugin
        public void ShowWeapon()
        {
            UpdateItem(GameManager.Instance.Player.currentWeapon);
        }

        public void ShowArmor()
        {
            UpdateItem(GameManager.Instance.Player.currentArmor);
        }

        public void ShowHelmet()
        {
            UpdateItem(GameManager.Instance.Player.currentHelmet);
        }

        public void ShowPants()
        {
            UpdateItem(GameManager.Instance.Player.currentPants);
        }


        #endregion

        public void UpdateItem(Equipment item)
        {
            choiceItem = item;

            ShowNameText(choiceItem);
            ShowDescText(choiceItem);
            switch (choiceItem.equipmentType)
            {
                case EquipmentItemType.Weapon:
                    ShowItemStatus((choiceItem as Weapon));
                    break;
                case EquipmentItemType.Armor:
                    ShowItemStatus((choiceItem as Armor));
                    break;
                case EquipmentItemType.Pants:
                    ShowItemStatus((choiceItem as Pants));
                    break;
                case EquipmentItemType.Helmet:
                    ShowItemStatus((choiceItem as Helmet));
                    break;
            }

            if (item.hasAbilitySkill())
            {
                if (item.hasPrefixAbilitySkill())
                {
                    prefixIncantUI.gameObject.SetActive(true);
                    prefixIncantUI.InitAbility(item.prefix, () => { incantAbilityDescText.text = $"{item.prefix.abilityDesc}"; });
                }
                else
                {
                    prefixIncantUI.gameObject.SetActive(false);
                }

                if (item.hasSuffixAbilitySkill())
                {
                    suffixIncantUI.gameObject.SetActive(true);
                    suffixIncantUI.InitAbility(item.suffix, () => { incantAbilityDescText.text = $"{item.suffix.abilityDesc}"; });
                }
                else
                {
                    suffixIncantUI.gameObject.SetActive(false);
                }

                incantAbilityDescText.text = "인챈트 이미지를 누르시면 인챈트 설명이 나옵니다.";
            }
            else
            {
                prefixIncantUI.gameObject.SetActive(false);
                suffixIncantUI.gameObject.SetActive(false);
                incantAbilityDescText.text = "인챈트 스킬이 없습니다.";
            }

            ShowReinforceCount(choiceItem);
        }


        public void ShowNameText(Equipment equipment)
        {
            string name = $"<color=\"white\">{equipment.itemName}";
            if (equipment.isIncant())
            {
                name = "\n" + name;
            }
            name = (equipment.suffix != null) ? MyUtility.returnColorText(equipment.suffix.incantName, suffixColor) + name : name;
            name = (equipment.prefix != null) ? MyUtility.returnColorText(equipment.prefix.incantName, prefixColor) + name : name;
            equipmentName.text = name;
        }

        public void ShowDescText(Equipment equipment)
        {
            string desc = equipment.description;
            //desc = (equipment.prefix != null) ? $"{desc}\n{equipment.prefix.ShowDesc()}" : desc;
            //desc = (equipment.suffix != null) ? $"{desc}\n{equipment.suffix.ShowDesc()}" : desc;
            equipmentDesc.text = desc;

        }

        public void ShowReinforceCount(Equipment equipment)
        {
            if (!equipment.isReinforce())
            {
                equipmentReinforce.gameObject.SetActive(false);
                return;
            }

            equipmentReinforce.gameObject.SetActive(true);
            equipmentReinforce.text = $"+{equipment.reinforceCount}";

            if (equipment.reinforceCount < 10)
            {
                equipmentReinforce.color = defaultColor;
            }
            else if (equipment.reinforceCount < 20)
            {
                equipmentReinforce.color = reinfoce10;
            }
            else if (equipment.reinforceCount < 30)
            {
                equipmentReinforce.color = reinfoce20;
            }
            else if (equipment.reinforceCount < 40)
            {
                equipmentReinforce.color = reinfoce30;
            }
            else if (equipment.reinforceCount < 50)
            {
                equipmentReinforce.color = reinfoce40;
            }
        }

        #region 장비 스텟 설명 부분


        public void ShowItemStatus(Weapon weapon)
        {
            string status =
                $"{MyUtility.returnSideText("공격력 :", weapon.AttackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("공격 속도 :", $"초당 {weapon.AttackSpeed}회 타격")}\n" +
                $"{MyUtility.returnSideText("공격 범위 :", weapon.AttackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("이동 속도 :", weapon.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("치명타 확률 :", $"{weapon.CriticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 데미지 :", $"기본 공격력의 {weapon.CriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("명중률 :", $"{weapon.AttackChance * 100}%")}";

            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Armor armor)
        {
            string status =
                $"{MyUtility.returnSideText("체력 :", armor.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 :", $"{armor.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("이동 속도 :", armor.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("회피율 :", $"{armor.EvasionPoint * 100}%")}";

            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Helmet helmet)
        {
            string status =
                $"{MyUtility.returnSideText("체력 :", helmet.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 :", $"{helmet.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("치명타 회피율 :", $"{helmet.EvasionCritical * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 피해 감소 :", $"{helmet.DecreseCriticalDamage * 100}%")}";


            equipmentStatus.text = status;
        }

        public void ShowItemStatus(Pants pants)
        {
            string status =
                $"{MyUtility.returnSideText("체력 :", pants.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 :", $"{pants.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("이동 속도 :", pants.MovementSpeed.ToString())}";

            equipmentStatus.text = status;
        }
        #endregion


    }

}