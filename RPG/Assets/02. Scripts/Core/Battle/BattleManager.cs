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

/*
 * 전투를 관리하는 매니저 클래스
 */

namespace RPG.Battle.Core
{
    public class BattleManager : MonoBehaviour
    {
        // ORDER : #1) 싱글턴 패턴 사용 예시
        // 싱글톤 클래스
        private static BattleManager instance;
        public static BattleManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogWarning("BattleManager is NULL");
                    return null;
                }

                return instance;
            }
        }
        private static BattleSceneUIManager battleUI;                           // 전투 UI 관리 매니저
        public static BattleSceneUIManager BattleUI
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogWarning("BattleManager is NULL");
                    return null;
                }
                return battleUI;
            }
        }
        private static ObjectPooling objectPool;                                // 오브젝트 풀
        public static ObjectPooling ObjectPool
        {
            get
            {
                if (instance == null)
                {
                    Debug.LogWarning("BattleManager is NULL");
                    return null;
                }
                return objectPool;
            }
        }

        [Header("BattleCore")]
        // Component
        public BattleSceneState currentBattleState = BattleSceneState.Default;  // 현재 전투 상태


        [Header("Controller")]
        public PlayerController livePlayer;                                     // 현재 플레이어
        public List<EnemyController> liveEnemies = new List<EnemyController>(); // 생존한 적군들


        [Header("Stage")]
        public int currentStageFloor = 1;               // 현재 스테이지 층수
        public float battleReadyTime = 2f;              // 전투 시작 시간
        public float playerCreatePositionXOffset = 15f; // 플레이어 생성 x좌표 위치
        public float EnemyCreatePositionXOffset = -18f; // 적 생성 x좌표 위치
        public StageFomation stageFomation;             // 적군 생성 진형

        private StageData stageData;    // 스테이지 데이터

        // Looting
        public int gainEnergy = 0;      // 루팅한 에너지
        public int gainGacha = 0;       // 루팅한 가챠 티켓
        public int gainReinforce = 0;   // 루팅한 강화 티켓
        public int gainIncant = 0;      // 루팅한 인챈트 티켓
        
        private int currentStageGainEnergy = 0;     // 현재층 얻은 에너지
        private int currentStageGainGacha = 0;      // 현재층 얻은 가챠 티켓
        private int currentStageGainReinforce = 0;  // 현재층 얻은 강화 티켓
        private int currentStageGainIncant = 0;     // 현재층 얻은 인챈트 티켓

        private readonly Dictionary<BattleSceneState, UnityEvent> battleEventDic = new Dictionary<BattleSceneState, UnityEvent>();  // 전투 이벤트 버스
        private delegate void voidFunc();   // 함수 호출 지연 사용 델리게이트

        [Header("Test")]
        [SerializeField] bool isTest;   // 테스트 여부

        private void Awake()
        {
            // 싱글톤 패턴
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

            // 오브젝트 풀 UI 세팅
            ObjectPool.SetUp(BattleUI.battleCanvas);
        }

        private void Start()
        {
            if (GameManager.Instance == null || isTest == true)
            {
                return;
            }

            // 현재 층수를 세팅합니다.
            currentStageFloor = GameManager.Instance.choiceStageID;
            AudioManager.Instance.PlayMusic("BattleBackGroundMusic",true);
            // 전투를 준비합니다.
            Ready();
        }

        // 현재 전투 상태를 변경합니다.
        public void SetBattleState(BattleSceneState state)
        {
            this.currentBattleState = state;
            // 전투 상태에 따른 이벤트를 호출합니다.
            Publish(currentBattleState);
        }
        #region 전투 상태



        // 전투를 준비합니다.
        private void Ready()
        {
            // 중단 버튼을 숨겨줍니다.
            BattleUI.InitResultBtn(false);
            // 층 정보를 보여줍니다.
            BattleUI.ShowFloor(currentStageFloor);
            // 현재 스테이지를 불러옵니다.
            LoadCurrentStage();
            // 전투 상태를 변경합니다.
            SetBattleState(BattleSceneState.Ready);
            // 모든 스킬 이펙트를 회수합니다.
            objectPool.ReleaseAllAbility();
            // 전투 시작 시간동안 대기후 전투를 개시합니다.
            StartCoroutine(MethodCallTimer(() =>
            {
                Battle();
            }, battleReadyTime));
        }

        // 전투에서 승리했습니다.
        private void Win()
        {
            if (isTest)
            {
                return;
            }

            if (GameManager.Instance.stageDataDic.ContainsKey(currentStageFloor + 1))
                // 다음층이 존재한다면 
            {
                // 전투 상태를 승리로 변경합니다
                SetBattleState(BattleSceneState.Win);
                // 현재 층수를 변경합니다.
                currentStageFloor++;
                // 플레이어 캐릭터가 중앙을 바라보고 이동하는 애니메이션으로 변경합니다.
                livePlayer.transform.LookAt(livePlayer.transform.position + Vector3.left);
                livePlayer.animator.ResetTrigger("Idle");
                livePlayer.animator.SetTrigger("Move");
                // 플레이어가 전투 준비 시간동안 플레이어 생성 위치로 이동한뒤 전투 준비 상태로 변경합니다.
                livePlayer.transform.DOMoveX(EnemyCreatePositionXOffset, battleReadyTime).OnComplete(() => { Ready(); });
            }
            else
                // 다음 층이 없다면 엔딩을 보여줍니다.
            {
                // 현재 상태를 엔딩으로 변경하고 엔딩음악을 재생합니다.
                SetBattleState(BattleSceneState.Ending);
                AudioManager.Instance.PlayMusic("EndingBackGroundMusic", true);
            }

            // 유저 정보를 업데이트 합니다.
            UpdateUserinfo();
        }

        // 전투에서 패배했습니다.
        private void Defeat()
        {
            if (isTest)
            {
                return;
            }

            // 패배 음악을 재생합니다.
            AudioManager.Instance.PlayMusic("DefeatMusicBackGround", false);
            // 패배 연출
            SetBattleState(BattleSceneState.Defeat);
        }

        // 전투를 시작합니다.
        private void Battle()
        {
            // 전투 상태로 변경합니다.
            SetBattleState(BattleSceneState.Battle);
        }

        // 전투를 중단합니다.
        private void Pause()
        {
            SetBattleState(BattleSceneState.Pause);
        }

        #endregion


        // 현재 층 ID로 스테이지 데이터를 불러옵니다
        private StageData LoadStageData()
        {
            if (GameManager.Instance.stageDataDic.TryGetValue(currentStageFloor, out StageData stage))
            {
                return stage;
            }

            Debug.Log("Stage Data in NULL!");
            return null;
        }

        // 캐릭터가 죽었습니다.
        public void CharacterDead(Controller controller)
        {
            if (controller is PlayerController)
                // 죽은 캐릭터가 플레이어 캐릭터면
            {
                // 패배합니다.
                Defeat();
            }
            else if (controller is EnemyController)
                // 죽은 캐릭터가 적 캐릭터라면
            {
                var enemy = controller as EnemyController;
                // 생존 적에서 죽은 캐릭터를 제거합니다.
                liveEnemies.Remove(enemy);
                // 오브젝트 풀에 죽은 캐릭터를 넣어줍니다.
                ObjectPool.ReturnEnemy((controller as EnemyController));
                if (liveEnemies.Count <= 0)
                    // 생존한 적이 한명도 없다면
                {
                    // 승리합니다.
                    Win();
                }
            }
        }

            // 적이 죽었을 때 아이템을 루팅합니다.
        public void LootingItem(EnemyController enemy)
        {
            EnemyData enemyData;
            if (GameManager.Instance.enemyDataDic.TryGetValue((enemy.battleStatus.status as EnemyStatus).enemyID, out enemyData))
                // 죽은 적 캐릭터의 데이터 ID를 조회하여 루팅 테이블을 가져옵니다.
            {
                // 에너지는 무조건 드랍합니다.
                // 아이템을 드랍할 때 현재 죽은 적의 위치에서 드랍하도록 합니다.
                ObjectPool.GetLootingItem(Camera.main.WorldToScreenPoint(enemy.transform.position), DropItemType.Energy, BattleUI.backpack.transform);
                // 에너지를 획득합니다.
                gainItem(DropItemType.Energy, enemyData.dropEnergy);

                foreach (var dropTable in enemyData.dropitems)
                    // 적데이터의 드랍테이블을 순회합니다.
                {
                    float random = Random.Range(0f, 100f);
                    if (random <= dropTable.percent)
                        // 확률 판정에 성공했다면
                    {
                        // 아이템을 드랍합니다.
                        // 아이템 타입에 맞는 이미지를 보여줍니다.
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

        // 아이템을 획득합니다.
        private void gainItem(DropItemType type, int count)
        {
            // 얻은 아이템 타입에 따라 갯수만큼 획득합니다.
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
        // 유저 정보를 업데이트 합니다.
        private void UpdateUserinfo()
        {
            // 게임매니저의 유저 정보를 참조합니다.
            UserInfo userInfo = GameManager.Instance.UserInfo;
            // 유저의 가장 높이 오른 층이 현재 층보다 낮다면 갱신시킵니다.
            if (userInfo.risingTopCount < currentStageFloor)
            {
                userInfo.risingTopCount = currentStageFloor;
            }

            // 현재 층에서 획득한 아이템들을 유저에 넣어줍니다.
            userInfo.energy += currentStageGainEnergy;
            userInfo.itemGachaTicket += currentStageGainGacha;
            userInfo.itemIncantTicket += currentStageGainIncant;
            userInfo.itemReinforceTicket += currentStageGainReinforce;

            // 획득한 아이템들을 획득한 총아이템에 더해줍니다.
            gainEnergy += currentStageGainEnergy;
            gainGacha += currentStageGainGacha;
            gainIncant += currentStageGainIncant;
            gainReinforce += currentStageGainReinforce;

            // 획득한 아이템들을 초기화시킵니다.
            currentStageGainEnergy = 0;
            currentStageGainGacha = 0;
            currentStageGainIncant = 0;
            currentStageGainReinforce = 0;

        }

        // 현재 스테이지를 초기화합니다.
        private void ResetStage()
        {
            if (livePlayer != null)
            {
                livePlayer.gameObject.SetActive(false);
            }

            // 모든 적을 풀에 넣어줍니다.
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

        // 전투 상태 이벤트들을 호출합니다.
        public void Publish(BattleSceneState state)
        {
            UnityEvent thisEvent;

            if (battleEventDic.TryGetValue(state, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }





        #endregion

        // ORDER : #5) 현재 자신의 위치에서 가장 가까운 컨트롤러를 반환하는 함수
        /// <summary>
        /// 가장 가까운 T를 찾아서 리턴합니다.
        /// </summary>
        /// <typeparam name="T">Controller한정</typeparam>
        public T ReturnNearDistanceController<T>(Transform transform) where T : Controller
        {
            if (typeof(T) == typeof(PlayerController))
            // 찾는 컨트롤러가 플레이어 컨트롤러라면
            {
                if (livePlayer != null)
                {
                    return livePlayer as T;
                }
            }
            else if (typeof(T) == typeof(EnemyController))
                // 찾는 컨트롤러가 에너미컨트롤러 라면
            {
                // 생존한 적리스트를 가져옵니다.
                List<EnemyController> list = liveEnemies.Where(enemy => !enemy.battleStatus.isDead).ToList();
                // 생존한 적이 없다면 null 리턴
                if (list.Count == 0) return null;

                // 리스트를 순회하면서 가장 가까운 적을 찾습니다.
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

                // T 타입으로 형변환 해서 전달 해줍니다.
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