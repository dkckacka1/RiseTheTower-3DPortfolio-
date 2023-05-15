using RPG.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Main.Audio
{
    public class AudioManager : MonoBehaviour
    {
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

        public float musicVolume = 100f;
        public float soundVolume = 100f;

        [SerializeField] AudioSource musicSource;
        [SerializeField] AudioSource soundSource;

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

        public void PlayMusic(string musicName, bool isLooping)
        {
            if (GameManager.Instance.audioDic.TryGetValue(musicName, out AudioClip audioClip))
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

        public void PlaySound(string soundName)
        {
            if (GameManager.Instance.audioDic.TryGetValue(soundName, out AudioClip audioClip))
            {
                soundSource.clip = audioClip;
                soundSource.Play();
            }
            else
            {
                Debug.Log("soundName is NULL");
            }
        }

        public void PlaySoundOneShot(string soundName)
        {
            if (GameManager.Instance.audioDic.TryGetValue(soundName, out AudioClip audioClip))
            {
                soundSource.PlayOneShot(audioClip);
            }
            else
            {
                Debug.Log(soundName + " is NULL");
            }
        }

        public void ChangeMusicVolume(float value)
        {
            value = Mathf.Clamp(value, 0, 100);

            musicVolume = value;
            musicSource.volume = musicVolume / 100;

            GameManager.Instance.configureData.musicVolume = musicVolume;
        }

        public void ChangeSoundVolume(float value)
        {
            value = Mathf.Clamp(value, 0, 100);

            soundVolume = value;
            soundSource.volume = soundVolume / 100;

            GameManager.Instance.configureData.soundVolume = soundVolume;
        }
    }
}