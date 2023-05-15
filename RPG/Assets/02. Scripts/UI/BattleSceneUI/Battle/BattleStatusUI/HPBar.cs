using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public class HPBar : MonoBehaviour
    {
        [SerializeField] Slider hpSlider;
        [SerializeField] Slider remainSlider;

        Coroutine hpDownCoroutine;

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
                {
                    StopCoroutine(hpDownCoroutine);
                    hpDownCoroutine = null;
                }

                hpDownCoroutine = StartCoroutine(HPDownCoroutine());
            }
            else
            // 현재 체력이 증가된 경우
            {
                remainSlider.value = currentHp;
            }
            hpSlider.value = currentHp;
        }

        private IEnumerator HPDownCoroutine()
        {
            yield return new WaitForSeconds(0.25f);
            float changeValue = (remainSlider.value - hpSlider.value) * 2;

            while (true)
            {
                remainSlider.value -= changeValue * Time.deltaTime;
                yield return null;

                if (remainSlider.value <= hpSlider.value)
                {
                    break;
                }
            }
        }
    }
}