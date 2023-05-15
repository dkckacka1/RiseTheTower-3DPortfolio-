using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Main.UI.Tutorial
{
    public class TutorialUI : MonoBehaviour
    {
        [SerializeField] ScrollRect scrollRect;
        [SerializeField] RectTransform defaultText;

        private int contentChildCount;

        private void Awake()
        {
            contentChildCount = scrollRect.content.transform.childCount;
        }

        private void OnEnable()
        {
            ShowTutorial(defaultText);
        }

        public void ShowTutorial(RectTransform targetTutorial)
        {
            for (int i = 0; i < contentChildCount; i++)
            {
                scrollRect.content.GetChild(i).gameObject.SetActive(false);
            }

            targetTutorial.gameObject.SetActive(true);
            LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform)scrollRect.content.transform);
        }
    }

}