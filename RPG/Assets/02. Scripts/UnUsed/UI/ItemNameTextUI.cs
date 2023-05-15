using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.UnUsed
{
    public class ItemNameTextUI : MonoBehaviour
    {
        TextMeshProUGUI txt;

        Color gradientFirstColor;
        Color gradientLastColor;

        float firstColorG;
        float lastColorG;

        bool isFirstG = true;
        bool isLastG = true;

        [SerializeField] float colorSpeed = 0.01f;

        private void Awake()
        {
            txt = GetComponent<TextMeshProUGUI>();
            gradientFirstColor = txt.colorGradient.topLeft;
            gradientLastColor = txt.colorGradient.bottomLeft;
            firstColorG = txt.colorGradient.topLeft.g;
            lastColorG = txt.colorGradient.bottomLeft.g;
        }

        private void Update()
        {
            if (isFirstG == true)
            {
                firstColorG += (colorSpeed * Time.deltaTime);
                if (firstColorG >= 1)
                {
                    isFirstG = false;
                }
            }
            else
            {
                firstColorG -= (colorSpeed * Time.deltaTime);
                if (firstColorG <= 0)
                {
                    isFirstG = true;
                }
            }

            if (isLastG == true)
            {
                lastColorG += (colorSpeed * Time.deltaTime);
                if (lastColorG >= 1)
                {
                    isLastG = false;
                }
            }
            else
            {
                lastColorG -= (colorSpeed * Time.deltaTime);
                if (lastColorG <= 0)
                {
                    isLastG = true;
                }
            }

            var gradient = txt.colorGradient;
            gradient.topLeft = new Color(gradientFirstColor.r, firstColorG, gradientFirstColor.b);
            gradient.bottomLeft = new Color(gradientFirstColor.r, lastColorG, gradientFirstColor.b);

            txt.colorGradient = gradient;
        }
    }

}