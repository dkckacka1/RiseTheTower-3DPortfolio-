using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Character.Equipment;

/*
 * 스탯창의 인챈트 효과 설명 UI 클래스
 */

namespace RPG.Main.UI.StatusUI
{
    public class AbilityDescUI : MonoBehaviour
    {
        [SerializeField] Image abilityImage;                // 효과 이미지
        [SerializeField] TextMeshProUGUI abilityDescText;   // 효과 설명 텍스트

        // 인챈트의 효과를 보여줍니다.
        public void ShowAbility(Incant incant)
        {
            if (!incant.isIncantAbility)
                // 인챈트에 따로 효과가 없다면 숨겨줍니다.
            {
                gameObject.SetActive(false);
                return;
            }

            abilityImage.sprite = incant.abilityIcon;
            abilityDescText.text = incant.abilityDesc;
        }
    }
}