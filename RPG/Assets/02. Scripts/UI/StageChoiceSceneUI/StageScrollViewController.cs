using RPG.Battle.Core;
using RPG.Core;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ORDER : #8) 스테이지 선택 씬에서 무한 스크롤링 UI 구현
// 참고 URL : https://wonjuri.tistory.com/entry/Unity-UI-%EC%9E%AC%EC%82%AC%EC%9A%A9-%EC%8A%A4%ED%81%AC%EB%A1%A4%EB%B7%B0-%EC%A0%9C%EC%9E%91
// 포트폴리오의 스크롤뷰는 밑에서부터 위로 올라가기 때문에 참고한 URL과 구성이 다릅니다.
/*
 * 무한 스크롤뷰 UI 클래스입니다
 */


namespace RPG.Stage.UI
{
    [RequireComponent(typeof(ScrollRect))]
    [RequireComponent(typeof(RectTransform))]
    public class StageScrollViewController : MonoBehaviour
    {
        protected List<StageData> stageDataList = new List<StageData>(); // 리스트 항목의 데이터를 저장
        [SerializeField]
        protected GameObject cellBase = null; // 복사 원본 셀
        [SerializeField]
        private float spacingHeight = 4.0f; // 각 셀의 간격

        private LinkedList<StageFloorUI> cellList = new LinkedList<StageFloorUI>(); // 셀 저장 리스트

        private Rect visibleRect; // 리스트 항목을 셀의 형태로 표시하는 범위를 나타내는 사각형

        private Vector2 prevScrollPos; // 바로 전의 스크롤 위치를 저장

        public RectTransform CachedRectTransform => GetComponent<RectTransform>();  // 렉트 트랜스폼 참조
        public ScrollRect CachedScrollRect => GetComponent<ScrollRect>();   // 스크롤 렉트 참조

        public RawImage BackGroundImage;    // 현재 백그라운드 이미지

        [SerializeField] float contentBackGroundSpeed;  // 스크롤함에 따라 이동할 백그라운드 이미지 속도

        private int nameIndex = 0;

        protected virtual void Start()
        {
            // 복사 원본 셀은 비활성화 해둔다.
            cellBase.SetActive(false);

            // Scroll Rect 컴포넌트의 OnvalueChanged이벤트의 이벤트 리스너를 설정한다.
            CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);

            // 게임 매니저로부터 스테이지 정보 리스트를 받아옵니다.
            if (GameManager.Instance != null)
            {
                var list = GameManager.Instance.stageDataDic.ToList();
                // 가장 낮은 층이 가장 밑에 있도록 정렬해줍니다.
                list.Sort((value1, value2) => { return (value1.Value.ID > value2.Value.ID) ? 1 : -1; });
                foreach (var stageData in list)
                {
                    stageDataList.Add(stageData.Value);
                }

                CachedScrollRect.SetLayoutHorizontal();
            }

            // 스크롤 뷰를 초기화합니다.
            InitializeTableView();

            // 스테이지씬이 로드될때 1층을 보여줄 수 있도록 한다.
            CachedScrollRect.verticalNormalizedPosition = 0f;
        }


        // 디버그용 기즈모
        private void OnDrawGizmosSelected()
        {
            Vector3[] corners = new Vector3[4];
            corners[0].x = visibleRect.x;
            corners[0].y = visibleRect.y;

            corners[1].x = visibleRect.x;
            corners[1].y = visibleRect.y + visibleRect.height;

            corners[2].x = visibleRect.xMax;
            corners[2].y = visibleRect.y + visibleRect.height;

            corners[3].x = visibleRect.xMax;
            corners[3].y = visibleRect.y;


            Gizmos.color = Color.red;
            Gizmos.DrawSphere(corners[0], 100f);
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(corners[1], 100f);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(corners[2], 100f);
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(corners[3], 100f);
        }

