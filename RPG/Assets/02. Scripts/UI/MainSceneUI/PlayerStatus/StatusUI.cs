using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace RPG.Main.UI.StatusUI
{
    public class StatusUI : MonoBehaviour
    {
        [SerializeField] UserinfoDescUI userinfoUI;
        [SerializeField] PlayerStatusDescUI statusUI;

        [SerializeField] float scaleAnimDuration = 1f;

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(Vector3.one, scaleAnimDuration).SetEase(Ease.InOutBounce);
        }
    }
}