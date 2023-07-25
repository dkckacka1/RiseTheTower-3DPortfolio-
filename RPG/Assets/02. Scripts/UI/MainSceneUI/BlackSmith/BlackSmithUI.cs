using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using RPG.Core;
using RPG.Character.Equipment;

/*
 * 장비 강화창 UI 클래스
 */

namespace RPG.Main.UI.BlackSmith
{
    public class BlackSmithUI : MonoBehaviour
    {
        [SerializeField] ItemPopupUI popupUI;       // 현재 아이템 창 UI
        [SerializeField] ItemChoiceUI choiceUI;     // 장비 선택 창 UI

        [SerializeField] TextMeshProUGUI remainIncantText;      // 남은 인챈트 티켓 텍스트
        [SerializeField] TextMeshProUGUI remainReinfoceText;    // 남은 강화 티켓 텍스트
        [SerializeField] TextMeshProUGUI remainGachaText;       // 남은 가챠 티켓 텍스트

        // 창이 활성화 될때 초기화합니다.
        private void OnEnable()
        {
            choiceUI.InitButtonImage();
            // 기본적으로 무기를 선택합니다.
            choiceUI.ChoiceWeapon(popupUI);
            // 인챈트를 세팅합니다.
            popupUI.InitIncant();
            InitRemainText();
        }

        // 남은 티켓 텍스트를 보여줍니다.
        public void InitRemainText()
        {
            remainIncantText.text = GameManager.Instance.UserInfo.itemIncantTicket.ToString();
            remainReinfoceText.text = GameManager.Instance.UserInfo.itemReinforceTicket.ToString();
            remainGachaText.text = GameManager.Instance.UserInfo.itemGachaTicket.ToString();
        }
    } 
}
