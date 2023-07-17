using System;
using System.IO;
using UnityEngine;

/*
 * 게임의 세이브와 로드 관련된 함수를 모아놓은 static 클래스입니다.
 */

namespace RPG.Core
{
    public static class GameSLManager
    {
        public const string saveFileName = @"\Userinfo.json";   // 파일 저장과 불러올떄의 경로

        // 유저 정보를 JSON 파일로 저장합니다.
        public static void SaveToJSON(UserInfo userinfo, string path)
        {
            var json = JsonUtility.ToJson(userinfo, true);

            File.WriteAllText(path, json);
        }

        [Obsolete]
        // 유저 정보를 JSON파일로 저장합니다.
        public static void SaveToJSON(UserInfo userinfo)
        {
            var json = JsonUtility.ToJson(userinfo, true);

            File.WriteAllText(string.Join("/", Application.dataPath, saveFileName), json);
        }

        // 저장 경로에 세이브 파일이 있는지 확인합니다.
        public static bool isSaveFileExist(string path = null)
        {
            if (path == null)
            {
                return File.Exists(string.Join("/", Application.dataPath, saveFileName));
            }
            else
            {
                return File.Exists(path);
            }
        }

        // 유저 정보 JSON파일을 로드합니다.
        public static UserInfo LoadFromJson()
        {
            string json;
            if (!File.Exists(string.Join("/", Application.dataPath, saveFileName)))
            {
                Debug.Log("SaveFile is not Exist");
                return null;
            }
            else
            {
                json = File.ReadAllText(string.Join("/", Application.dataPath, saveFileName));
            }

            UserInfo userinfo = JsonUtility.FromJson<UserInfo>(json);

            return userinfo;
        }

        // 유저 환경설정 데이터를 저장합니다.
        public static void SaveConfigureData(ConfigureData data)
        {
            PlayerPrefs.SetFloat("MusicVolume", data.musicVolume);
            PlayerPrefs.SetFloat("SoundVolume", data.soundVolume);
        }

        // 유저 환경설정 데이터를 불러옵니다.
        public static ConfigureData LoadConfigureData()
        {
            ConfigureData configure = new ConfigureData();


            configure.musicVolume = PlayerPrefs.GetFloat("MusicVolume", 100f);
            configure.soundVolume = PlayerPrefs.GetFloat("SoundVolume", 100f);

            return configure;
        }
    }
}