        /// <summary>
        /// 테이블 뷰를 초기화 하는 함수
        /// </summary>
        protected void InitializeTableView()
        {
            UpdateScrollViewSize(); // 스크롤할 내용의 크기를 갱신한다.
            UpdateVisibleRect(); // visibleRect를 갱신한다.

            if (cellList.Count < 1)
                // 셀이 하나도 없을 경우
                // 기준이 될 셀을 하나 작성해야합니다.
            {
                Vector2 cellBottom = new Vector2(0.0f, 0.0f); // 현재 만들 셀의 바닥 위치

                for (int i = 0; i < stageDataList.Count; i++)
                {
                    float cellHeight = GetCellHeightAtIndex(i); // 만들 셀의 높이
                    Vector2 cellTop = cellBottom + new Vector2(0.0f, cellHeight);   // 만들 셀의 꼭대기 위치 (셀의 바닥에서 부터 높이만큼 더하면 꼭대기 값이 된다.)
                    if ((cellBottom.y <= visibleRect.y && cellBottom.y >= visibleRect.y - visibleRect.height) ||
                        (cellTop.y <= visibleRect.y && cellTop.y >= visibleRect.y - visibleRect.height))
                        // 만들어아햘 셀의 렉트값이 보여줄 범위 내에 있을경우
                    {
                        // 셀을 생성합니다.
                        StageFloorUI cell = CreateCellForIndex(i);
                        // 셀의 위치를 보여줄 범위 내에 있도록 수정합니다.
                        cell.Bottom = cellBottom;
                        break;
                    }

                    // 렉트 값이 보여줄 범위 내에 있을 때 까지 셀 생성 위치를 수정합니다.
                    cellBottom = cellTop + new Vector2(0.0f, spacingHeight);
                }

                // visibleRect의 범위에 빈 곳이 있으면 셀을 작성한다.
                SetFillVisibleRectWithCells();
            }
            else
            {
                // 이미 셀이 있을 때는 첫 번째 셀 부터 순서대로 대응하는 리스트 항목의
                // 인덱스를 다시 설정하고 위치와 내용을 갱신한다.
                LinkedListNode<StageFloorUI> node = cellList.First;
                UpdateCellForIndex(node.Value, node.Value.Index);

                node = node.Next;
                while (node != null)
                    // 다음 노드가 없을때까지 반복합니다.
                {
                    UpdateCellForIndex(node.Value, node.Previous.Value.Index + 1);
                    node.Value.Top = node.Previous.Value.Bottom + new Vector2(0.0f, -spacingHeight);
                }

                // visibleRect의 범위에 빈 곳이 있으면 셀을 작성한다.
                SetFillVisibleRectWithCells();
            }
        }

        /// <summary>
        /// 셀의 높이값을 리턴하는 함수
        /// </summary>
        protected virtual float GetCellHeightAtIndex(int index)
        {
            // 실제 값을 반환하는 처리는 상속한 클래스에서 구현합니다.
            // 셀마다 크기가 다를 경우 상속받은 클래스에서 재 구현합니다.
            // 다만 나는 셀마다 크기가 동일하므로 기본 렉트 트랜스폼에서 높이를 반환합니다.
            return cellBase.GetComponent<RectTransform>().sizeDelta.y;
        }

        /// <summary>
        /// 스크롤할 내용 전체의 높이를 갱신하는 함수
        /// </summary>
        protected void UpdateScrollViewSize()
        {
            float contentHeight = 0.0f;
            for (int i = 0; i < stageDataList.Count; i++)
            {
                // 전체 스테이지 데이터만큼 셀 노드의높이를 더해줍니다.
                contentHeight += GetCellHeightAtIndex(i);

                if (i > 0)
                {
                    // 만약 셀 사이의 간격이 있다면 추가로 더해줍니다.
                    contentHeight += spacingHeight;
                }
            }

            // 스크롤할 내용의 높이를 설정합니다.
            Vector2 sizeDelta = CachedScrollRect.content.sizeDelta;
            sizeDelta.y = contentHeight;
            CachedScrollRect.content.sizeDelta = sizeDelta;
        }

