using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Battle.Core;
using DG.Tweening;

/*
 * 루티아이템을 보여주는 UI 클래스
 */
namespace RPG.Battle.UI
{
    public class LootingItem : MonoBehaviour
    {
        public Transform targetPos;     // 가방 UI의 위치

        [SerializeField] Image rootImage;               // 루팅 아잍메 이미지
        [SerializeField] float minDistance;             // 가방UI 까지 다가갈 최소거리
        [SerializeField] float bouncePointX;            // 바운스할 X위치
        [SerializeField] float bouncePointY;            // 바운스할 Y위치
        [SerializeField] float jumpTime;                // 점프할 시간
        [SerializeField] float moveTime;                // 이동 시간
        [SerializeField] List<LootingImage> lootings;   // 루팅 아이템의 스프라이트

        public float jumpPower; // 점프 파워

        float timer;                // 보간 이동할 타이머
        float timeRate;             // 보간 이동 시간 비율
        bool canMove = false;   // 이동 가능한지 여부

        // 활성화 시 보간 이동 변수를 초기화합니다.
        private void OnEnable()
        {
            timer = 0;
            timeRate = 0;

            // 점프할 위치를 결정합니다.
            Vector3 jumpPosition = new Vector3(transform.position.x + Random.Range(-bouncePointX, bouncePointX), transform.position.y);
            // 점프 위치까지 바운스합니다.
            // 바운스가 끝나면 이동가능하도록 설정합니다
            transform.DOJump(jumpPosition, jumpPower, 3, jumpTime).OnComplete(() => { canMove = true; });
        }

        private void Update()
        {
            if (canMove)
            // 이동 가능하다면
            {
                if (targetPos == null) return;

                // 가방 UI위치까지 보간 이동을 수행합니다.
                MoveLerp(this.transform, transform.position, targetPos.position, moveTime);
                if (Vector3.Distance(transform.position, targetPos.position) < minDistance)
                // 최소거리 까지 다가왔다면
                {
                    //이동 불가 상태로 만들고
                    // 오브젝트 풀에 루팅 아이템을 반환합니다.
                    // 가방 UI의 애니메이션을 플레이합니다.
                    canMove = false;
                    BattleManager.ObjectPool.ReturnLootingItem(this);
                    targetPos.GetComponent<Animation>().Play();
                }
            }
        }

        // 루팅아이템에 가방  UI를 설정합니다.
        public void SetUp(Transform targetPos)
        {
            this.targetPos = targetPos;
        }

        // 루팅아이템 타입에 따라 설정합니다.
        public void Init(Vector3 position, DropItemType type)
        {
            transform.position = position;

            try
            {
                rootImage.sprite = lootings.Find(item => item.type.Equals(type)).sprite;
            }
            catch
            {
                Debug.Log($"찾는 {type}의 이미지가 없습니다.");
            }
        }

        // 보간 이동을 수행합니다.
        private void MoveLerp(Transform transform, Vector3 startPos, Vector3 endPos, float time)
        {
            timeRate = 1.0f / time;
            if (timer < 1.0f)
            {
                timer += Time.deltaTime * timeRate;
                transform.position = Vector3.Lerp(startPos, endPos, timer);
            }
        }
    }

}