using RPG.Battle.Ability;
using RPG.Battle.Core;
using RPG.Character.Equipment;
using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * 데이터를 불러오는 메서드를 모아놓은 static 클래스 입니다.
 */

namespace RPG.Core
{
    public static class ResourcesLoader
    {
        public const string dataPath = "Data";              // 데이터를 찾는 경로
        public const string prefabPath = "Prefab";          // 프리팹을 찾는 경로
        public const string audioPath = "Audio";            // 오디오 소스를 찾는 경로
        public const string equipmentPath = "Equipment";    // 장비데이터를 찾는 경로
        public const string incantPath = "Incant";          // 인챈트 데이터를 찾는 경로
        public const string enemyPath = "Enemy";            // 적 데이터를 찾는 경로
        public const string stagePath = "Stage";            // 스테이지 데이터를 찾는 경로
        public const string skillPath = "Skill";            // 스킬 데이터를 찾는 경로

        // 장비아이템 데이터를 불러옵니다.
        public static void LoadEquipmentData(ref Dictionary<int, EquipmentData> dic)
        {
            var list = Resources.LoadAll<EquipmentData>(string.Join("/", dataPath, equipmentPath));
            foreach (var data in list)
            {
                dic.Add(data.ID, data);
            }
        }

        [Obsolete]
        public static void LoadEquipmentData(string path, ref Dictionary<int, EquipmentData> dic)
        {
            var list = Resources.LoadAll<EquipmentData>(path);
            foreach (var data in list)
            {
                dic.Add(data.ID, data);
            }
        }

        // 적 데이터를 불러옵니다.
        public static void LoadEnemyData(ref Dictionary<int, EnemyData> dic)
        {
            var enemies = Resources.LoadAll<EnemyData>(string.Join("/", dataPath, enemyPath));
            foreach (var enemy in enemies)
            {
                //Debug.Log(enemy.enemyName + "Loaded");
                dic.Add(enemy.ID, enemy);
            }
        }

        [Obsolete]
        // 적 데이터를 불러옵니다.
        public static void LoadEnemyData(string path, ref Dictionary<int, EnemyData> dic)
        {
            var enemies = Resources.LoadAll<EnemyData>(path);
            foreach (var enemy in enemies)
            {
                //Debug.Log(enemy.enemyName + "Loaded");
                dic.Add(enemy.ID, enemy);
            }
        }

        // 스테이지 데이터를 불러옵니다.
        public static void LoadStageData(ref Dictionary<int, StageData> dic)
        {
            var items = Resources.LoadAll<StageData>(string.Join("/", dataPath, stagePath));
            foreach (var item in items)
            {
                dic.Add(item.ID, item);
            }
        }

        [Obsolete]
        // 스테이지 데이터를 불러옵니다.
        public static void LoadStageData(string path, ref Dictionary<int, StageData> dic)
        {
            var items = Resources.LoadAll<StageData>(path);
            foreach (var item in items)
            {
                dic.Add(item.ID, item);
            }
        }

        // 인챈트 데이터를 로드합니다.
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
                        // 각 인챈트 데이터의 장비타입으로 알맞는 인챈트 클래스를 생성합니다.
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
                    // 만약 인챈트에 스킬이 붙어있다면 알맞는 자식 클래스로 만들어줍니다.
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

        [Obsolete]
        // 인챈트 데이터를 불러옵니다.
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


        // 스킬 이펙트 프리팹을 불러옵니다.
        public static void LoadSkillPrefab(ref Dictionary<int, Ability> dic)
        {
            var skills = Resources.LoadAll<Ability>(string.Join("/", prefabPath, skillPath));
            foreach (var skill in skills)
            {
                dic.Add(skill.abilityID, skill);
            }
        }

        [Obsolete]
        // 스킬 이펙트 프리팹을 불러옵니다.
        public static void LoadSkillPrefab(string path, ref Dictionary<int, Ability> dic)
        {
            var skills = Resources.LoadAll<Ability>(path);
            foreach (var skill in skills)
            {
                dic.Add(skill.abilityID, skill);
            }
        }

        //오디오 소스를 불러옵니다.
        public static void LoadAudioData(ref Dictionary<string, AudioClip> dic)
        {
            var audios = Resources.LoadAll<AudioClip>(audioPath);

            foreach (var audio in audios)
            {
                dic.Add(audio.name, audio);
            }
        }

        [Obsolete]
        //오디오 소스를 불러옵니다.
        public static void LoadAudioData(string path, ref Dictionary<string, AudioClip> dic)
        {
            var audios = Resources.LoadAll<AudioClip>(path);

            foreach (var audio in audios)
            {
                dic.Add(audio.name, audio);
            }
        }
    }
}
