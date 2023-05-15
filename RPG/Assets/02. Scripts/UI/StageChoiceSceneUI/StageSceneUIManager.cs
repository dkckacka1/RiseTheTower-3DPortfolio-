using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RPG.Stage.UI
{
    public class StageSceneUIManager : MonoBehaviour
    {
        [SerializeField] StageInfomationUI ui;

        [SerializeField] TextMeshProUGUI reinforceText;
        [SerializeField] TextMeshProUGUI incantText;
        [SerializeField] TextMeshProUGUI GachaText;
        [SerializeField] TextMeshProUGUI EnergyText;

        private void Start()
        {
            ui.ShowStageInfomation(GameManager.Instance.stageDataDic[1]);
            ShowUserinfo(GameManager.Instance.UserInfo);
        }

        void ShowUserinfo(UserInfo userinfo)
        {
            reinforceText.text = userinfo.itemReinforceTicket.ToString();
            incantText.text = userinfo.itemIncantTicket.ToString();
            GachaText.text = userinfo.itemGachaTicket.ToString();
            EnergyText.text = userinfo.energy.ToString();
        }
    } 
}
