using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using RPG.Battle.Core;
using System.Linq;
using RPG.Core;

/*
 * 스테이지 정보를 표시해주는 UI 클래스
 */

namespace RPG.Stage.UI
{
    public class StageInfomationUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI floorTxt;          // 현재 층 수 텍스트
        [SerializeField] TextMeshProUGUI ConsumeEnergyTxt;  // 소비 에너지 텍스트

        [SerializeField] List<EnemyInfomationUI> enemyInfoList; // 정 정보 UI 리스트

        [SerializeField] Button ChallengeBtn;   // 도전 버튼

        private StageData stageData;// 현재 스테이지 데이터

        // 현재 스테이지 정보를 표시합니다.
        public void ShowStageInfomation(StageData data)
        {
            this.stageData = data;

            floorTxt.text = $"{stageData.ID} 층";
            ConsumeEnergyTxt.text = $"소비 에너지 : {(stageData.ConsumEnergy != 0 ? $"-{stageData.ConsumEnergy}" : "0")}";
            Dictionary<int, int> stageEnemy = new Dictionary<int, int>();

            // 스테이지 데이터에있는 적 데이터를 가져와 UI에 표시해줍니다.
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

            // 현재 유저가 가진 에너지양에 따라 도전 버튼을 활성화합니다.
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

        // 선택한 스테이지로 전투를 개시합니다.
        public void Challenge()
        {
            GameManager.Instance.UserInfo.energy -= this.stageData.ConsumEnergy;
            SceneLoader.LoadBattleScene(this.stageData);
        }

        // 메인 로비씬으로 이동합니다.
        public void ReturnMainMenu()
        {
            SceneLoader.LoadMainScene();
        }
    }

}