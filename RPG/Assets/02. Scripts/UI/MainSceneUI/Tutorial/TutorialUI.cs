using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * 물음표 버튼을 눌렀을때 나오는 튜토리얼 창 UI 클래스
 */

namespace RPG.Main.UI.Tutorial
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] ScrollRect scrollRect;     // 튜토리얼 창의 스크롤 뷰
        [SerializeField] RectTransform defaultText; // 기본 텍스트

        private int contentChildCount;

        private void Awake()
        {
            contentChildCount = scrollRect.content.transform.childCount;
        }

        // 창이 활성화되면 기본 텍스트를 보여줍니다.
        private void OnEnable()
        {
            ShowTutorial(defaultText);
        }

        // 버튼을 클릭하면 튜토리얼을 보여줍니다.
        public void ShowTutorial(RectTransform targetTutorial)
        {
            // 스크롤 뷰를 세팅합니다
            for (int i = 0; i < contentChildCount; i++)
            {
                scrollRect.content.GetChild(i).gameObject.SetActive(false);
            }

            targetTutorial.gameObject.SetActive(true);
            // 레이아웃을 재구성합니다.
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)scrollRect.content.transform);
        }
    }

}