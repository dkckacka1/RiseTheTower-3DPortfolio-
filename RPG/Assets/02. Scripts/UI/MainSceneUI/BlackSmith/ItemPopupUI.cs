using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Character.Equipment;
using RPG.Core;
using UnityEngine.Events;
using RPG.Main.Audio;

/*
 * 장비 창 UI 클래스
 */

namespace RPG.Main.UI.BlackSmith
{
    public class ItemPopupUI : MonoBehaviour
    {
        public Equipment choiceItem;

        [Header("itemPopupProperty")]
        [SerializeField] Button incantExcuteBtn;            // 인챈트 실행 버튼
        [SerializeField] Button reinforceExcuteBtn;         // 강화 실행 버튼
        [SerializeField] Button gachaExcuteBtn;             // 가챠 실행 버튼
        [SerializeField] TextMeshProUGUI TodoText;          // 버튼 설명 텍스트

        [Header("EquipmentData")]
        [SerializeField] Image equipmentImage;                      // 장비 이미지
        [SerializeField] TextMeshProUGUI equipmentDescText;         // 장비 설명 텍스트
        [SerializeField] TextMeshProUGUI weaponEquipmentStatusText; // 무기 설명 텍스트
        [SerializeField] TextMeshProUGUI armorEquipmentStatusText;  // 갑옷 설명 텍스트
        [SerializeField] TextMeshProUGUI helmetEquipmentStatusText; // 헬멧 설명 텍스트
        [SerializeField] TextMeshProUGUI pantsEquipmentStatusText;  // 바지 설명 텍스트
        [SerializeField] VerticalLayoutGroup layout;                // 레이아웃 그룹

        [Header("PrefixIncant")]
        [SerializeField] IncantDescUI prefixDescUI;         // 접두 인챈트 설명 UI
        [SerializeField] IncantAbilityUI prefixAbilityUI;   // 접두 인챈트 효과 설명 UI

        [Header("SuffixIncant")]
        [SerializeField] IncantDescUI suffixDescUI;         // 접미 인챈트 설명 UI
        [SerializeField] IncantAbilityUI suffixAbilityUI;   // 접미 인챈트 효과 설명 UI

        [Header("Effecter")]
        [SerializeField] UIEffecter reinforceEffecter;  // 강화 이펙트 

        CharacterAppearance appearance; // 현재 로비 캐릭터 외형
        Animation animation;            // 현재 창의 애니메이션

        private void Awake()
        {
            animation = GetComponent<Animation>();
            appearance = FindObjectOfType<CharacterAppearance>();
        }

        // 창이 활성화되었다면 애니메이션을 초기화합니다.
        private void OnEnable()
        {
            animation.Rewind();
            animation.Play();
            animation.Sample();
            animation.Stop();
        }

        // 가챠 버튼 눌렀을 때 세팅합니다.
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

        // 인챈트 버튼 눌렀을 때 세팅합니다.
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

        // 강화 버튼 눌렀을 때 세팅합니다.
        public void InitReinforce()
        {
            TodoText.text = $"아이템을 강화하시겠습니까?\n" +
                $"(아이템 강화확률 : {RandomSystem.ReinforceCalc(choiceItem)}%)";

            InitExcuteBtn();

            incantExcuteBtn.gameObject.SetActive(false);
            reinforceExcuteBtn.gameObject.SetActive(true);
            gachaExcuteBtn.gameObject.SetActive(false);
        }

        // 실행 버튼을 세팅합니다.
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
        
        // 장비를 인챈트 합니다.
        public void Incant()
        {
            GameManager.Instance.UserInfo.itemIncantTicket--;

            Incant incant;
            // 현재 선택한 장비아이템 타입의 랜덤한 등금의 인챈트를 가져옵니다.
            RandomSystem.TryGachaIncant(choiceItem.equipmentType, GameManager.Instance.incantDic, out incant);

            // 아이템을 인챈트 합니다.
            choiceItem.Incant(incant);

            // 장비아이템 설명을 표시하고 해당 장비아이템을 현재 캐릭터에 장착시킵니다.
            ShowItem(choiceItem);
            GameManager.Instance.Player.SetEquipment();
            InitIncant();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            AudioManager.Instance.PlaySoundOneShot("IncantSound");
        }

