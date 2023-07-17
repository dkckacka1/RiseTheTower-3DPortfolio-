using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Battle.Core;

/*
 * 씬과 관련된 함수들을 모아놓은 static 클래스입니다.
 */

namespace RPG.Core
{
    public static class SceneLoader
    {
        // 스테이지 ID를 가지고 해당 전투씬을 가져옵니다.
        public static void LoadBattleScene(int stageID)
        {
            GameManager.Instance.choiceStageID = stageID;
            SceneManager.LoadScene("BattleScene");
        }

        // 스테이지 데이터를 가지고 해당 전투씬을 가져옵니다.
        public static void LoadBattleScene(StageData data)
        {
            GameManager.Instance.choiceStageID = data.ID;
            SceneManager.LoadScene("BattleScene");
        }

        // 메인씬을 로드합니다.
        public static void LoadMainScene()
        {
            SceneManager.LoadScene("MainScene");
        }

        // 스테이지 선택 씬을 로드합니다.
        public static void LoadStageChoiceScene()
        {
            SceneManager.LoadScene("StageChoiceScene");
        }
    }

}