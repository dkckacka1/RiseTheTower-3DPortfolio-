using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 체력바 UI 클래스
 */

namespace RPG.Battle.UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] Slider hpSlider;       // 체력바 슬라이더
        [SerializeField] Slider remainSlider;   // 피격 체력바 슬라이더

        Coroutine hpDownCoroutine;  // 체력 감소 코루틴

        // 체력바를 초기화합니다
        public virtual void InitHpSlider(int maxHp)
        {
            hpSlider.maxValue = maxHp;
            hpSlider.value= maxHp;
            remainSlider.maxValue = maxHp;
            remainSlider.value = maxHp;
        }

        public virtual void ChangeCurrentHP(int currentHp)
        {
            if (hpSlider.value >= currentHp)
            // 현재 체력이 감소된 경우
            {
                if (hpDownCoroutine != null)
                    // 이미 체력 감소중이었다면 취소시킵니다.
                {
                    StopCoroutine(hpDownCoroutine);
                    hpDownCoroutine = null;
                }

                // 체력바 감소 코루틴을 시작합니다.
                hpDownCoroutine = StartCoroutine(HPDownCoroutine());
            }
            else
            // 현재 체력이 증가된 경우
            {
                remainSlider.value = currentHp;
            }
            hpSlider.value = currentHp;
        }

        // 체력바 감소하는 코루틴입니다.
        private IEnumerator HPDownCoroutine()
        {
            // 피격 체력바가 바로 감소되지 않도록 잠시 대기합니다.
            yield return new WaitForSeconds(0.25f);
            // 점차 감소할 슬라이더 값
            float changeValue = (remainSlider.value - hpSlider.value) * 2;

            while (true)
            {
                // 감소 값만큼 피격 슬라이드를 수정합니다.
                remainSlider.value -= changeValue * Time.deltaTime;
                yield return null;

                if (remainSlider.value <= hpSlider.value)
                    // 피격 슬라이더 값이 현재 체력 슬라이더 값보다 낮아질 경우 반복문 중단
                {
                    break;
                }
            }
        }
    }
}