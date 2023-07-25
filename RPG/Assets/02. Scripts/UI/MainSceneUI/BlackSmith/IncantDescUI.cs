using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Character.Equipment;
using UnityEngine.UI;

/*
 * 인챈트 설명 UI 클래스
 */

namespace RPG.Main.UI.BlackSmith
{
    public class IncantDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameTxt;           // 인챈트 이름 텍스트
        [SerializeField] TextMeshProUGUI tierTxt;           // 인챈트 등급 텍스트
        [SerializeField] TextMeshProUGUI addDescTxt;        // 증가 효과 텍스트
        [SerializeField] TextMeshProUGUI minusDescTxt;      // 감소 효과 텍스트

        // 인챈트를 보여줍니다
        public void ShowIncant(Incant incant)
        {
            if (incant == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            nameTxt.text = incant.incantName;

            string tierStr = "";

            // 현재 인챈트 등급을 표시합니다.
            switch (incant.incantTier)
            {
                case TierType.Normal:
                    tierStr = "노말";
                    break;
                case TierType.Rare:
                    tierStr = "레어";
                    break;
                case TierType.Unique:
                    tierStr = "유니크";
                    break;
                case TierType.Legendary:
                    tierStr = "전설";
                    break;
            }

            tierTxt.text = tierStr;

            // 인챈트의 증가 효과 문자열을 가져옵니다.
            string addDesc = incant.GetAddDesc();

            if (addDesc == "")
            // 증가효과가 없다면 증가 효과 텍스트를 숨겨줍니다.
            {
                addDescTxt.transform.parent.gameObject.SetActive(false);
            }
            else
            // 증가 효과가 있다면 증가 효과 텍스트를 보여줍니다.
            {
                addDescTxt.text = addDesc;
                addDescTxt.transform.parent.gameObject.SetActive(true);
            }

            // 인챈트의 감소 효과 문자열을 가져옵니다.
            string minusDesc = incant.GetMinusDesc();

            if (minusDesc == "")
            // 감소 효과가 없다면 감소 효과 텍스트를 숨겨줍니다.
            {
                minusDescTxt.transform.parent.gameObject.SetActive(false);
            }
            else
            // 감소 효과가 있다면 감소 효과 텍스트를 보여줍니다.
            {
                minusDescTxt.text = minusDesc;
                minusDescTxt.transform.parent.gameObject.SetActive(true);
            }

            // 자신과 자신의 자식 오브젝트의 모든 UI를 재구성합니다.
            for (int i = 0; i < this.transform.childCount; i++)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.GetChild(i));
            }
            this.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        }
    }
}

