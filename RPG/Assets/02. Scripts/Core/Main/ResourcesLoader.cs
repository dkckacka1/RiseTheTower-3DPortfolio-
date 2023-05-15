using RPG.Battle.Ability;
using RPG.Battle.Core;
using RPG.Character.Equipment;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public static class ResourcesLoader
    {
        public const string dataPath = "Data";
        public const string prefabPath = "Prefab";
        public const string audioPath = "Audio";
        public const string equipmentPath = "Equipment";
        public const string incantPath = "Incant";
        public const string enemyPath = "Enemy";
        public const string stagePath = "Stage";
        public const string skillPath = "Skill";

        public static void LoadEquipmentData(ref Dictionary<int, EquipmentData> dic)
        {
            var list = Resources.LoadAll<EquipmentData>(string.Join("/", dataPath, equipmentPath));
            foreach (var data in list)
            {
                dic.Add(data.ID, data);
            }
        }

        public static void LoadEquipmentData(string path, ref Dictionary<int, EquipmentData> dic)
        {
            var list = Resources.LoadAll<EquipmentData>(path);
            foreach (var data in list)
            {
                dic.Add(data.ID, data);
            }
        }

        public static void LoadEnemyData(ref Dictionary<int, EnemyData> dic)
        {
            var enemies = Resources.LoadAll<EnemyData>(string.Join("/", dataPath, enemyPath));
            foreach (var enemy in enemies)
            {
                //Debug.Log(enemy.enemyName + "Loaded");
                dic.Add(enemy.ID, enemy);
            }
        }

        public static void LoadEnemyData(string path, ref Dictionary<int, EnemyData> dic)
        {
            var enemies = Resources.LoadAll<EnemyData>(path);
            foreach (var enemy in enemies)
            {
                //Debug.Log(enemy.enemyName + "Loaded");
                dic.Add(enemy.ID, enemy);
            }
        }

        public static void LoadStageData(ref Dictionary<int, StageData> dic)
        {
            var items = Resources.LoadAll<StageData>(string.Join("/", dataPath, stagePath));
            foreach (var item in items)
            {
                dic.Add(item.ID, item);
            }
        }

        public static void LoadStageData(string path, ref Dictionary<int, StageData> dic)
        {
            var items = Resources.LoadAll<StageData>(path);
            foreach (var item in items)
            {
                dic.Add(item.ID, item);
            }
        }

        public static void LoadIncant(ref Dictionary<int, Incant> dic)
        {
            var list = Resources.LoadAll<IncantData>(string.Join("/", dataPath, incantPath));

            foreach (var incantData in list)
            {
                if (incantData.isIncantAbility == false)
                    // 스킬이 없다면 그냥 생성
                {
                    Incant instance = null;
                    switch (incantData.itemType)
                    {
                        case EquipmentItemType.Weapon:
                            instance = new WeaponIncant(incantData as WeaponIncantData);
                            break;
                        case EquipmentItemType.Armor:
                            instance = new ArmorIncant(incantData as ArmorIncantData);
                            break;
                        case EquipmentItemType.Pants:
                            instance = new PantsIncant(incantData as PantsIncantData);
                            break;
                        case EquipmentItemType.Helmet:
                            instance = new HelmetIncant(incantData as HelmetIncantData);
                            break;
                    }

                    dic.Add(incantData.ID, instance);
                }
                else
                {
                    // 클래스이름 만들기
                    string class_name = $"RPG.Character.Equipment.{incantData.className}_{incantData.itemType}";
                    // 클래스 이름을 통한 타입 만들기
                    Type incantType = Type.GetType(class_name);

                    // 매개변수가 있는 생성자를 호출해야함
                    // Activator.CreateInstance의 오버로딩 함수를 호출시켜야하기에 objects 변수 만들기
                    object[] objects = { incantData };

                    var incantInstance = Activator.CreateInstance(incantType, objects) as Incant;

                    dic.Add(incantInstance.incantID, incantInstance);
                }
            }
        }

        public static void LoadIncant(string path, ref Dictionary<int, Incant> dic)
        {
            var list = Resources.LoadAll<IncantData>(path);

            foreach (var incant in list)
            {
                // 클래스이름 만들기
                string class_name = $"RPG.Character.Equipment.{incant.className}_{incant.itemType}";
                // 클래스 이름을 통한 타입 만들기
                Type incantType = Type.GetType(class_name);

                // 매개변수가 있는 생성자를 호출해야함
                // Activator.CreateInstance의 오버로딩 함수를 호출시켜야하기에 objects 변수 만들기
                object[] objects = { incant };

                var incantInstance = Activator.CreateInstance(incantType, objects) as Incant;


                dic.Add(incantInstance.incantID, incantInstance);
            }
        }

        public static void LoadSkillPrefab(ref Dictionary<int, Ability> dic)
        {
            var skills = Resources.LoadAll<Ability>(string.Join("/", prefabPath, skillPath));
            foreach (var skill in skills)
            {
                dic.Add(skill.abilityID, skill);
            }
        }

        public static void LoadSkillPrefab(string path, ref Dictionary<int, Ability> dic)
        {
            var skills = Resources.LoadAll<Ability>(path);
            foreach (var skill in skills)
            {
                dic.Add(skill.abilityID, skill);
            }
        }

        public static void LoadAudioData(ref Dictionary<string, AudioClip> dic)
        {
            var audios = Resources.LoadAll<AudioClip>(audioPath);

            foreach (var audio in audios)
            {
                dic.Add(audio.name, audio);
            }
        }

        public static void LoadAudioData(string path, ref Dictionary<string, AudioClip> dic)
        {
            var audios = Resources.LoadAll<AudioClip>(path);

            foreach (var audio in audios)
            {
                dic.Add(audio.name, audio);
            }
        }


        #region UnUsed
        public static void LoadIncant2(ref Dictionary<int, Incant> dic)
        {
            //int id = 1;
            //dic.Add(id, new Sharpness_Weapon(id++));
            //dic.Add(id, new Fast_Weapon(id++));
            //dic.Add(id, new Heavy_Weapon(id++));
            //dic.Add(id, new Stone_Weapon(id++));
            //dic.Add(id, new Hard_Armor(id++));
            //dic.Add(id, new Smooth_Armor(id++));
            //dic.Add(id, new Balanced_Helmet(id++));
            //dic.Add(id, new Spakling_Helmet(id++));
            //dic.Add(id, new Heavy_Pants(id++));
            //dic.Add(id, new Quick_Pants(id++));
            //dic.Add(id, new Regenerative_Armor(id++));
            //dic.Add(id, new Revenge_Armor(id++));

            //dic.Add(id++, new Sharpness_Weapon());
            //dic.Add(id++, new Fast_Weapon());
            //dic.Add(id++, new Heavy_Weapon());
            //dic.Add(id++, new Stone_Weapon());
            //dic.Add(id++, new Hard_Armor());
            //dic.Add(id++, new Smooth_Armor());
            //dic.Add(id++, new Balanced_Helmet());
            //dic.Add(id++, new Spakling_Helmet());
            //dic.Add(id++, new Heavy_Pants());
            //dic.Add(id++, new Quick_Pants());
            //dic.Add(id++, new Regenerative_Armor());
            //dic.Add(id++, new Revenge_Armor());
        }
        #endregion

    }
}
