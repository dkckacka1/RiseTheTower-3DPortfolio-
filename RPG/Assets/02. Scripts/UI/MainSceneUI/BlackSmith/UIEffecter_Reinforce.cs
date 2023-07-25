using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using UnityEngine.UI;

/*
 * 장비 강화시 UI 이펙트 클래스
 */

namespace RPG.Main.UI.BlackSmith
{
    public class UIEffecter_Reinforce : UIEffecter
    {
        [SerializeField] Image[] flareImages;   // 플레어 이미지

        [SerializeField] float minHeight = 80;  // 플레어의 최대 길이
        [SerializeField] float maxHeight = 120; // 플레어의 최소 길이

        float flareAngle;   // 플레어의 앵글값

        protected override void Awake()
        {
            base.Awake();

            // 각 플레어가 가질 수있는 앵글값을 설정합니다.
            flareAngle = 360 / flareImages.Length;
        }

        private void OnEnable()
        {
            float currentMinAngle = 0f;

            // 각 플레어를 앵글에 맞게 설정합니다.
            // 각 플레어의 길이가 다르도록 설정합니다.
            for (int i = 0; i < flareImages.Length; i++)
            {
                float angle = Random.Range(currentMinAngle, flareAngle * (i + 1));

                flareImages[i].rectTransform.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
                flareImages[i].rectTransform.sizeDelta = new Vector2(flareImages[i].rectTransform.rect.width, Random.Range(minHeight, maxHeight));
                currentMinAngle += flareAngle;
            }
        }
    }

}