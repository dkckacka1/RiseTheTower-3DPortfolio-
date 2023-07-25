using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using RPG.Character.Equipment;
using RPG.Character.Status;
using RPG.Battle.Core;
using RPG.Battle.Ability;

/*
 * 게임을 전체적으로 관리해주는 매니저 클래스
 */

namespace RPG.Core
{
    public class GameManager : MonoBehaviour
    {
        // 싱글톤 클래스 정의
        private static GameManager instance;
        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    Debug.Log("GameManager is null");
                    return null;
                }

                return instance;
            }
        }

        private UserInfo userInfo;              // 게임의 유저 정보
        [SerializeField] PlayerStatus player;   // 게임 플레이할 캐릭터
        public ConfigureData configureData;     // 게임 환경설정 데이터
        public int choiceStageID;               // 유저가 선택한 스테이지 ID

        // Encapsule
        public UserInfo UserInfo
        {
            get
            {
                if (userInfo == null)
                {
                    Debug.LogError("Userinfo가 NULL 입니다.");
                    return null;
                }
                return userInfo;
            }
            set => userInfo = value;
        }

        public PlayerStatus Player 
        {
            get
            { 
                if(player == null)
                {
                    Debug.LogError("PlayerStatus 가 NULL입니다.");
                    return null;
                }
                return player;
            }
            set => player = value; 
        }

        #region DIC
        public Dictionary<int, StageData> stageDataDic = new Dictionary<int, StageData>();              // 스테이지 정보 Dic
        public Dictionary<int, EnemyData> enemyDataDic = new Dictionary<int, EnemyData>();              // 적 정보 Dic
        public Dictionary<int, EquipmentData> equipmentDataDic = new Dictionary<int, EquipmentData>();  // 장비 아이템 데이터 Dic
        public Dictionary<int, Incant> incantDic = new Dictionary<int, Incant>();                       // 장비 인챈트 Dic
        public Dictionary<int, Ability> abilityPrefabDic = new Dictionary<int, Ability>();              // 스킬 이펙트 Dic
        public Dictionary<string, AudioClip> audioDic = new Dictionary<string, AudioClip>();            // 오디오 Dic
        #endregion

        [Header("TEST")]
        [SerializeField] bool isTest;

        private void Awake()
        {
            // 싱글톤 클래스 작업
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(this.gameObject);
            }

            DontDestroyOnLoad(this.gameObject);

            // 게임 기본 프레임 설정
            Application.targetFrameRate = 60;

            if (isTest)
                // 만약 테스트 중이라면
            {
                // 바로 게임데이터를 로드하고 신규 유저 생성, 캐릭터 생성, 환경설정 데이터까지 새로 만든다.
                DataLoad();

                this.userInfo = CreateUserInfo();
                this.player.SetPlayerStatusFromUserinfo(userInfo);
                this.configureData = new ConfigureData();
            }
        }

        // 모든 데이터를 로드합니다.
        public void DataLoad()
        {
            ResourcesLoader.LoadEquipmentData(ref equipmentDataDic);
            ResourcesLoader.LoadIncant(ref incantDic);
            ResourcesLoader.LoadEnemyData(ref enemyDataDic);
            ResourcesLoader.LoadStageData(ref stageDataDic);
            ResourcesLoader.LoadSkillPrefab(ref abilityPrefabDic);
            ResourcesLoader.LoadAudioData(ref audioDic);
        }

        #region UserInfo
        // 새로운 유저 정보를 생성합니다.
        public UserInfo CreateUserInfo()
        {
            UserInfo userInfo = new UserInfo();
            // 기본 자원을 줍니다.
            userInfo.itemReinforceTicket = 10;
            userInfo.itemIncantTicket = 10;
            userInfo.itemGachaTicket = 10;
            userInfo.risingTopCount = 1;
            userInfo.energy = 0;
            
            // 캐릭터가 기본적으로 착용할 장비 데이터입니다.
            userInfo.lastedWeaponID = 100;
            userInfo.weaponReinforceCount = 0;
            userInfo.weaponPrefixIncantID = -1;
            userInfo.weaponSuffixIncantID = -1;

            userInfo.lastedArmorID = 200;
            userInfo.armorReinforceCount = 0;
            userInfo.armorPrefixIncantID = -1;
            userInfo.armorSuffixIncantID = -1;

            userInfo.lastedHelmetID = 300;
            userInfo.helmetReinforceCount = 0;
            userInfo.helmetPrefixIncantID = -1;
            userInfo.helmetSuffixIncantID = -1;

            userInfo.lastedPantsID = 400;
            userInfo.pantsReinforceCount = 0;
            userInfo.pantsPrefixIncantID = -1;
            userInfo.pantsSuffixIncantID = -1;

            return userInfo;
        }
        #endregion

        // ORDER : 제네릭 형식 제약조건으로 장비아이템 데이터를 가져오는 함수
        // 장비 아이템 데이터를 가져옵니다.
        public bool GetEquipmentData<T>(int id,out T sourceData) where T : EquipmentData
        {
            EquipmentData data;
            if (!equipmentDataDic.TryGetValue(id, out data))
                // 찾는 ID가 없다면
            {
                Debug.LogError("찾는 데이터가 없습니다.");
                sourceData = null;
                return false;
            }

            // 찾은 데이터를 T 로 변환합니다.
            sourceData = data as T;
            if (sourceData == null)
                // 변환 값이 없다면
            {
                Debug.LogError("찾은 데이터가 잘못된 데이터입니다.");
                return false;
            }

            return true;
        }
    }
}