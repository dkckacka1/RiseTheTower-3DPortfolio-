using RPG.Character.Equipment;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 인챈트 효과 UI 클래스
 */

namespace RPG.Main.UI.BlackSmith
{
    public class IncantAbilityUI : MonoBehaviour
    {
        [SerializeField] Image abilityImage;                // 인챈트 이미지
        [SerializeField] TextMeshProUGUI abilityDescTxt;    // 인챈트 설명 텍스트

        // 인챈트를 보여줍니다.
        public void ShowIncant(Incant incant)
        {
            // 인챈트가 없거나 인챈트에 효과가없는 경우 비활성화합니다.
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

            // 효과가 있다면 보여줍니다.
            abilityImage.sprite = incant.abilityIcon;   
            abilityDescTxt.text = incant.abilityDesc;   
            this.gameObject.SetActive(true);
        }
    }

}