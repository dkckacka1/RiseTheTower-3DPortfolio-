using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

/*
 * 전투 결과창에 표시될 아이템 획득 텍스트 UI 클래스
 */ 

namespace RPG.Battle.UI
{
    public class GetItemText : MonoBehaviour
    {
        TextMeshProUGUI txt;    // 텍스트

        float showGain = 0;                         // 얻은 개수
        float DelayPerGainNum = 0;                  // 한 틱당 올라갈 갯수
        [SerializeField] float gainDelay = 0.01f;   // 한 틱의 시간

        private void Awake()
        {
            txt = GetComponent<TextMeshProUGUI>();
        }

        // 활성화 될때 초기화합니다.
        private void OnEnable()
        {
            txt.text = "0";
        }

        // 텍스트를 보여줍니다.
        public void GainText(int gain, float time = 1f)
        {
            // 한틱당 얻을 갯수를 정의합니다.
            DelayPerGainNum = gain / (time / gainDelay);
            // 코루틴을 수행합니다.
            StartCoroutine(GainCoroutine(gain));
        }

        // 텍스트가 순차적으로 올라갈 수 있도록 하는 코루틴
        IEnumerator GainCoroutine(int gain)
        {
            while (true)
            {
                // 딜레이만큼 대기합니다.
                yield return new WaitForSecondsRealtime(gainDelay);
                // 틱만큼 올라갈 갯수를 얻습니다.
                showGain += DelayPerGainNum;

                // 텍스트를 업데이트합니다.
                txt.text = showGain.ToString("F0");

                // 표시될 갯수가 현재 갯수보다 커졌다면
                if ((int)(showGain + 0.5) >= gain)
                {
                    // 텍스트를 업데이트하고 반복문을 중단합니다.
                    txt.text = gain.ToString();
                    break;
                }
            }
        }
    }

}