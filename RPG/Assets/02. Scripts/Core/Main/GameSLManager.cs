using System.IO;
using UnityEngine;


namespace RPG.Core
{
    public static class GameSLManager
    {
        public const string saveFileName = @"\Userinfo.json";

        public static void SaveToJSON(UserInfo userinfo, string path)
        {
            var json = JsonUtility.ToJson(userinfo, true);

            File.WriteAllText(path, json);
        }

        public static void SaveToJSON(UserInfo userinfo)
        {
            var json = JsonUtility.ToJson(userinfo, true);

            File.WriteAllText(string.Join("/", Application.dataPath, saveFileName), json);
        }

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

        public static void SaveConfigureData(ConfigureData data)
        {
            PlayerPrefs.SetFloat("MusicVolume", data.musicVolume);
            PlayerPrefs.SetFloat("SoundVolume", data.soundVolume);
        }

        public static ConfigureData LoadConfigureData()
        {
            ConfigureData configure = new ConfigureData();


            configure.musicVolume = PlayerPrefs.GetFloat("MusicVolume", 100f);
            configure.soundVolume = PlayerPrefs.GetFloat("SoundVolume", 100f);

            return configure;
        }
    }
}