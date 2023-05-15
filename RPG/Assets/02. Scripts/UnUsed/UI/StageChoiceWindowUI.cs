using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Core;

namespace RPG.UnUsed
{
    public class StageChoiceWindowUI : MonoBehaviour
    {
        List<StageWindowUI> stageWindows = new List<StageWindowUI>();

        [SerializeField] StageWindowUI StageWindowUIPrefab;
        [SerializeField] Transform ScrollViewContextLayout;

        public void SetUp()
        {
            CreateStageWindow();
        }

        public void Init()
        {
            foreach (var window in stageWindows)
            {
                bool isClear = (window.Data.ID < GameManager.Instance.UserInfo.risingTopCount);
                bool isLast = (window.Data.ID == GameManager.Instance.UserInfo.risingTopCount);
                window.Init(isClear, isLast);
            }
        }

        public void CreateStageWindow()
        {
            var stageList = GameManager.Instance.stageDataDic
                .Select(item => item.Value)
                .ToList();

            foreach (var stage in stageList)
            {
                StageWindowUI ui = Instantiate<StageWindowUI>(this.StageWindowUIPrefab, ScrollViewContextLayout);
                ui.Setup(stage);
                stageWindows.Add(ui);
            }
        }
    }
}