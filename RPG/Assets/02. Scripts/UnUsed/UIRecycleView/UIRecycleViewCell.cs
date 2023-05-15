using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIRecycleViewCell<T> : MonoBehaviour
    {
        public RectTransform CachedRectTrasnfrom => GetComponent<RectTransform>();

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

        public abstract void UpdateContent(T itemData);

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
    }

}
