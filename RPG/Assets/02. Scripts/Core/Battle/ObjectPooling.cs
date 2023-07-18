using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;
using RPG.Battle.Control;
using RPG.Character.Status;
using UnityEditor;
using RPG.Battle.Ability;
using RPG.Character.Equipment;
using RPG.Core;
using UnityEngine.Events;

/*
 * 전투 시 사용될 오브젝트 풀 시스템 클래스
 */

namespace RPG.Battle.Core
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] PlayerController playerController;     // 플레이어 캐릭터 프리팹
        [SerializeField] EnemyController enemyController;       // 적 캐릭터 프리팹
        [SerializeField] GameObject battleTextPrefab;           // 전투 텍스트 프리팹
        [SerializeField] LootingItem lootingItem;               // 루팅아이템 프리팹
        [SerializeField] Transform playerParent;                // 플레이어 캐릭터 부모 오브젝트
        [SerializeField] Transform enemyParent;                 // 적 캐릭터 부모 오브젝트
        [SerializeField] Transform battleTextParent;            // 전투 텍스트 부모 오브젝트
        [SerializeField] Transform LootingItemParent;           // 루팅 아이템 부모 오브젝트
        [SerializeField] Transform abilityParent;               // 스킬 이펙트 부모 오브젝트

        Canvas battleCanvas;    // 전투 UI 캔버스

        // 전투 UI 캔버스를 세팅합니다.
        public void SetUp(Canvas canvas)
        {
            this.battleCanvas = canvas;
        }

        // 플레이어 캐릭터를 생성합니다.
        public PlayerController CreatePlayer(PlayerStatus status)
        {
            // 플레이어 캐릭터를 생성합니다.
            PlayerController controller = Instantiate<PlayerController>(playerController, playerParent);
            // 플레이어 스텟을 세팅해줍니다.
            var controllerStatus = (controller.battleStatus.status as PlayerStatus);
            controllerStatus.SetPlayerStatusFromStatus(status, controller.GetComponentInChildren<CharacterAppearance>());
            controller.gameObject.SetActive(true);

            return controller;
        }

        #region Enemy
        Queue<EnemyController> enemyControllerPool = new Queue<EnemyController>(); // 적 캐릭터 오브젝트 풀

        // 적 캐릭터를 생성합니다.
        private EnemyController CreateController()
        {
            EnemyController enemy = Instantiate<EnemyController>(enemyController, enemyParent);
            return enemy;
        }
        
        // 적 캐릭터를 오브젝트 풀에서 가져옵니다.
        public EnemyController GetEnemyController(EnemyData data, Vector3 position)
        {
            EnemyController enemy;

            if (enemyControllerPool.Count > 0)
            {
                // 풀에 남아있다면 남아있는 컨트롤러 재활용
                enemy = enemyControllerPool.Dequeue();
            }
            else
            {
                // 풀이 비어있다면 생성
                enemy = CreateController();
            }

            // 적 캐릭터의 데이터 변경
            (enemy.battleStatus.status as EnemyStatus).ChangeEnemyData(data);
            // 적 외형 변경
            SetEnemyLook(ref enemy,ref data);
            // 적 위치값 변경
            enemy.gameObject.transform.localPosition= position;
            // 활성화
            enemy.gameObject.SetActive(true);

            return enemy;
        }

        // 적 캐릭터의 외형을 변경합니다.
        public void SetEnemyLook(ref EnemyController enemy,ref EnemyData data)
        {
            // 외형에 맞는 오브젝트를 보여줍니다
            enemy.transform.GetChild(data.apperenceNum).gameObject.SetActive(true);
            enemy.GetComponent<CharacterAppearance>().EquipWeapon(data.weaponApparenceID, data.handleType);
        }


        // 적 캐릭터를 오브젝트 풀에 넣어줍니다.
        public void ReturnEnemy(EnemyController enemy)
        {
            // 풀에 넣어줍니다
            enemyControllerPool.Enqueue(enemy);
            // 현재 외형을 꺼줍니다
            enemy.transform.GetChild((enemy.battleStatus.status as EnemyStatus).apperenceNum).gameObject.SetActive(false);

            enemy.gameObject.SetActive(false);
        }

        #endregion

        #region BattleText

        Queue<BattleText> battleTextPool = new Queue<BattleText>(); // 전투 텍스트 오브젝트 풀

        // 전투 텍스트를 생성합니다.
        public BattleText CreateText()
        {
            // 부모 오브젝트 하위에 전투 텍스트를 생성합니다.
            GameObject obj = Instantiate(battleTextPrefab, battleTextParent.transform);
            BattleText text = obj.GetComponent<BattleText>();
            return text;
        }

        // 오브젝트 풀에 있는 전투 텍스트를 반환합니다.
        public BattleText GetText(string textStr, Vector3 position, DamagedType type = DamagedType.Normal)
        {
            BattleText text;

            if (battleTextPool.Count > 0)
            {
                // 풀에 있는 것 사용
                text = battleTextPool.Dequeue();

            }
            else
            {
                // 새로 만들어서 풀에 넣기
                text = CreateText();
            }

            // 전투 텍스트를 세팅합니다.
            text.Init(textStr, position, type);
            text.gameObject.SetActive(true);

            return text;
        }

        // 전투 텍스트를 풀에 반환합니다.
        public void ReturnText(BattleText text)
        {
            text.gameObject.SetActive(false);
            battleTextPool.Enqueue(text);
        }
        #endregion

        #region LootingItem

        Queue<LootingItem> LootingItemPool = new Queue<LootingItem>();  // 루팅 아이템 오브젝트 풀

        // 루팅아이템을 생성합니다.
        public LootingItem CreateLooitngItem(Transform backpackTransform)
        {
            // 부모 오브젝트 하위에 루팅아이템를을생성합니다.
            LootingItem item = Instantiate(lootingItem, LootingItemParent);
            // 루팅아이템이 찾아갈 가방 오브젝트의 위치를 세팅합니다.
            item.SetUp(backpackTransform);
            return item;
        }

        // 오브젝트 풀에서 루팅아이템을 가져옵니다
        public LootingItem GetLootingItem(Vector3 position, DropItemType type, Transform backpackTransform)
        {
            LootingItem lootingItem;

            if (LootingItemPool.Count > 0)
                // 풀에 남아있다면 재활용합니다.
            {
                lootingItem = LootingItemPool.Dequeue();
            }
            else
            {
                // 풀이 비어있다면 새롭게 생성합니다.
                lootingItem = CreateLooitngItem(backpackTransform);
            }

            // 루팅아이템을 세팅합니다.
            lootingItem.Init(position, type);
            lootingItem.gameObject.SetActive(true);

            return lootingItem;
        }

        // 루팅아이템을 풀에 반환합니다
        public void ReturnLootingItem(LootingItem item)
        {
            item.gameObject.SetActive(false);
            LootingItemPool.Enqueue(item);
        }

        #endregion

        #region Skill

        List<Ability.Ability> abilityPool = new List<Ability.Ability>();    // 스킬 이펙트 오브젝트 풀

        // 스킬 이펙트를 생성합니다
        private Ability.Ability CreateAbility(int abilityID)
        {
            // 해당 ID에 맞는 스킬이펙트를 생성합니다.
            Ability.Ability prefab = GameManager.Instance.abilityPrefabDic[abilityID];
            var ability = Instantiate(prefab, abilityParent);
            abilityPool.Add(ability);  
            return ability;
        }

        // 스킬 이펙트를 오브젝트 풀에서 가져옵니다.
        public Ability.Ability GetAbility(int abilityID)
        {
            Ability.Ability getAbility;

            if (abilityPool.Count > 0)
            {
                // 필요한 스킬 이펙트 ID가 풀에있는지 확인합니다.
                if ((getAbility = abilityPool.Find(ability => (ability.abilityID == abilityID) && (!ability.gameObject.activeInHierarchy))) == null)
                {
                    // 풀에 알맞은 이펙트가 없다면 새롭게 생성합니다.
                    getAbility = CreateAbility(abilityID);
                }
            }
            else
            {
                // 풀이 아예 비워져 있다면 가져옵니다.
                getAbility = CreateAbility(abilityID);
            }

            getAbility.gameObject.SetActive(true);
            return getAbility;
        }

        // 스킬 이펙트에 별도의 효과가 있을 시 효과를 적요앟고 오브젝트 풀에서 가져옵니다
        public Ability.Ability GetAbility(int abilityID, Transform starPos, UnityAction<BattleStatus> hitAction = null, UnityAction<BattleStatus> chainAction = null, Space space = Space.Self)
        {
            Ability.Ability getAbility;

            if (abilityPool.Count > 0)
            {
                // 필요한 스킬 이펙트 ID가 풀에있는지 확인합니다.
                if ((getAbility = abilityPool.Find(ability => (ability.abilityID == abilityID) && (!ability.gameObject.activeInHierarchy))) == null)
                {
                    // 풀에 알맞은 이펙트가 없다면 새롭게 생성합니다.
                    getAbility = CreateAbility(abilityID);
                }
            }
            else
            // 풀이 아예 비워져 있다면 가져옵니다.
            {
                getAbility = CreateAbility(abilityID);
            }

            // 스킬 이펙트에 이벤트를 세팅합니다.
            getAbility.InitAbility(starPos, hitAction, chainAction, space);
            getAbility.gameObject.SetActive(true);
            return getAbility;
        }

        // 스킬 이펙트를 풀에 반환합니다.
        public void ReturnAbility(Ability.Ability ability)
        {
            ability.transform.position = Vector3.zero;
            ability.gameObject.SetActive(false);
        }

        // 모든 스킬 이펙트를 풀에 반환합니다.
        public void ReleaseAllAbility()
        {
            foreach (var ability in abilityPool)
            {
                ReturnAbility(ability);
            }
        }

        #endregion
    }
}