using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 스프라이트로만든 UI 애니메이션 이펙트 클래스
 */

namespace RPG.Core
{
    public abstract class UIEffecter : MonoBehaviour
    {
        public Canvas effectCanvas;     // 이펙트 캔버스

        public List<UIEffecter> effectList = new List<UIEffecter>();        // 자기 밑의 UI이펙트 리스트
        public bool isPlaying = false;                                      // 현재 플레이 중인지 여부
        public bool isLoop = false;                                         // 루프 상태인지 여부

        protected virtual void Awake()
        {
            // 자기 밑의 UI 이펙트를 리스트에 넣어줍니다.
            foreach (var effect in effectCanvas.GetComponentsInChildren<UIEffecter>())
            {
                effectList.Add(effect);
            }
        }

        // UI이펙트를 재생합니다.
        public void Play()
        {
            this.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }

        // 이펙트를 재생합니다.
        public virtual void StartEffect()
        {
            effectCanvas.enabled = true;
            isPlaying = true;
        }

        // 이펙트를 종료합니다.
        public virtual void EndEffect()
        {
            // 만약 루프 상태면 리턴
            if (isLoop) return;

            // 플레이를 정지합니다.
            isPlaying = false;
            foreach (var effect in effectList)
            {
                if (effect.isPlaying)
                    return;
            }

            effectCanvas.enabled = false;
        }

        // 루프를 종료시킵니다.
        public virtual void EndLoop()
        {
            isLoop = false;
            EndEffect();
        }
    }

}