using RPG.Core;
using RPG.Main.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Start.UI
{
    public class StartSceneUIManager : MonoBehaviour
    {
        public void GameStart()
        {
            StartCoroutine(MainSceneLoad());
        }

        IEnumerator MainSceneLoad()
        {
            // 게임매니저 데이터 로드
            GameManager.Instance.DataLoad();

            // 유저 데이터 로드
            if (GameSLManager.isSaveFileExist())
            {
                GameManager.Instance.UserInfo = GameSLManager.LoadFromJson();
            }
            else
            {
                GameManager.Instance.UserInfo = GameManager.Instance.CreateUserInfo();
            }

            GameManager.Instance.Player.SetPlayerStatusFromUserinfo(GameManager.Instance.UserInfo);

            // 유저 설정값 로드
            GameManager.Instance.configureData = GameSLManager.LoadConfigureData();

            // 유저 설정값 세팅
            AudioManager.Instance.ChangeMusicVolume(GameManager.Instance.configureData.musicVolume);
            AudioManager.Instance.ChangeSoundVolume(GameManager.Instance.configureData.soundVolume);

            yield return null;
            SceneLoader.LoadMainScene();
        }
    }
}