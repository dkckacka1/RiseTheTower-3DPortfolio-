using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/*
 * 유닛의 스탯창 UI 클래스
 */

namespace RPG.Main.UI.StatusUI
{
    public class StatusUI : MonoBehaviour
    {
        [SerializeField] UserinfoDescUI userinfoUI;     // 유저 정보 UI
        [SerializeField] PlayerStatusDescUI statusUI;   // 캐릭터의 스탯 정보 UI

        [SerializeField] float scaleAnimDuration = 1f;  // 스케일 애니메이션 시간

        private void OnEnable()
        {
            // 스케일 애니메이션을 합니다.
            transform.localScale = Vector3.zero;
            // 예상치보다 조금더 커졌다가 줄어들게 하기위해 Ease를 설정합니다.
            transform.DOScale(Vector3.one, scaleAnimDuration).SetEase(Ease.InOutBounce);
        }
    }
}