using RPG.Battle.Core;
using RPG.Core;
using RPG.Main.Audio;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 전투 중단 UI 클래스
 */

namespace RPG.Battle.UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] Slider musicSlider;                    // 음악 슬라이더
        [SerializeField] TextMeshProUGUI musicVolumeValueTxt;   // 음악 볼륨 텍스트
        [SerializeField] Slider soundSlider;                    // 효과음 슬라이더
        [SerializeField] TextMeshProUGUI soundVolumeValueTxt;   // 효과음 볼륨 텍스트

        // 초기화합니다.
        public void Init()
        {
            // 게임매니저의 오디오 매니저에 접속
            this.transform.parent.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            // 볼륨값을 가져옵니다.
            musicSlider.value = AudioManager.Instance.musicVolume;
            soundSlider.value = AudioManager.Instance.soundVolume;
        }

        private void OnDisable()
        {
            GameSLManager.SaveConfigureData(GameManager.Instance.configureData);
        }

        // 음악 볼륨값을 수정합니다.
        public void ChangeMusicVolum()
        {
            musicVolumeValueTxt.text = musicSlider.value.ToString("F0");
            AudioManager.Instance.ChangeMusicVolume(musicSlider.value);
        }

        // 효과음 볼륨값을 수정합니다.
        public void ChangeSoundVolum()
        {
            soundVolumeValueTxt.text = soundSlider.value.ToString("F0");
            AudioManager.Instance.ChangeSoundVolume(soundSlider.value);
        }

        // UI를 숨겨줍니다.
        public void Release()
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        // 전투로 돌아갑니다
        public void ReturnBattle()
        {
            BattleManager.Instance.ReturnBattle();
        }

        // 전투를 중단합니다.
        public void StopBattle()
        {
            SceneLoader.LoadMainScene();
        }
    }
}