        /// <summary>
        /// 셀을 생성하는 함수
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>The cell ofr index.</returns>
        private StageFloorUI CreateCellForIndex(int index)
        {
            // 복사 원본 셀을 이용해 새로운 셀을 생성한다.
            GameObject obj = Instantiate(cellBase) as GameObject;
            obj.name = "StageFloor " + nameIndex++;
            obj.SetActive(false);
            StageFloorUI cell = obj.GetComponent<StageFloorUI>();

            // 부모 요소를 바꾸면 스케일이나 크기를 잃어버리므로 변수에 저장해둔다.
            Vector3 scale = cell.transform.localScale;
            Vector2 sizeDelta = cell.CachedRectTrasnfrom.sizeDelta;
            Vector2 offsetMin = cell.CachedRectTrasnfrom.offsetMin;
            Vector2 offsetMax = cell.CachedRectTrasnfrom.offsetMax;

            // 셀이 스크롤뷰 컨텐트의 자식오브젝트로 설정합니다.
            cell.transform.SetParent(cellBase.transform.parent);

            // 셀의 스케일과 크기를 설정한다.
            cell.transform.localScale = scale;
            cell.CachedRectTrasnfrom.sizeDelta = sizeDelta;
            cell.CachedRectTrasnfrom.offsetMin = offsetMin;
            cell.CachedRectTrasnfrom.offsetMax = offsetMax;

            // 지정된 인덱스가 붙은 리스트 항목에 대응하는 셀로 내용을 갱신한다.
            UpdateCellForIndex(cell, index);

            // 생성한 셀을 셀리스트에 넣어줍니다.
            cellList.AddLast(cell);

            return cell;
        }

