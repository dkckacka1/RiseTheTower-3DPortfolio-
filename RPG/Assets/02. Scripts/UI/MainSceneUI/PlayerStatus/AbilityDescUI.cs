using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Character.Equipment;

namespace RPG.Main.UI.StatusUI

{
    public class AbilityDescUI : MonoBehaviour
    {
        [SerializeField] Image abilityImage;
        [SerializeField] TextMeshProUGUI abilityDescText;

        public void ShowAbility(Incant incant)
        {
            if (!incant.isIncantAbility)
            {
                gameObject.SetActive(false);
                return;
            }

            abilityImage.sprite = incant.abilityIcon;
            abilityDescText.text = incant.abilityDesc;
        }
    }
}