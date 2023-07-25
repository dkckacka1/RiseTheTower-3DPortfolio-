using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/*
 * 전투 캐릭터의 디버프 상황을 보여주는 UI
 */

namespace RPG.Battle.UI
{
    public class DebuffUI : MonoBehaviour
    {
        [Header("Bloody")]
        [SerializeField] GameObject bloodyObject;           // 출혈 상태 오브젝트
        [SerializeField] TextMeshProUGUI remainBloodyText;  // 남은 출혈 시간
        int bloodyOverlapping;                              // 출혈 중첩 횟수

        [Header("Curse")]
        [SerializeField] GameObject curseObject;            // 저주 상태 오브젝트
        [SerializeField] TextMeshProUGUI remainCurseText;   // 남은 저주 시간

        [Header("Fear")]
        [SerializeField] GameObject FearObject;             // 공포 상태 오브젝트
        [SerializeField] TextMeshProUGUI remainFearText;    // 남은 공포 시간

        [Header("Paralysis")]
        [SerializeField] GameObject paralysisObject;            // 마비 상태 오브젝트
        [SerializeField] TextMeshProUGUI remainParalysisText;   // 남은 마비 시간

        [Header("Stern")]
        [SerializeField] GameObject sternObject;                // 기절 상태 오브젝트
        [SerializeField] TextMeshProUGUI remainSternText;       // 남은 기절 시간

        [Header("Temptation")]
        [SerializeField] GameObject TemptationObject;           // 유혹 상태 오브젝트
        [SerializeField] TextMeshProUGUI remainTemptationText;  // 남은 유혹 시간 

        // 모든 디버프 UI를 초기화합니다..
        public void ResetAllDebuff()
        {
            ResetDebuff(DebuffType.Stern);
            ResetDebuff(DebuffType.Bloody);
            ResetDebuff(DebuffType.Paralysis);
            ResetDebuff(DebuffType.Temptation);
            ResetDebuff(DebuffType.Fear);
            ResetDebuff(DebuffType.Curse);
        }

        // 디버프 UI를 초기화합니다.
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

        // 디버브 UI를 보여줍니다
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

        // 디버프 UI를 숨겨줍니다.
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

        // 디버프 UI를 보여줍니다.
        public void ShowDebuff(DebuffType type, float duration)
        {
            // 알맞는 UI 오브젝트를 보여주고 남은 시간을 표기합니다.
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