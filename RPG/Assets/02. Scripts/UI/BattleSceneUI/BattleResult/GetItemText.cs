using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using DG.Tweening;

namespace RPG.Battle.UI
{
    public class GetItemText : MonoBehaviour
    {
        TextMeshProUGUI txt;

        float showGain = 0;
        float DelayPerGainNum = 0;
        [SerializeField] float gainDelay = 0.01f;

        private void Awake()
        {
            txt = GetComponent<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            txt.text = "0";
        }

        public void GainText(int gain, float time = 1f, UnityAction action = null)
        {
            DelayPerGainNum = gain / (time / gainDelay);
            StartCoroutine(GainCoroutine(gain, action));
        }

        IEnumerator GainCoroutine(int gain, UnityAction action)
        {
            float currentTime = 0;

            while (true)
            {
                yield return new WaitForSecondsRealtime(gainDelay);
                currentTime += gainDelay;
                showGain += DelayPerGainNum;

                txt.text = showGain.ToString("F0");

                if ((int)(showGain + 0.5) >= gain)
                {
                    break;
                }

            }

            if (action != null)
            {
                action.Invoke();
            }
        }
    }

}