        // 장비를 뽑습니다.
        public void Gacha()
        {
            GameManager.Instance.UserInfo.itemGachaTicket--;

            EquipmentData data;
            // 랜덤한 등급의 현재 아이템과 같은 아이템을 뽑습니다.
            RandomSystem.TryGachaRandomData(GameManager.Instance.equipmentDataDic, choiceItem.equipmentType, out data);
            if (data == null)
            {
                return;
            }

            // 가챠 애니메이션을 보여줍니다.
            if (animation.isPlaying)
            {
                ShowItem(choiceItem);
                animation.Stop();
            }

            // 선택한 아이템의 데이터를 변경해줍니다.
            choiceItem.ChangeData(data);
            if (choiceItem.equipmentType == EquipmentItemType.Weapon)
            {
                appearance.EquipWeapon((data as WeaponData).weaponApparenceID, (data as WeaponData).weaponHandleType);
            }

            //뽑은 아이템을 장착 시켜줍니다.
            GameManager.Instance.Player.SetEquipment();
            InitGacha();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);


            AudioManager.Instance.PlaySoundOneShot("GachaSound");
            animation.Play();
        }
        // 선택한 아이템을 강화합니다.
        public void Reinforce()
        {
            GameManager.Instance.UserInfo.itemReinforceTicket--;

            if (MyUtility.ProbailityCalc(100 - RandomSystem.ReinforceCalc(choiceItem), 0, 100))
                // 강화에 성공했다면
            {
                // 장비를 강화하고 강화 이펙트를 보여줍니다.
                choiceItem.ReinforceItem();
                reinforceEffecter.Play();
            }

            // 로비 캐릭터가 장비를 장착합니다.
            GameManager.Instance.Player.SetEquipment();
            ShowItem(choiceItem);
            InitReinforce();
            GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            AudioManager.Instance.PlaySoundOneShot("ReinforceSound");
        }

        // 아이템을 보여줍니다.
        public void ShowItemAnim()
        {
            ShowItem(choiceItem);
        }


        // 장비아이템을 선택합니다.
        public void ChoiceItem(Equipment item)
        {
            choiceItem = item;
            ShowItem(item);
        }

        // 아이템 설명을 보여줍니다.
        private void ShowItem(Equipment item)
        {
            // 아이템의 이미지와 설명을 표시합니다.
            equipmentImage.sprite = item.data.equipmentSprite;
            equipmentDescText.text = $"" +
                $"{MyUtility.returnSideText("장비 이름 : ", $"{((item.reinforceCount > 0) ? $"+{item.reinforceCount} " : "")}{item.itemName}")}\n" +
                $"{MyUtility.returnSideText("장비 유형 : ", item.ToStringEquipmentType())}\n" +
                $"{MyUtility.returnSideText("장비 등급 : ", item.ToStringTier())}\n" +
                $"{MyUtility.returnSideText("접두 인챈트 : ", (item.prefix != null ? item.prefix.incantName : "없음"))}\n" +
                $"{MyUtility.returnSideText("접미 인챈트 : ", (item.suffix != null ? item.suffix.incantName : "없음"))}";

            // 모든 장비 타입 설명을 숨겨주고 알맞는 장비타입 설명만 보여줍니다.

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

            // 장비에 적용되어있는 인챈트를 보여줍니다.
            prefixDescUI.ShowIncant(choiceItem.prefix);
            prefixAbilityUI.ShowIncant(choiceItem.prefix);
            suffixDescUI.ShowIncant(choiceItem.suffix);
            suffixAbilityUI.ShowIncant(choiceItem.suffix);

            // 레이아웃을 재구성합니다.
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)layout.transform);
        }

        // 각 장비아이템 타입에 맞는 스탯을 설명합니다.
        // 스텟은 양 라벨과 스탯 수치를 양사이드에 세팅합니다.

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