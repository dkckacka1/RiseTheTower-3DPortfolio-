using RPG.Battle.Core;
using RPG.Core;
using RPG.Main.Audio;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField] Slider musicSlider;
        [SerializeField] TextMeshProUGUI musicVolumeValueTxt;
        [SerializeField] Slider soundSlider;
        [SerializeField] TextMeshProUGUI soundVolumeValueTxt;

        public void Init()
        {
            // 게임매니저의 오디오 매니저에 접속
            this.transform.parent.gameObject.SetActive(true);
        }

        private void OnEnable()
        {
            musicSlider.value = AudioManager.Instance.musicVolume;
            soundSlider.value = AudioManager.Instance.soundVolume;
        }

        private void OnDisable()
        {
            GameSLManager.SaveConfigureData(GameManager.Instance.configureData);
        }

        public void ChangeMusicVolum()
        {
            musicVolumeValueTxt.text = musicSlider.value.ToString("F0");
            AudioManager.Instance.ChangeMusicVolume(musicSlider.value);
        }

        public void ChangeSoundVolum()
        {
            soundVolumeValueTxt.text = soundSlider.value.ToString("F0");
            AudioManager.Instance.ChangeSoundVolume(soundSlider.value);
        }

        public void Release()
        {
            this.transform.parent.gameObject.SetActive(false);
        }

        public void ReturnBattle()
        {
            BattleManager.Instance.ReturnBattle();
        }

        public void StopBattle()
        {
            SceneLoader.LoadMainScene();
        }
    }
}