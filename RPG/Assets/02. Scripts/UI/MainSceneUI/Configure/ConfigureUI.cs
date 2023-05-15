using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;
using TMPro;
using RPG.Main.Audio;
using UnityEngine.UI;

namespace RPG.Main.UI
{
    public class ConfigureUI : MonoBehaviour
    {
        [SerializeField] Slider musicSlider;
        [SerializeField] TextMeshProUGUI musicVolumeValueTxt;
        [SerializeField] Slider soundSlider;
        [SerializeField] TextMeshProUGUI soundVolumeValueTxt;

        private void OnEnable()
        {
            musicSlider.value = AudioManager.Instance.musicVolume;
            soundSlider.value = AudioManager.Instance.soundVolume;
        }

        private void OnDisable()
        {
            if (GameManager.Instance == null)
            {
                return;
            }
            GameSLManager.SaveConfigureData(GameManager.Instance.configureData);
        }

        public void ChangeMusicVolume()
        {
            musicVolumeValueTxt.text = musicSlider.value.ToString("F0");
            AudioManager.Instance.ChangeMusicVolume(musicSlider.value);
        }

        public void ChangeSoundVoume()
        {
            soundVolumeValueTxt.text = soundSlider.value.ToString("F0");
            AudioManager.Instance.ChangeSoundVolume(soundSlider.value);
        }

        public void GameExit()
        {
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