using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 게임의 오디오 관련 기능을 수행하는 매니저 클래스입니다.
 */

namespace RPG.Main.Audio
{
    public class AudioManager : MonoBehaviour
    {
        // 싱클톤 클래스 정의
        public static AudioManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    Debug.Log("AudioManager is NULL");
                    return null;
                }

                return instance;
            }
        }
        private static AudioManager instance;

        public float musicVolume = 100f;    // 게임의 음악 볼륨
        public float soundVolume = 100f;    // 게임의 효과음 볼륨

        [SerializeField] AudioSource musicSource;   // 음악 소스
        [SerializeField] AudioSource soundSource;   // 효과음 소스

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }
        }

        // 음악 클립을 재생합니다.
        public void PlayMusic(string musicName, bool isLooping)
        {
            if (GameManager.Instance.audioDic.TryGetValue(musicName, out AudioClip audioClip))
                // 오디오 클립을 불러와 세팅합니다.
            {
                musicSource.clip = audioClip;
                musicSource.loop = isLooping;
                musicSource.Play();
            }
            else
            {
                Debug.Log("musicName is NULL");
            }
        }

        // 효과음 클립을 재생합니다.
        public void PlaySound(string soundName)
        {
            if (GameManager.Instance.audioDic.TryGetValue(soundName, out AudioClip audioClip))
                // 오디오 클립을 불러와 세팅합니다.
            {
                soundSource.clip = audioClip;
                soundSource.Play();
            }
            else
            {
                Debug.Log("soundName is NULL");
            }
        }

        // 효과음을 한번 재생합니다.
        public void PlaySoundOneShot(string soundName)
        {
            if (GameManager.Instance.audioDic.TryGetValue(soundName, out AudioClip audioClip))
                // 오디오 클립을 불러와 재생합니다.
            {
                soundSource.PlayOneShot(audioClip);
            }
        }

        // 음악 볼륨값을 수정합니다.
        public void ChangeMusicVolume(float value)
        {
            value = Mathf.Clamp(value, 0, 100);

            musicVolume = value;
            musicSource.volume = musicVolume / 100;

            GameManager.Instance.configureData.musicVolume = musicVolume;
        }

        // 효과음 볼륨값을 수정합니다.
        public void ChangeSoundVolume(float value)
        {
            value = Mathf.Clamp(value, 0, 100);

            soundVolume = value;
            soundSource.volume = soundVolume / 100;

            GameManager.Instance.configureData.soundVolume = soundVolume;
        }
    }
}