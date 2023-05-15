using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace RPG.Battle.UI
{
    public class DebuffUI : MonoBehaviour
    {
        [Header("Bloody")]
        [SerializeField] GameObject bloodyObject;
        [SerializeField] TextMeshProUGUI remainBloodyText;
        int bloodyOverlapping;

        [Header("Curse")]
        [SerializeField] GameObject curseObject;
        [SerializeField] TextMeshProUGUI remainCurseText;

        [Header("Fear")]
        [SerializeField] GameObject FearObject;
        [SerializeField] TextMeshProUGUI remainFearText;

        [Header("Paralysis")]
        [SerializeField] GameObject paralysisObject;
        [SerializeField] TextMeshProUGUI remainParalysisText;

        [Header("Stern")]
        [SerializeField] GameObject sternObject;
        [SerializeField] TextMeshProUGUI remainSternText;

        [Header("Temptation")]
        [SerializeField] GameObject TemptationObject;
        [SerializeField] TextMeshProUGUI remainTemptationText;

        public void ResetAllDebuff()
        {
            ResetDebuff(DebuffType.Stern);
            ResetDebuff(DebuffType.Bloody);
            ResetDebuff(DebuffType.Paralysis);
            ResetDebuff(DebuffType.Temptation);
            ResetDebuff(DebuffType.Fear);
            ResetDebuff(DebuffType.Curse);
        }

        public void ResetDebuff(DebuffType type)
        {
            switch (type)
            {
                case DebuffType.Stern:
                    sternObject.SetActive(false);
                    break;
                case DebuffType.Bloody:
                    bloodyObject.SetActive(false);
                    bloodyOverlapping = 0;
                    break;
                case DebuffType.Paralysis:
                    paralysisObject.SetActive(false);
                    break;
                case DebuffType.Temptation:
                    TemptationObject.SetActive(false);
                    break;
                case DebuffType.Fear:
                    FearObject.SetActive(false);
                    break;
                case DebuffType.Curse:
                    curseObject.SetActive(false);
                    break;
            }
        }

        public void InitDebuff(DebuffType type)
        {
            switch (type)
            {
                case DebuffType.Stern:
                    sternObject.SetActive(true);
                    break;
                case DebuffType.Bloody:
                    bloodyOverlapping++;
                    if (!bloodyObject.activeInHierarchy)
                    {
                        bloodyObject.SetActive(true);
                    }
                    break;
                case DebuffType.Paralysis:
                    paralysisObject.SetActive(true);
                    break;
                case DebuffType.Temptation:
                    TemptationObject.SetActive(true);
                    break;
                case DebuffType.Fear:
                    FearObject.SetActive(true);
                    break;
                case DebuffType.Curse:
                    curseObject.SetActive(true);
                    break;
            }
        }

        public void ReleaseDebuff(DebuffType type)
        {
            switch (type)
            {
                case DebuffType.Stern:
                    sternObject.SetActive(false);
                    break;
                case DebuffType.Bloody:
                    bloodyOverlapping--;
                    if (bloodyOverlapping <= 0)
                    {
                        bloodyObject.SetActive(false);
                    }
                    break;
                case DebuffType.Paralysis:
                    paralysisObject.SetActive(false);
                    break;
                case DebuffType.Temptation:
                    TemptationObject.SetActive(false);
                    break;
                case DebuffType.Fear:
                    FearObject.SetActive(false);
                    break;
                case DebuffType.Curse:
                    curseObject.SetActive(false);
                    break;
            }
        }

        public void ShowDebuff(DebuffType type, float duration)
        {
            switch (type)
            {
                case DebuffType.Stern:
                    remainSternText.text = $"{duration.ToString("N1")}";
                    break;
                case DebuffType.Bloody:
                    if (bloodyOverlapping > 1)
                    {
                        remainBloodyText.text = bloodyOverlapping.ToString();
                    }
                    else
                    {
                        remainSternText.text = $"{duration.ToString("N1")}";
                    }
                    break;
                case DebuffType.Paralysis:
                    remainParalysisText.text = $"{duration.ToString("N1")}";
                    break;
                case DebuffType.Temptation:
                    remainTemptationText.text = $"{duration.ToString("N1")}";
                    break;
                case DebuffType.Fear:
                    remainFearText.text = $"{duration.ToString("N1")}";
                    break;
                case DebuffType.Curse:
                    remainCurseText.text = $"{duration.ToString("N1")}";
                    break;
            }
        }
    }

}