        /// <summary>
        /// 샐의 내용을 갱신하는 함수
        /// </summary>
        /// <param name="cell">Cell.</param>
        /// <param name="index">Index.</param>
        private void UpdateCellForIndex(StageFloorUI cell, int index)
        {
            // 셀에 대응하는 리스트 항목의 인덱스를 설정한다.
            cell.Index = index;

            if (cell.Index >= 0 && cell.Index <= stageDataList.Count - 1)
            // 셀에 대응하는 리스트 항목이 있다면 셀을 활성화해서 내용을 갱신하고 높이를 설정한다.
            {
                cell.gameObject.SetActive(true);
                // 셀에 스테이지 데이터를 넘겨줍니다.
                cell.UpdateContent(stageDataList[cell.Index]);
                cell.Height = GetCellHeightAtIndex(cell.Index);
            }
            else
            {
                // 셀에 대응하는 리스트 항목이 없다면 셀을 비활성화 시켜 표시되지 않게 한다.
                cell.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// VisibleRect를 갱신하기 위한 함수
        /// </summary>
        private void UpdateVisibleRect()
        {
            // visibleRect의 위치는 스크롤할 내용의 기준으로부터 상대적인 위치다.
            visibleRect.x = CachedScrollRect.content.anchoredPosition.x + CachedRectTransform.rect.width;
            visibleRect.y = CachedScrollRect.content.anchoredPosition.y;

            visibleRect.width = CachedRectTransform.rect.width;
            visibleRect.height = CachedRectTransform.rect.height;
        }


        /// <summary>
        /// VisibleRect 범위에 표시될 만큼의 셀을 생성하여 배치하는 함수
        /// </summary>
        private void SetFillVisibleRectWithCells()
        {
            if (cellList.Count < 1)
                // 셀리스트가 비었다면 리턴
            {
                return;
            }

            // 표시된 마지막 셀에 대응하는 리스트 항목의 다음 리스트 항목이 있고
            // 또한 그 셀이 visibleRect의 범위에 들어온다면 대응하는 셀을 작성한다.
            StageFloorUI lastCell = cellList.Last.Value; // 연결리스트 꼬리 셀을 참조합니다.
            int nextCellDataIndex = lastCell.Index + 1;
            Vector2 nextCellBottom = lastCell.Top + new Vector2(0.0f, -spacingHeight);  // 다음 셀의 바닥 값 = 마지막 셀의 높이 값 + 높이 간격

            while (nextCellDataIndex < stageDataList.Count && nextCellBottom.y < visibleRect.y + visibleRect.height)
                // 만들 데이터가 존재하며, 다음셀의 바닥이 보여줄 범위 내에 존재한다면
            {
                // 셀을 생성합니다.
                StageFloorUI cell = CreateCellForIndex(nextCellDataIndex);
                // 만든 셀의 위치를 조정합니다.
                cell.Bottom = nextCellBottom;

                // 다음셀을 만들 수 있도록 세팅합니다.
                lastCell = cell;
                nextCellDataIndex = lastCell.Index + 1;
                nextCellBottom = lastCell.Top + new Vector2(0.0f, -spacingHeight);
            }
        }


        /// <summary>
        /// 스크롤뷰가 움직였을때 호출되는 함수
        /// </summary>
        /// <param name="scrollPos">Scroll position.</param>
        private void OnScrollPosChanged(Vector2 scrollPos)
        {
            // 스테이지 스크롤 뷰가 움직였다면 움직인 위치에 따라 백그라운드 이미지의 UV값을 수정합니다.
            // UV값을 수정해서 백그라운드가 연속되도록 만듭니다.
            Rect uvRect = BackGroundImage.uvRect;
            uvRect.y = scrollPos.y * (CachedScrollRect.content.rect.height / contentBackGroundSpeed);
            BackGroundImage.uvRect = uvRect;

            // 보여줄 범위를 갱신합니다.
            UpdateVisibleRect();

            // 스크롤뷰가 아래로 움직였는지 위로 움직였는지 체크해서 셀을 업데이트합니다.
            UpdateCells((scrollPos.y < prevScrollPos.y) ? 1 : -1);
            prevScrollPos = scrollPos;
        }

        // 셀을 재사용하여 표시를 갱신하는 함수
        private void UpdateCells(int scrollDirection)
        {
            if (cellList.Count < 1)
                // 셀리스트가 비어있다면 리턴
            {
                return;
            }

            if (scrollDirection > 0)
                // 위로 스크롤하고 있을 때는 보여줄 범위보다 위에 있는 셀을
                // 아래로 향해 순서대로 이동시켜 내용을 갱신한다.
            {
                StageFloorUI lastCell = cellList.Last.Value;
                while (lastCell.Bottom.y > -visibleRect.y + visibleRect.height)
                    // 마지막 셀의 바닥값이 보여줄 범위의 꼭대기 지점을 지나쳤다면
                {
                    // 마지막 셀에 첫번째 셀이 가지고 있던 정보의 전 정보를 넣어줍니다.
                    StageFloorUI firstCell = cellList.First.Value;
                    UpdateCellForIndex(lastCell, firstCell.Index - 1);

                    // 마지막 셀의 꼭대기가 첫번째 셀의 바닥에 위치할수 있도록 위치를 조정합니다.
                    lastCell.Top = firstCell.Bottom + new Vector2(0.0f, -spacingHeight);

                    // 마지막셀을 연결리스트의 첫번째 노드로 만듭니다.
                    cellList.AddFirst(lastCell);
                    cellList.RemoveLast();

                    // 연결리스트의 구조가 변경되었으니 마지막셀을 갱신합니다.
                    lastCell = cellList.Last.Value;
                }

            }
            else if (scrollDirection < 0)
                // 아래로 스크롤하고 있을 때는 보여줄 범위보다 밑에 있는 셀을
                // 위로 향해 순서대로 이동시켜 내용을 갱신합니다.
            {
                StageFloorUI firstCell = cellList.First.Value;
                while (firstCell.Top.y < -visibleRect.y)
                    // 첫번째 셀의 꼭대기 값이 보여줄 범위의 바닥 지점을 지나쳤다면
                {
                    //첫번째 셀에 마지막 셀이 가지고 있던 정보의 다음 정보를 넣어줍니다.
                    StageFloorUI lastCell = cellList.Last.Value;
                    UpdateCellForIndex(firstCell, lastCell.Index + 1);

                    // 첫번째 셀의 바닥값이 마지막셀의 꼭대기가 될 수 있도록 위치를 조정합니다.
                    firstCell.Bottom = lastCell.Top + new Vector2(0.0f, spacingHeight);

                    // 첫번째 셀을 연결리스트의 마지막 노드로 만듭니다.
                    cellList.AddLast(firstCell);
                    cellList.RemoveFirst();

                    // 연결리스트의 구조가 변경되었으니 첫번째 셀을 갱신합니다.
                    firstCell = cellList.First.Value;
                }

                // 만약 스크롤 뷰를 움직이면서 빈 공간이 나타날 수 있으므로 빈공간을 채워줍니다.
                SetFillVisibleRectWithCells();
            }
        }

    }

}
