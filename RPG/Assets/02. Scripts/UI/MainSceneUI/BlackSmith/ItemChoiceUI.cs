using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 장비 선택 창 UI 클래스
 */

namespace RPG.Main.UI.BlackSmith
{
    public class ItemChoiceUI : MonoBehaviour
    {
        [SerializeField] Image weaponImage; // 무기 이미지
        [SerializeField] Image armorImage;  // 갑옷 이미지
        [SerializeField] Image HelmetImage; // 헬멧 이미지
        [SerializeField] Image pantsImage;  // 바지 이미지

        // 버튼 이미지를 세팅합니다.
        public void InitButtonImage()
        {
            weaponImage.sprite = GameManager.Instance.Player.currentWeapon.data.equipmentSprite;
            armorImage.sprite = GameManager.Instance.Player.currentArmor.data.equipmentSprite;
            HelmetImage.sprite = GameManager.Instance.Player.currentHelmet.data.equipmentSprite;
            pantsImage.sprite = GameManager.Instance.Player.currentPants.data.equipmentSprite;
        }

        // 각 장비를 선택하고
        // 유저의 장비 정보를 장비 창 UI에 알려줍니다.

        public void ChoiceWeapon(ItemPopupUI ui)
        {
            ui.ChoiceItem(GameManager.Instance.Player.currentWeapon);
        }

        public void ChoiceArmor(ItemPopupUI ui)
        {
            ui.ChoiceItem(GameManager.Instance.Player.currentArmor);
        }

        public void ChoiceHelmet(ItemPopupUI ui)
        {
            ui.ChoiceItem(GameManager.Instance.Player.currentHelmet);
        }

        public void ChoicePants(ItemPopupUI ui)
        {
            ui.ChoiceItem(GameManager.Instance.Player.currentPants);
        }
    }
}
