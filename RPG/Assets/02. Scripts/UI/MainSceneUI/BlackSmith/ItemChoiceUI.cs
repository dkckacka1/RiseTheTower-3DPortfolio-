using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Main.UI.BlackSmith
{
    public class ItemChoiceUI : MonoBehaviour
    {
        [SerializeField] Image weaponImage;
        [SerializeField] Image armorImage;
        [SerializeField] Image HelmetImage;
        [SerializeField] Image pantsImage;

        public void InitButtonImage()
        {
            weaponImage.sprite = GameManager.Instance.Player.currentWeapon.data.equipmentSprite;
            armorImage.sprite = GameManager.Instance.Player.currentArmor.data.equipmentSprite;
            HelmetImage.sprite = GameManager.Instance.Player.currentHelmet.data.equipmentSprite;
            pantsImage.sprite = GameManager.Instance.Player.currentPants.data.equipmentSprite;
        }

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
