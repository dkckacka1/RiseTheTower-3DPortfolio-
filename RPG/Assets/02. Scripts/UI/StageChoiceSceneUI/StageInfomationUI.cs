using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RPG.Battle.Core;
using System.Linq;
using RPG.Core;

namespace RPG.Stage.UI
{
    public class StageInfomationUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI floorTxt;
        [SerializeField] TextMeshProUGUI ConsumeEnergyTxt;

        [SerializeField] List<EnemyInfomationUI> enemyInfoList;

        [SerializeField] Button ChallengeBtn;

        private StageData stageData;

        public void ShowStageInfomation(StageData data)
        {
            this.stageData = data;

            floorTxt.text = $"{stageData.ID} 층";
            ConsumeEnergyTxt.text = $"소비 에너지 : {(stageData.ConsumEnergy != 0 ? $"-{stageData.ConsumEnergy}" : "0")}";
            Dictionary<int, int> stageEnemy = new Dictionary<int, int>();

            foreach (var enemy in data.enemyDatas)
            {
                if (stageEnemy.ContainsKey(enemy))
                {
                    stageEnemy[enemy]++;
                }
                else
                {
                    stageEnemy.Add(enemy, 1);
                }
            }


            var list = stageEnemy.ToList();

            for (int i = 0; i < enemyInfoList.Count; i++)
            {
                if (i >= list.Count)
                {
                    enemyInfoList[i].gameObject.SetActive(false);
                    continue;
                }

                EnemyData enemyinfodata = GameManager.Instance.enemyDataDic[list[i].Key];
                enemyInfoList[i].ShowEnemyInfomation(enemyinfodata, list[i].Value);
                enemyInfoList[i].gameObject.SetActive(true);
            }

            if (GameManager.Instance.UserInfo.energy < stageData.ConsumEnergy)
            {
                ChallengeBtn.GetComponentInChildren<TextMeshProUGUI>().text = "에너지가\n부족해요!";
                ChallengeBtn.interactable = false;
            }
            else
            {
                ChallengeBtn.GetComponentInChildren<TextMeshProUGUI>().text = "도전 하기!";
                ChallengeBtn.interactable = true;
            }
        }

        public void Challenge()
        {
            GameManager.Instance.UserInfo.energy -= this.stageData.ConsumEnergy;
            SceneLoader.LoadBattleScene(this.stageData);
        }

        public void ReturnMainMenu()
        {
            SceneLoader.LoadMainScene();
        }
    }

}