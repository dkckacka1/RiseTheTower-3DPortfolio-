using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Character.Equipment;
using RPG.Core;
using UnityEngine.Events;
using RPG.Main.Audio;

namespace RPG.Main.UI.BlackSmith
{
    public class ItemPopupUI : MonoBehaviour
    {
        public Equipment choiceItem;

        [Header("itemPopupProperty")]
        [SerializeField] Button incantExcuteBtn;
        [SerializeField] Button reinforceExcuteBtn;
        [SerializeField] Button gachaExcuteBtn;
        [SerializeField] TextMeshProUGUI TodoText;

        [Header("EquipmentData")]
        [SerializeField] Image equipmentImage;
        [SerializeField] TextMeshProUGUI equipmentDescText;
        [SerializeField] TextMeshProUGUI weaponEquipmentStatusText;
        [SerializeField] TextMeshProUGUI armorEquipmentStatusText;
        [SerializeField] TextMeshProUGUI helmetEquipmentStatusText;
        [SerializeField] TextMeshProUGUI pantsEquipmentStatusText;
        [SerializeField] VerticalLayoutGroup layout;

        [Header("PrefixIncant")]
        [SerializeField] IncantDescUI prefixDescUI;
        [SerializeField] IncantAbilityUI prefixAbilityUI;

        [Header("SuffixIncant")]
        [SerializeField] IncantDescUI suffixDescUI;
        [SerializeField] IncantAbilityUI suffixAbilityUI;



        [Header("Effecter")]
        [SerializeField] UIEffecter reinforceEffecter;

        CharacterAppearance appearance;
        Animation animation;

        private void Awake()
        {
            animation = GetComponent<Animation>();
            appearance = FindObjectOfType<CharacterAppearance>();
        }

        private void OnEnable()
        {
            animation.Rewind();
            animation.Play();
            animation.Sample();
            animation.Stop();
        }

        public void InitGacha()
        {
            TodoText.fontSize = 18.5f;
            TodoText.text = $"" +
                $"아이템을 새롭게 뽑으시겠습니까?\n" +
                $"(적용된 인챈트와 강화 수치가 사라집니다.)\n" +
                $"(노말 : {Constant.getNormalPercent}%, 레어 : {Constant.getRarelPercent}%, 유니크 : {Constant.getUniquePercent}%, 전설 : {Constant.getLegendaryPercent}%)";

            InitExcuteBtn();

            incantExcuteBtn.gameObject.SetActive(false);
            reinforceExcuteBtn.gameObject.SetActive(false);
            gachaExcuteBtn.gameObject.SetActive(true);


        }

        public void InitIncant()
        {
            TodoText.fontSize = 22;
            TodoText.text = $"아이템에 인챈트를 적용하시겠습니까?\n" +
                $"(접두와 접미 인챈트 둘중에 하나만 인챈트\n" +
                $"되며 기존의 인챈트는 대체됩니다.)";

            InitExcuteBtn();

            incantExcuteBtn.gameObject.SetActive(true);
            reinforceExcuteBtn.gameObject.SetActive(false);
            gachaExcuteBtn.gameObject.SetActive(false);

        }

        public void InitReinforce()
        {
            TodoText.text = $"아이템을 강화하시겠습니까?\n" +
                $"(아이템 강화확률 : {RandomSystem.ReinforceCalc(choiceItem)}%)";

            InitExcuteBtn();

            incantExcuteBtn.gameObject.SetActive(false);
            reinforceExcuteBtn.gameObject.SetActive(true);
            gachaExcuteBtn.gameObject.SetActive(false);
        }

        public void InitExcuteBtn()
        {
            if (GameManager.Instance.UserInfo.itemIncantTicket <= 0)
            {
                incantExcuteBtn.interactable = false;
            }
            else
            {
                incantExcuteBtn.interactable = true;
            }

            if (GameManager.Instance.UserInfo.itemReinforceTicket <= 0)
            {
                reinforceExcuteBtn.interactable = false;
            }
            else
            {
                reinforceExcuteBtn.interactable = true;
            }

            if (GameManager.Instance.UserInfo.itemGachaTicket <= 0)
            {
                gachaExcuteBtn.interactable = false;
            }
            else
            {
                gachaExcuteBtn.interactable = true;
            }
        }
        public void Incant()
        {
            GameManager.Instance.UserInfo.itemIncantTicket--;

            Incant incant;
            RandomSystem.TryGachaIncant(choiceItem.equipmentType, GameManager.Instance.incantDic, out incant);

            choiceItem.Incant(incant);

            ShowItem(choiceItem);
            GameManager.Instance.Player.SetEquipment();
            InitIncant();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            AudioManager.Instance.PlaySoundOneShot("IncantSound");
        }

        public void Gacha()
        {
            GameManager.Instance.UserInfo.itemGachaTicket--;

            EquipmentData data;
            RandomSystem.TryGachaRandomData(GameManager.Instance.equipmentDataDic, choiceItem.equipmentType, out data);
            if (data == null)
            {
                return;
            }

            if (animation.isPlaying)
            {
                ShowItem(choiceItem);
                animation.Stop();
            }

            choiceItem.ChangeData(data);
            if (choiceItem.equipmentType == EquipmentItemType.Weapon)
            {
                appearance.EquipWeapon((data as WeaponData).weaponApparenceID, (data as WeaponData).weaponHandleType);
            }

            GameManager.Instance.Player.SetEquipment();
            InitGacha();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);


