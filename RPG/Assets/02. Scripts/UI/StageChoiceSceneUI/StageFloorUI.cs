using RPG.Battle.Core;
using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 스테이지 스크롤뷰의 각 셀에 표시될 정보 입니다.
 */

namespace RPG.Stage.UI
{
    [RequireComponent(typeof(RectTransform))]
    public class StageFloorUI : MonoBehaviour
    {
        public RectTransform CachedRectTrasnfrom => GetComponent<RectTransform>(); // 현재 렉트 트랜스폼을 참조
        public Button ShowStageBtn;     // 노드의 버튼

        [Header("UnLockObject")]
        [SerializeField] GameObject unLockObject;           // 보여줄 오브젝트
        [SerializeField] TextMeshProUGUI stageFloorText;    // 층 수 텍스트
        [Header("LockObject")]
        [SerializeField] GameObject lockObject; // 잠김 오브젝트

        private StageData stageData;    // 스테이지 데이터

        // 셀에 대응하는 리스트 항목의 인덱스
        public int Index { get; set; }

        //셀의 높이
        public float Height
        {
            get { return CachedRectTrasnfrom.sizeDelta.y; }     // 렉트 트랜스폼의 사이즈 델타의 y수치입니다.
            set
            {
                Vector2 sizeDelta = CachedRectTrasnfrom.sizeDelta;
                sizeDelta.y = value;
                CachedRectTrasnfrom.sizeDelta = sizeDelta;
            }
        }

        // 들어올 스테이지 데이터를 표시해줍니다.
        public void UpdateContent(StageData stageData)
        {
            this.stageData = stageData;
            stageFloorText.text = stageData.ID.ToString() + "층!";

            if (GameManager.Instance.UserInfo.risingTopCount < this.stageData.ID)
                // 유저가 가장 높이 오른 층수와 스테이지 데이터의 층수를 비교해서
                // 셀을 잠궈줄지 데이터를 보여줄지 선택합니다.
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

        // 스테이지 정보창에 현재 스테이지 데이터를 표시합니다.
        public void ShowStage(StageInfomationUI ui)
        {
            ui.ShowStageInfomation(stageData);
        }
    }
}