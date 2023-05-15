using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG;
using RPG.Core;
using RPG.Battle.Core;
using RPG.Character.Equipment;
using UnityEditor;
using TMPro;
using RPG.Battle.Control;

namespace RPG.Test
{
    public class BattleTester : MonoBehaviour
    {
        [Header("Core")]
        [SerializeField] GameManager gameManager;
        [SerializeField] BattleManager battleManager;

        [Header("TestButton")]
        [SerializeField] bool createButton;
        [SerializeField] bool battleStateButton;
        [SerializeField] bool changeEquipmentButton;

        [Header("SpawnPosition")]
        [SerializeField] Transform playerPos;
        [SerializeField] Transform enemyPosParent;
        [Range(1, 3)]
        [SerializeField] int enemyCreateNum;

        [Header("DefaultPlayerEquipment")]
        [SerializeField] int defaultWeaponID;
        [SerializeField] int defaultArmorId;
        [SerializeField] int defaultHelmetID;
        [SerializeField] int defaultPantsID;

        [Header("DataID")]
        [SerializeField] int stageDataID;
        [SerializeField] int enemyDataID;
        [SerializeField] int equipmentDataID;
        [SerializeField] int incantDataID;

        [Header("InputField")]
        [SerializeField] TMP_InputField inputField;

        private void Start()
        {
            SetDefaultEquipment();
        }

