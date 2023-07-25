using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using TMPro;
using RPG.Main.Audio;
using UnityEngine.UI;

/*
 *  환경설정 창 UI 클래스
 */

namespace RPG.Main.UI
{
    public class ConfigureUI : MonoBehaviour
    {
        [SerializeField] Slider musicSlider;                    // 음악 슬라이더
        [SerializeField] TextMeshProUGUI musicVolumeValueTxt;   // 음악 볼륨 텍스트
        [SerializeField] Slider soundSlider;                    // 효과음 슬라이더
        [SerializeField] TextMeshProUGUI soundVolumeValueTxt;   // 효과음 볼륨 텍스트

        // 창이 활성화 될때 볼륨값을 가져옵니다
        private void OnEnable()
        {
            musicSlider.value = AudioManager.Instance.musicVolume;
            soundSlider.value = AudioManager.Instance.soundVolume;
        }

        // 창이 비활성화 되면 환경설정값을 저장합니다.
        private void OnDisable()
        {
            if (GameManager.Instance == null)
            {
                return;
            }
            GameSLManager.SaveConfigureData(GameManager.Instance.configureData);
        }

        // 음악 볼륨값을 수정합니다.
        public void ChangeMusicVolume()
        {
            musicVolumeValueTxt.text = musicSlider.value.ToString("F0");
            AudioManager.Instance.ChangeMusicVolume(musicSlider.value);
        }

        // 효과음 볼륨값을 수정합니다.
        public void ChangeSoundVoume()
        {
            soundVolumeValueTxt.text = soundSlider.value.ToString("F0");
            AudioManager.Instance.ChangeSoundVolume(soundSlider.value);
        }

        // 게임을 종료합니다
        public void GameExit()
        {
            // 유저 데이터와 환경설정 데이터를 저장합니다.
            GameSLManager.SaveToJSON(GameManager.Instance.UserInfo, Application.dataPath + @"\Userinfo.json");
#if UNITY_EDITOR
            GameSLManager.SaveConfigureData(GameManager.Instance.configureData);
            UnityEditor.EditorApplication.isPlaying = false;
#else
            GameSLManager.SaveConfigureData(GameManager.Instance.configureData);
            Application.Quit();
#endif
        }
    }
}