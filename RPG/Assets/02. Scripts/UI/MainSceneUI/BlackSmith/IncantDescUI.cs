using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using RPG.Character.Equipment;
using UnityEngine.UI;

namespace RPG.Main.UI.BlackSmith
{
    public class IncantDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI nameTxt;
        [SerializeField] TextMeshProUGUI tierTxt;
        [SerializeField] TextMeshProUGUI addDescTxt;
        [SerializeField] TextMeshProUGUI minusDescTxt;

        public void ShowIncant(Incant incant)
        {
            if (incant == null)
            {
                this.gameObject.SetActive(false);
                return;
            }

            nameTxt.text = incant.incantName;

            string tierStr = "";

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

            string addDesc = incant.GetAddDesc();

            if (addDesc == "")
            {
                addDescTxt.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                addDescTxt.text = addDesc;
                addDescTxt.transform.parent.gameObject.SetActive(true);
            }

            string minusDesc = incant.GetMinusDesc();

            if (minusDesc == "")
            {
                minusDescTxt.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                minusDescTxt.text = minusDesc;
                minusDescTxt.transform.parent.gameObject.SetActive(true);
            }

            for (int i = 0; i < this.transform.childCount; i++)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform.GetChild(i));
            }
            this.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)transform);
        }
    }
}

