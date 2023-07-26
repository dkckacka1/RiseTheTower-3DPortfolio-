using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 스테이지 씬의 전체적인 UI를 관리해주는 매니저 클래스입니다.
 */

namespace RPG.Stage.UI
{
    public class StageSceneUIManager : MonoBehaviour
    {
        [SerializeField] StageInfomationUI ui;              // 스테이지 정보 UI

        [SerializeField] TextMeshProUGUI reinforceText; // 강화 티켓 텍스트
        [SerializeField] TextMeshProUGUI incantText;    // 인챈트 티켓 텍스트 
        [SerializeField] TextMeshProUGUI GachaText;     // 가챠 티켓 텍스트 
        [SerializeField] TextMeshProUGUI EnergyText;    // 유저 에너지 양

        private void Start()
        {
            // 씬에 들어간뒤 처음으로 보여줄 스테이지정보입니다.
            ui.ShowStageInfomation(GameManager.Instance.stageDataDic[1]);
            ShowUserinfo(GameManager.Instance.UserInfo);
        }

        // 유저 정보를 표시합니다.
        void ShowUserinfo(UserInfo userinfo)
        {
            reinforceText.text = userinfo.itemReinforceTicket.ToString();
            incantText.text = userinfo.itemIncantTicket.ToString();
            GachaText.text = userinfo.itemGachaTicket.ToString();
            EnergyText.text = userinfo.energy.ToString();
        }
    } 
}
