using RPG.Battle.Core;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Stage.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class StageFloorUI : MonoBehaviour
    {
        public RectTransform CachedRectTrasnfrom => GetComponent<RectTransform>();
        public Button ShowStageBtn; 

        [Header("UnLockObject")]
        [SerializeField] GameObject unLockObject;
        [SerializeField] TextMeshProUGUI stageFloorText;
        [Header("LockObject")]
        [SerializeField] GameObject lockObject;

        private StageData stageData;

        // 셀에 대응하는 리스트 항목의 인덱스
        public int Index { get; set; }

        //셀의 높이
        public float Height
        {
            get { return CachedRectTrasnfrom.sizeDelta.y; }
            set
            {
                Vector2 sizeDelta = CachedRectTrasnfrom.sizeDelta;
                sizeDelta.y = value;
                CachedRectTrasnfrom.sizeDelta = sizeDelta;
            }
        }

        public void UpdateContent(StageData stageData)
        {
            this.stageData = stageData;
            stageFloorText.text = stageData.ID.ToString() + "층!";

            if (GameManager.Instance.UserInfo.risingTopCount < this.stageData.ID)
            {
                lockObject.SetActive(true);
                unLockObject.SetActive(false);
                ShowStageBtn.interactable = false;
            }
            else
            {
                lockObject.SetActive(false);
                unLockObject.SetActive(true);
                ShowStageBtn.interactable = true;
            }
        }

        // 셀의 위쪽 끝의 위치
        public Vector2 Top
        {
            get
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                return CachedRectTrasnfrom.anchoredPosition + new Vector2(0.0f, corners[1].y); // corner[1] = 좌상단 로컬 좌표
            }
            set
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                CachedRectTrasnfrom.anchoredPosition = value - new Vector2(0.0f, corners[1].y);
            }
        }

        // 셀의 아래쪽 끝의 위치
        public Vector2 Bottom
        {
            get
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                return CachedRectTrasnfrom.anchoredPosition + new Vector2(0.0f, corners[3].y); // corner[3] = 우하단 로컬 좌표
            }
            set
            {
                Vector3[] corners = new Vector3[4];
                CachedRectTrasnfrom.GetLocalCorners(corners);
                CachedRectTrasnfrom.anchoredPosition = value - new Vector2(0.0f, corners[3].y);
            }
        }

        public void ShowStage(StageInfomationUI ui)
        {
            ui.ShowStageInfomation(stageData);
        }
    }
}