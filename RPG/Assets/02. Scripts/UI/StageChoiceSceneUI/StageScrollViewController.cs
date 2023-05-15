using RPG.Battle.Core;
using RPG.Core;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        private LinkedList<StageFloorUI> cells = new LinkedList<StageFloorUI>(); // 셀 저장 리스트

        private Rect visibleRect; // 리스트 항목을 셀의 형태로 표시하는 범위를 나타내는 사각형

        private Vector2 prevScrollPos; // 바로 전의 스크롤 위치를 저장

        public RectTransform CachedRectTransform => GetComponent<RectTransform>();
        public ScrollRect CachedScrollRect => GetComponent<ScrollRect>();

        public RawImage BackGroundImage;

        [SerializeField] float contentBackGroundSpeed;

        private int nameIndex = 0;

        protected virtual void Start()
        {
            // 복사 원본 셀은 비활성화 해둔다.
            cellBase.SetActive(false);

            // Scroll Rect 컴포넌트의 OnvalueChanged이벤트의 이벤트 리스너를 설정한다.
            CachedScrollRect.onValueChanged.AddListener(OnScrollPosChanged);

            if (GameManager.Instance != null)
            {
                var list = GameManager.Instance.stageDataDic.ToList();
                list.Sort((value1, value2) => { return (value1.Value.ID > value2.Value.ID) ? 1 : -1; });
                foreach (var stageData in list)
                {
                    stageDataList.Add(stageData.Value);
                }

                CachedScrollRect.SetLayoutHorizontal();
            }
            else
            {
                var list = Resources.LoadAll<StageData>("Data/Stage");
                foreach (var stageData in list)
                {
                    stageDataList.Add(stageData);
                }

                CachedScrollRect.SetLayoutHorizontal();
            }

            InitializeTableView();

            CachedScrollRect.verticalNormalizedPosition = 0f;
        }


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

            if (cells.Count < 1)
            {
                // 셀이 하나도 없을 때는 visibleRect의 범위에 들어가는 첫 번째 리스트 항목을 찾아서
                // 그에 대응하는 셀을 작성한다.
                Vector2 cellTop = new Vector2(0.0f, 0.0f);
                for (int i = 0; i < stageDataList.Count; i++)
                {
                    float cellHeight = GetCellHeightAtIndex(i);
                    Vector2 cellBottom = cellTop + new Vector2(0.0f, cellHeight);
                    if ((cellTop.y <= visibleRect.y && cellTop.y >= visibleRect.y - visibleRect.height) ||
                        (cellBottom.y <= visibleRect.y && cellBottom.y >= visibleRect.y - visibleRect.height))
                    {
                        StageFloorUI cell = CreateCellForIndex(i);
                        cell.Bottom = cellTop;
                        break;
                    }

                    cellTop = cellBottom + new Vector2(0.0f, spacingHeight);
                }

                // visibleRect의 범위에 빈 곳이 있으면 셀을 작성한다.
                SetFillVisibleRectWithCells();
            }
            else
            {
                // 이미 셀이 있을 때는 첫 번째 셀 부터 순서대로 대응하는 리스트 항목의
                // 인덱스를 다시 설정하고 위치와 내용을 갱신한다.
                LinkedListNode<StageFloorUI> node = cells.First;
                UpdateCellForIndex(node.Value, node.Value.Index);
                node = node.Next;

                while (node != null)
                {
                    UpdateCellForIndex(node.Value, node.Previous.Value.Index + 1);
                    node.Value.Top = node.Previous.Value.Bottom + new Vector2(0.0f, -spacingHeight);
                    Debug.Log(node.Value.Top);
                    Debug.Log(node.Previous.Value.Bottom);
                }

                // visibleRect의 범위에 빈 곳이 있으면 셀을 작성한다.
                SetFillVisibleRectWithCells();
            }
        }

        /// <summary>
        /// 셀의 높이값을 리턴하는 함수
        /// </summary>
        /// <param name="index">Index.</param>
        /// <returns>The cell Height at Index</returns>
        protected virtual float GetCellHeightAtIndex(int index)
        {
            // 실제 값을 반환하는 처리는 상속한 클래스에서 구현한다.
            // 셀마다 크기가 다를 경우 상속받은 클래스에서 재 구현한다.
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
                contentHeight += GetCellHeightAtIndex(i);

                if (i > 0)
                {
                    contentHeight += spacingHeight;
                }
            }

            // 스크롤할 내용의 높이를 설정한다.
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

            cell.transform.SetParent(cellBase.transform.parent);

            // 셀의 스케일과 크기를 설정한다.
            cell.transform.localScale = scale;
            cell.CachedRectTrasnfrom.sizeDelta = sizeDelta;
            cell.CachedRectTrasnfrom.offsetMin = offsetMin;
            cell.CachedRectTrasnfrom.offsetMax = offsetMax;

            // 지정된 인덱스가 붙은 리스트 항목에 대응하는 셀로 내용을 갱신한다.
            UpdateCellForIndex(cell, index);

            cells.AddLast(cell);

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
            {
                // 셀에 대응하는 리스트 항목이 있다면 셀을 활성화해서 내용을 갱신하고 높이를 설정한다.
                cell.gameObject.SetActive(true);
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
            if (cells.Count < 1)
            {
                return;
            }

            // 표시된 마지막 셀에 대응하는 리스트 항목의 다음 리스트 항목이 있고
            // 또한 그 셀이 visibleRect의 범위에 들어온다면 대응하는 셀을 작성한다.
            StageFloorUI lastCell = cells.Last.Value;
            int nextCellDataIndex = lastCell.Index + 1;
            Vector2 nextCellBottom = lastCell.Top + new Vector2(0.0f, -spacingHeight);

            while (nextCellDataIndex < stageDataList.Count && nextCellBottom.y < visibleRect.y + visibleRect.height)
            {
                StageFloorUI cell = CreateCellForIndex(nextCellDataIndex);
                cell.Bottom = nextCellBottom;

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
            Rect uvRect = BackGroundImage.uvRect;
            uvRect.y = scrollPos.y * (CachedScrollRect.content.rect.height / contentBackGroundSpeed);
            BackGroundImage.uvRect = uvRect;
            UpdateVisibleRect();
            UpdateCells((scrollPos.y < prevScrollPos.y) ? 1 : -1);
            prevScrollPos = scrollPos;
        }

        // 셀을 재사용하여 표시를 갱신하는 함수
        private void UpdateCells(int scrollDirection)
        {
            if (cells.Count < 1)
            {
                return;
            }

            if (scrollDirection > 0)
            {
                // 위로 스크롤하고 있을 때는 visibleRect에 지정된 범위보다 위에 있는 셀을
                // 아래로 향해 순서대로 이동시켜 내용을 갱신한다.
                StageFloorUI lastCell = cells.Last.Value;
                while (lastCell.Bottom.y > -visibleRect.y + visibleRect.height)
                {
                    StageFloorUI firstCell = cells.First.Value;
                    UpdateCellForIndex(lastCell, firstCell.Index - 1);
                    lastCell.Top = firstCell.Bottom + new Vector2(0.0f, -spacingHeight);

                    cells.AddFirst(lastCell);
                    cells.RemoveLast();
                    lastCell = cells.Last.Value;
                }

                // visibleRect에 지정된 범이 안에 빈 곳이 있으면 셀을 작성한다.
                //SetFillVisibleRectWithCells();
            }
            else if (scrollDirection < 0)
            {
                StageFloorUI firstCell = cells.First.Value;
                while (firstCell.Top.y < -visibleRect.y)
                {
                    StageFloorUI lastCell = cells.Last.Value;
                    UpdateCellForIndex(firstCell, lastCell.Index + 1);
                    firstCell.Bottom = lastCell.Top + new Vector2(0.0f, spacingHeight);

                    cells.AddLast(firstCell);
                    cells.RemoveFirst();
                    firstCell = cells.First.Value;
                }
                SetFillVisibleRectWithCells();
            }
        }

    }

}
