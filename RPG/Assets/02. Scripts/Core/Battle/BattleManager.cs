using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using RPG.Core;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Battle.UI;
using DG.Tweening;
using RPG.Main.Audio;

namespace RPG.Battle.Core
{
    public class BattleManager : MonoBehaviour
    {
        // Singletone
        public static BattleManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log("BattleManager is NULL");
                    return null;
                }

                return instance;
            }
        }
        private static BattleManager instance;
        public static BattleSceneUIManager BattleUI
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log("BattleManager is NULL");
                    return null;
                }
                return battleUI;
            }
        }
        public static ObjectPooling ObjectPool
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log("BattleManager is NULL");
                    return null;
                }
                return objectPool;
            }
        }

        [Header("BattleCore")]
        // Component
        public BattleSceneState currentBattleState = BattleSceneState.Default;
        private static BattleSceneUIManager battleUI;
        private static ObjectPooling objectPool;


        [Header("Controller")]
        public PlayerController livePlayer;
        public List<EnemyController> liveEnemies = new List<EnemyController>();


        [Header("Stage")]
        public int currentStageFloor = 1;
        public float battleReadyTime = 2f;
        public float playerCreatePositionXOffset = 15f;
        public float EnemyCreatePositionXOffset = -18f;
        public StageFomation stageFomation;

        private StageData stageData;

        // Looting
        public int gainEnergy = 0;
        public int gainGacha = 0;
        public int gainReinforce = 0;
        public int gainIncant = 0;
        
        private int currentStageGainEnergy = 0;
        private int currentStageGainGacha = 0;
        private int currentStageGainReinforce = 0;
        private int currentStageGainIncant = 0;

        private readonly Dictionary<BattleSceneState, UnityEvent> battleEventDic = new Dictionary<BattleSceneState, UnityEvent>();
        private delegate void voidFunc();
        private delegate IEnumerator IEnumeratorFunc();

        [Header("Test")]
        [SerializeField] bool isTest;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                battleUI = GetComponentInChildren<BattleSceneUIManager>();
                objectPool = GetComponentInChildren<ObjectPooling>();
            }
            else
            {
                Destroy(this.gameObject);
                return;
            }



            ObjectPool.SetUp(BattleUI.battleCanvas);
        }

        private void Start()
        {
            if (GameManager.Instance == null || isTest == true)
            {
                return;
            }

            currentStageFloor = GameManager.Instance.choiceStageID;
            AudioManager.Instance.PlayMusic("BattleBackGroundMusic",true);
            Ready();
        }

        public void SetBattleState(BattleSceneState state)
        {
            this.currentBattleState = state;
            Publish(currentBattleState);
        }
        #region 전투 상태



        private void Ready()
        {
            BattleUI.InitResultBtn(false);
            BattleUI.ShowFloor(currentStageFloor);
            LoadCurrentStage();
            SetBattleState(BattleSceneState.Ready);
            objectPool.ReleaseAllAbility();
            StartCoroutine(MethodCallTimer(() =>
            {
                Battle();
            }, battleReadyTime));
        }

        private void Win()
        {
            if (isTest)
            {
                return;
            }

            if (GameManager.Instance.stageDataDic.ContainsKey(currentStageFloor + 1))
                // 다음층이 있다면 준비
            {
                SetBattleState(BattleSceneState.Win);
                currentStageFloor++;
                livePlayer.transform.LookAt(livePlayer.transform.position + Vector3.left);
                livePlayer.animator.ResetTrigger("Idle");
                livePlayer.animator.SetTrigger("Move");
                livePlayer.transform.DOMoveX(EnemyCreatePositionXOffset, battleReadyTime).OnComplete(() => { Ready(); });
            }
            else
                // 다음 층이 없다면 엔딩
            {
                SetBattleState(BattleSceneState.Ending);
                AudioManager.Instance.PlayMusic("EndingBackGroundMusic", true);
            }

            UpdateUserinfo();
        }

        private void Defeat()
        {
            if (isTest)
            {
                return;
            }

            AudioManager.Instance.PlayMusic("DefeatMusicBackGround", false);
            // 패배 연출
            SetBattleState(BattleSceneState.Defeat);
        }

        private void Battle()
        {
            SetBattleState(BattleSceneState.Battle);
        }

        private void Pause()
        {
            SetBattleState(BattleSceneState.Pause);
        }

        #endregion


        private StageData LoadStageData()
        {
            if (GameManager.Instance.stageDataDic.TryGetValue(currentStageFloor, out StageData stage))
            {
                return stage;
            }

            Debug.Log("Stage Data in NULL!");
            return null;
        }

        public void CharacterDead(Controller controller)
        {
            if (controller is PlayerController)
            {
                Defeat();
            }
            else if (controller is EnemyController)
            {
                var enemy = controller as EnemyController;
                liveEnemies.Remove(enemy);
                ObjectPool.ReturnEnemy((controller as EnemyController));
                if (liveEnemies.Count <= 0)
                {
                    Win();
                }
            }
        }

        public void LootingItem(EnemyController enemy)
        {
            EnemyData enemyData;
            if (GameManager.Instance.enemyDataDic.TryGetValue((enemy.battleStatus.status as EnemyStatus).enemyID, out enemyData))
            {
                ObjectPool.GetLootingItem(Camera.main.WorldToScreenPoint(enemy.transform.position), DropItemType.Energy, BattleUI.backpack.transform);
                gainItem(DropItemType.Energy, enemyData.dropEnergy);

                foreach (var dropTable in enemyData.dropitems)
                {
                    float random = Random.Range(0f, 100f);
                    if (random <= dropTable.percent)
                    {
                        ObjectPool.GetLootingItem(Camera.main.WorldToScreenPoint(enemy.transform.position), dropTable.itemType, BattleUI.backpack.transform);
                        switch (dropTable.itemType)
                        {
                            case DropItemType.GachaItemScroll:
                                gainItem(DropItemType.GachaItemScroll, 1);
                                break;
                            case DropItemType.reinfoceScroll:
                                gainItem(DropItemType.reinfoceScroll, 1);
                                break;
                            case DropItemType.IncantScroll:
                                gainItem(DropItemType.IncantScroll, 1);
                                break;
                        }
                    }
                }
            }
        }

        private void gainItem(DropItemType type, int count)
        {
            switch (type)
            {
                case DropItemType.Energy:
                    currentStageGainEnergy += count;
                    break;
                case DropItemType.GachaItemScroll:
                    currentStageGainGacha += count;
                    break;
                case DropItemType.reinfoceScroll:
                    currentStageGainReinforce += count;
                    break;
                case DropItemType.IncantScroll:
                    currentStageGainIncant += count;
                    break;
            }
        }
        #region BattleSceneStateEvent







        #endregion
        private void UpdateUserinfo()
        {
            UserInfo userInfo = GameManager.Instance.UserInfo;
            if (userInfo.risingTopCount < currentStageFloor)
            {
                userInfo.risingTopCount = currentStageFloor;
            }

            userInfo.energy += currentStageGainEnergy;
            userInfo.itemGachaTicket += currentStageGainGacha;
            userInfo.itemIncantTicket += currentStageGainIncant;
            userInfo.itemReinforceTicket += currentStageGainReinforce;

            gainEnergy += currentStageGainEnergy;
            gainGacha += currentStageGainGacha;
            gainIncant += currentStageGainIncant;
            gainReinforce += currentStageGainReinforce;

            currentStageGainEnergy = 0;
            currentStageGainGacha = 0;
            currentStageGainIncant = 0;
            currentStageGainReinforce = 0;

        }

        private void ResetStage()
        {
            if (livePlayer != null)
            {
                livePlayer.gameObject.SetActive(false);
            }

            foreach (var enemy in liveEnemies)
            {
                ObjectPool.ReturnEnemy(enemy);
            }

            liveEnemies.Clear();
        }




        #region LoadStage

        private void LoadCurrentStage()
        {
            stageData = LoadStageData();
            SetUpStage(stageData);
        }

        private void SetUpStage(StageData stage)
        {
            // PlayerSetting

            if (livePlayer == null)
            // 플레이어가 없다면 생성
            {
                livePlayer = BattleManager.ObjectPool.CreatePlayer(GameManager.Instance.Player);
                livePlayer.battleStatus.currentState = CombatState.Actable;
            }

            Vector3 playerPosition = new Vector3(playerCreatePositionXOffset, stage.playerSpawnPosition.y, stage.playerSpawnPosition.z);
            (livePlayer.battleStatus.status as PlayerStatus).SetPlayerDefaultStatus(GameManager.Instance.Player);
            livePlayer.battleStatus.UpdateBehaviour();
            livePlayer.transform.position = playerPosition;
            livePlayer.transform.LookAt(livePlayer.transform.position + Vector3.left);
            livePlayer.animator.SetTrigger("Move");
            livePlayer.transform.DOMoveX(stage.playerSpawnPosition.x, battleReadyTime).OnComplete(() =>
            {
                livePlayer.animator.ResetTrigger("Move");
                livePlayer.animator.SetTrigger("Idle");
            });

            // EnemiesSetting
            Fomation fomation = stageFomation.FomationList.Find(temp => temp.fomationEnemyCount == stage.enemyDatas.Length);
            for (int i = 0; i < stage.enemyDatas.Length; i++)
            {
                EnemyData enemyData;
                if (GameManager.Instance.enemyDataDic.TryGetValue(stage.enemyDatas[i], out enemyData))
                {
                    Vector3 enemyPosition = new Vector3(EnemyCreatePositionXOffset, fomation.positions[i].y, fomation.positions[i].z);
                    EnemyController enemy = ObjectPool.GetEnemyController(enemyData, enemyPosition);
                    enemy.transform.LookAt(enemy.transform.position + Vector3.right);
                    enemy.animator.SetTrigger("Move");
                    enemy.transform.DOLocalMoveX(fomation.positions[i].x, battleReadyTime).OnComplete(() =>
                    {
                        enemy.animator.ResetTrigger("Move");
                        enemy.animator.SetTrigger("Idle");
                    });
                    enemy.battleStatus.currentState = CombatState.Actable;
                    liveEnemies.Add(enemy);
                }
            }
        }
        #endregion

        private IEnumerator MethodCallTimer(voidFunc func, float duration)
        {
            yield return new WaitForSeconds(duration);
            func.Invoke();
        }

        #region eventListener

        // 이벤트 구독
        public void SubscribeEvent(BattleSceneState state, UnityAction listener)
        {
            UnityEvent thisEvent;
            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                battleEventDic.Add(state, thisEvent);
            }
        }

        // 이벤트 구독 해제
        public void UnsubscribeEvent(BattleSceneState state, UnityAction unityAction)
        {
            UnityEvent thisEvent;

            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.RemoveListener(unityAction);
            }
        }

        // 이벤트 활성화
        public void Publish(BattleSceneState state)
        {
            UnityEvent thisEvent;

            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }





        #endregion

        /// <summary>
        /// 가장 가까운 T를 찾아서 리턴합니다.
        /// </summary>
        /// <typeparam name="T">Controller한정</typeparam>
        public T ReturnNearDistanceController<T>(Transform transform) where T : Controller
        {
            if (typeof(T) == typeof(PlayerController))
            {
                if (livePlayer != null)
                {
                    return livePlayer as T;
                }
            }
            else if (typeof(T) == typeof(EnemyController))
            {
                List<EnemyController> list = liveEnemies.Where(enemy => !enemy.battleStatus.isDead).ToList();
                if (list.Count == 0) return null;

                Controller nearTarget = list[0];
                float distance = Vector3.Distance(nearTarget.transform.position, transform.position);

                for (int i = 1; i < list.Count; i++)
                {
                    float newDistance = Vector3.Distance(list[i].transform.position, transform.position);

                    if (distance > newDistance)
                    {
                        nearTarget = list[i];
                        distance = newDistance;
                    }
                }

                return (T)nearTarget;
            }

            return null;
        }

        #region ButtonPlugin

        public void SetPause()
        {
            Pause();
        }

        public void ToMainScene()
        {
            ResetStage();
            SceneLoader.LoadMainScene();
        }

        public void ReStartBattle()
        {
            ResetStage();
            SceneLoader.LoadBattleScene(currentStageFloor);
        }

        public void ReturnBattle()
        {
            Battle();
            BattleUI.ReleaseResultUI();
        }
        #endregion
    }

}