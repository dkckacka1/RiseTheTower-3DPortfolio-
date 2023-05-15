using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public abstract class UIEffecter : MonoBehaviour
    {
        public Canvas effectCanvas;

        public List<UIEffecter> effectList = new List<UIEffecter>();
        public bool isPlaying = false;
        public bool isLoop = false;

        protected virtual void Awake()
        {
            foreach (var effect in effectCanvas.GetComponentsInChildren<UIEffecter>())
            {
                effectList.Add(effect);
            }
        }

        public void Play()
        {
            this.gameObject.SetActive(false);
            this.gameObject.SetActive(true);
        }

        public virtual void StartEffect()
        {
            effectCanvas.enabled = true;
            isPlaying = true;
        }

        public virtual void EndEffect()
        {
            if (isLoop) return;

            isPlaying = false;
            foreach (var effect in effectList)
            {
                if (effect.isPlaying)
                    return;
            }

            effectCanvas.enabled = false;
        }

        public virtual void EndLoop()
        {
            isLoop = false;
            EndEffect();
        }
    }

}