            AudioManager.Instance.PlaySoundOneShot("GachaSound");
            animation.Play();
        }
        public void Reinforce()
        {
            GameManager.Instance.UserInfo.itemReinforceTicket--;

            if (MyUtility.ProbailityCalc(100 - RandomSystem.ReinforceCalc(choiceItem), 0, 100))
            {
                choiceItem.ReinforceItem();
                reinforceEffecter.Play();
            }

            GameManager.Instance.Player.SetEquipment();
            ShowItem(choiceItem);
            InitReinforce();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            AudioManager.Instance.PlaySoundOneShot("ReinforceSound");
        }

        public void ShowItemAnim()
        {
            ShowItem(choiceItem);
        }


        public void ChoiceItem(Equipment item)
        {
            choiceItem = item;
            ShowItem(item);
        }

        private void ShowItem(Equipment item)
        {
            equipmentImage.sprite = item.data.equipmentSprite;
            equipmentDescText.text = $"" +
                $"{MyUtility.returnSideText("장비 이름 : ", $"{((item.reinforceCount > 0) ? $"+{item.reinforceCount} " : "")}{item.itemName}")}\n" +
                $"{MyUtility.returnSideText("장비 유형 : ", item.ToStringEquipmentType())}\n" +
                $"{MyUtility.returnSideText("장비 등급 : ", item.ToStringTier())}\n" +
                $"{MyUtility.returnSideText("접두 인챈트 : ", (item.prefix != null ? item.prefix.incantName : "없음"))}\n" +
                $"{MyUtility.returnSideText("접미 인챈트 : ", (item.suffix != null ? item.suffix.incantName : "없음"))}";

            weaponEquipmentStatusText.transform.parent.gameObject.SetActive(false);
            armorEquipmentStatusText.transform.parent.gameObject.SetActive(false);
            pantsEquipmentStatusText.transform.parent.gameObject.SetActive(false);
            helmetEquipmentStatusText.transform.parent.gameObject.SetActive(false); 

            switch (item.equipmentType)
            {
                case EquipmentItemType.Weapon:
                    ShowWeaponText(weaponEquipmentStatusText, item as Weapon);
                    break;
                case EquipmentItemType.Armor:
                    ShowArmorText(armorEquipmentStatusText, item as Armor);
                    break;
                case EquipmentItemType.Pants:
                    ShowPantsText(pantsEquipmentStatusText, item as Pants);
                    break;
                case EquipmentItemType.Helmet:
                    ShowHelmetText(helmetEquipmentStatusText, item as Helmet);
                    break;
            }

            prefixDescUI.ShowIncant(choiceItem.prefix);
            prefixAbilityUI.ShowIncant(choiceItem.prefix);
            suffixDescUI.ShowIncant(choiceItem.suffix);
            suffixAbilityUI.ShowIncant(choiceItem.suffix);

            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layout.transform);
        }

        private void ShowWeaponText(TextMeshProUGUI equipmentStatusText, Weapon weapon)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("공격력 : ", weapon.AttackDamage.ToString())}\n" +
                $"{MyUtility.returnSideText("공격속도 : ", $"초당 {weapon.AttackSpeed}회 타격")}\n" +
                $"{MyUtility.returnSideText("공격범위 : ", weapon.AttackRange.ToString())}\n" +
                $"{MyUtility.returnSideText("이동속도 : ", weapon.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("치명타 확률 : ", $"{weapon.CriticalChance * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 데미지 : ", $"공격력의 {weapon.CriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("적중률 : ", $"{weapon.AttackChance * 100}%")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }

        private void ShowArmorText(TextMeshProUGUI equipmentStatusText, Armor armor)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("체력 : ", armor.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 : ", $"{armor.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("이동속도 : ", armor.MovementSpeed.ToString())}\n" +
                $"{MyUtility.returnSideText("회피율 : ", $"{armor.EvasionPoint}%")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }

        private void ShowHelmetText(TextMeshProUGUI equipmentStatusText, Helmet helmet)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("체력 : ", helmet.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 : ", $"{helmet.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("치명타 데미지 감소율 : ", $"{helmet.DecreseCriticalDamage * 100}%")}\n" +
                $"{MyUtility.returnSideText("치명타 회피율 : ", $"{helmet.EvasionCritical * 100}%")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }

        private void ShowPantsText(TextMeshProUGUI equipmentStatusText, Pants pants)
        {
            equipmentStatusText.text = $"" +
                $"{MyUtility.returnSideText("체력 : ", pants.HpPoint.ToString())}\n" +
                $"{MyUtility.returnSideText("방어력 : ", $"{pants.DefencePoint}")}\n" +
                $"{MyUtility.returnSideText("이동속도 : ", $"{pants.MovementSpeed}")}";

            equipmentStatusText.transform.parent.gameObject.SetActive(true);
        }
    }
}