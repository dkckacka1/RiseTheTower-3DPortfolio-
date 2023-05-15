using RPG.Character.Equipment;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Main.UI.BlackSmith
{
    public class IncantAbilityUI : MonoBehaviour
    {
        [SerializeField] Image abilityImage;
        [SerializeField] TextMeshProUGUI abilityDescTxt;

        public void ShowIncant(Incant incant)
        {
            if (incant == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            if (!incant.isIncantAbility)
            {
                this.gameObject.SetActive(false);
                return;
            }

            abilityImage.sprite = incant.abilityIcon;
            abilityDescTxt.text = incant.abilityDesc;
            this.gameObject.SetActive(true);
        }
    }

}