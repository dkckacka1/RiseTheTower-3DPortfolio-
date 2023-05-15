using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Battle.Core;

namespace RPG.Core
{
    public static class SceneLoader
    {
        public static void LoadBattleScene(int stageID)
        {
            GameManager.Instance.choiceStageID = stageID;
            SceneManager.LoadScene("BattleScene");
        }

        public static void LoadBattleScene(StageData data)
        {
            GameManager.Instance.choiceStageID = data.ID;
            SceneManager.LoadScene("BattleScene");
        }

        public static void LoadMainScene()
        {
            SceneManager.LoadScene("MainScene");
        }

        public static void LoadStageChoiceScene()
        {
            SceneManager.LoadScene("StageChoiceScene");
        }
    }

}