        private void OnGUI()
        {
            if (createButton)
            {
                if (GUI.Button(new Rect(10, 10, 100, 100), "CreatePlayer"))
                {
                    var player = BattleManager.ObjectPool.CreatePlayer(this.gameManager.Player);
                    battleManager.livePlayer = player;
                    player.transform.position = playerPos.position;
                }

                if (GUI.Button(new Rect(10, 110, 100, 100), "CreateEnemy"))
                {
                    List<Transform> enemiesPos = new List<Transform>();
                    for (int i = 0; i < enemyPosParent.childCount; i++)
                    {
                        enemiesPos.Add(enemyPosParent.GetChild(i));
                    }

                    for (int i = 1; i <= enemyCreateNum; i++)
                    {
                        EnemyData data;
                        if (!gameManager.enemyDataDic.TryGetValue(enemyDataID, out data))
                        {
                            Debug.Log("EnemyData is NULL");
                            return;
                        }

                        var enemy = BattleManager.ObjectPool.GetEnemyController(data, enemiesPos[i - 1].position);
                        battleManager.liveEnemies.Add(enemy);
                    }
                }

                if (GUI.Button(new Rect(10, 210, 100, 100), "RemoveAllController"))
                {
                    if (battleManager.livePlayer != null)
                    {
                        Destroy(battleManager.livePlayer.gameObject);
                        battleManager.livePlayer = null;
                    }
                    foreach (var enemy in battleManager.liveEnemies)
                    {
                        BattleManager.ObjectPool.ReturnEnemy(enemy);
                    }
                    battleManager.liveEnemies.Clear();
                }

                if (GUI.Button(new Rect(10, 310, 100, 100), "CreateStage"))
                {
                    StageData data;
                    if (!gameManager.stageDataDic.TryGetValue(stageDataID, out data))
                    {
                        Debug.Log("StageData is NULL");
                        return;
                    }

                    var player = BattleManager.ObjectPool.CreatePlayer(this.gameManager.Player);
                    battleManager.livePlayer = player;
                    player.transform.position = data.playerSpawnPosition;

                    Fomation fomation = BattleManager.Instance.stageFomation.FomationList.Find(temp => temp.fomationEnemyCount == data.enemyDatas.Length);
                    for (int i = 0; i < data.enemyDatas.Length; i++)
                    {
                        EnemyData enemyData;
                        if (GameManager.Instance.enemyDataDic.TryGetValue(data.enemyDatas[i], out enemyData))
                        {
                            EnemyController enemy = BattleManager.ObjectPool.GetEnemyController(enemyData, fomation.positions[i]);
                            enemy.name = $"{enemyData.name} {i}";
                            battleManager.liveEnemies.Add(enemy);
                        }
                    }
                }

                if (GUI.Button(new Rect(10, 410, 100, 100), "CreateStage"))
                {
                    var stageFomation = AssetDatabase.LoadAssetAtPath<StageFomation>("Assets/98. CreateAssets/FomationAsset.asset");
                    if (stageFomation == null)
                    {
                        Debug.Log("없음");
                        stageFomation = ScriptableObject.CreateInstance<StageFomation>();

                        AssetDatabase.CreateAsset(stageFomation, "Assets/98. CreateAssets/FomationAsset.asset");
                        AssetDatabase.Refresh();
                    }

                    List<Transform> enemiesPos = new List<Transform>();
                    for (int i = 0; i < enemyPosParent.childCount; i++)
                    {
                        enemiesPos.Add(enemyPosParent.GetChild(i));
                    }

                    if (stageFomation.FomationList.Find(fomation => fomation.fomationEnemyCount == enemiesPos.Count) != null)
                    {
                        Debug.Log("이미 에너미 숫자에 맞는 포메이션이 존재합니다.");
                        return;
                    }

                    Fomation newFomation = new Fomation();
                    newFomation.fomationName = inputField.text;
                    newFomation.fomationEnemyCount = enemiesPos.Count;
                    foreach (var pos in enemiesPos)
                    {
                        newFomation.positions.Add(pos.localPosition);
                    }
                    stageFomation.FomationList.Add(newFomation);

                }
            }

            if (battleStateButton)
            {
                if (GUI.Button(new Rect(110, 10, 100, 100), "Battle"))
                {
                    battleManager.SetBattleState(BattleSceneState.Battle);
                }

                if (GUI.Button(new Rect(110, 110, 100, 100), "Pause"))
                {
                    battleManager.SetBattleState(BattleSceneState.Pause);
                }
            }

            if (changeEquipmentButton)
            {
                if (GUI.Button(new Rect(210, 10, 100, 100), "ChangeEquipment"))
                {
                    EquipmentData data;
                    if (!gameManager.equipmentDataDic.TryGetValue(equipmentDataID, out data))
                    {
                        Debug.Log("EquipmentData is NULL");
                        return;
                    }

                    Debug.Log($@"아이템 ID : {data.ID}
아이템 이름 : {data.EquipmentName}
아이템 타입 : {data.equipmentType}
레어도 : {data.equipmentTier}");

                    switch (data.equipmentType)
                    {
                        case EquipmentItemType.Weapon:
                            gameManager.Player.currentWeapon.ChangeData(data);
                            break;
                        case EquipmentItemType.Armor:
                            gameManager.Player.currentArmor.ChangeData(data);
                            break;
                        case EquipmentItemType.Pants:
                            gameManager.Player.currentPants.ChangeData(data);
                            break;
                        case EquipmentItemType.Helmet:
                            gameManager.Player.currentHelmet.ChangeData(data);
                            break;
                    }

                    gameManager.Player.SetEquipment();
                }

                if (GUI.Button(new Rect(210, 110, 100, 100), "ChangeIncant"))
                {
                    Incant incant;
                    if (!gameManager.incantDic.TryGetValue(incantDataID, out incant))
                    {
                        Debug.Log("IncantData is NULL");
                        return;
                    }

                    Debug.Log($@"인챈트 ID : {incant.incantID}
인챈트 이름 : {incant.incantName}
인챈트 스킬 설명 : {incant.abilityDesc}");

                    switch (incant.itemType)
                    {
                        case EquipmentItemType.Weapon:
                            gameManager.Player.currentWeapon.Incant(incant);
                            break;
                        case EquipmentItemType.Armor:
                            gameManager.Player.currentArmor.Incant(incant);
                            break;
                        case EquipmentItemType.Pants:
                            gameManager.Player.currentPants.Incant(incant);
                            break;
                        case EquipmentItemType.Helmet:
                            gameManager.Player.currentHelmet.Incant(incant);
                            break;
                    }

                    gameManager.Player.SetEquipment();
                }
            }
        }

        private void SetDefaultEquipment()
        {
            EquipmentData data;
            if (!gameManager.equipmentDataDic.TryGetValue(defaultWeaponID, out data))
            {
                Debug.Log("EquipmentData is NULL");
                return;
            }
            else
            {
                if ((data as WeaponData) != null)
                {
                    gameManager.Player.currentWeapon.ChangeData(data);
                }
                else
                {
                    Debug.Log("EquipmentData is Not WeaponData");
                }
            }

            if (!gameManager.equipmentDataDic.TryGetValue(defaultArmorId, out data))
            {
                Debug.Log("EquipmentData is NULL");
                return;
            }
            else
            {
                if ((data as ArmorData) != null)
                {
                    gameManager.Player.currentArmor.ChangeData(data);
                }
                else
                {
                    Debug.Log("EquipmentData is Not ArmorData");
                }
            }

            if (!gameManager.equipmentDataDic.TryGetValue(defaultHelmetID, out data))
            {
                Debug.Log("EquipmentData is NULL");
                return;
            }
            else
            {
                if ((data as HelmetData) != null)
                {
                    gameManager.Player.currentHelmet.ChangeData(data);
                }
                else
                {
                    Debug.Log("EquipmentData is Not HelmetDataID");
                }
            }

            if (!gameManager.equipmentDataDic.TryGetValue(defaultPantsID, out data))
            {
                Debug.Log("EquipmentData is NULL");
                return;
            }
            else
            {
                if ((data as PantsData) != null)
                {
                    gameManager.Player.currentPants.ChangeData(data);
                }
                else
                {
                    Debug.Log("EquipmentData is Not PantsID");
                }
            }

            gameManager.Player.SetEquipment();


        }
    }
}