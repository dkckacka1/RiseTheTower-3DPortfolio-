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

namespace RPG.Battle.Core
{
    public class ObjectPooling : MonoBehaviour
    {
        [SerializeField] PlayerController playerController;
        [SerializeField] EnemyController enemyController;
        [SerializeField] GameObject battleTextPrefab;
        [SerializeField] LootingItem lootingItem;
        [SerializeField] Transform playerParent;
        [SerializeField] Transform enemyParent;
        [SerializeField] Transform battleTextParent;
        [SerializeField] Transform LootingItemParent;
        [SerializeField] Transform abilityParent;

        Canvas battleCanvas;

        public void SetUp(Canvas canvas)
        {
            this.battleCanvas = canvas;
        }

        public PlayerController CreatePlayer(PlayerStatus status)
        {
            PlayerController controller = Instantiate<PlayerController>(playerController, playerParent);
            if (controller == null)
            {
                Debug.Log("Controller is NULL");
            }

            if (controller.battleStatus == null)
            {
                Debug.Log("(controller.battleStatus is NULL");
            }

            if ((controller.battleStatus.status as PlayerStatus) == null)
            {
                Debug.Log("(controller.battleStatus.status as PlayerStatus) is NULL");
            }

            var controllerStatus = (controller.battleStatus.status as PlayerStatus);

            controllerStatus.SetPlayerStatusFromStatus(status, controller.GetComponentInChildren<CharacterAppearance>());
            controller.gameObject.SetActive(true);

            return controller;
        }

        #region Enemy
        // Pool
        Queue<EnemyController> enemyControllerPool = new Queue<EnemyController>();

        private EnemyController CreateController(EnemyData data)
        {
            EnemyController enemy = Instantiate<EnemyController>(enemyController, enemyParent);
            return enemy;
        }

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
                enemy = CreateController(data);
            }

            // 데이터 변경
            (enemy.battleStatus.status as EnemyStatus).ChangeEnemyData(data);
            // 외형 변경
            SetEnemyLook(ref enemy,ref data);
            // 위치값 변경
            enemy.gameObject.transform.localPosition= position;
            // 활성화
            enemy.gameObject.SetActive(true);

            return enemy;
        }

        public void SetEnemyLook(ref EnemyController enemy,ref EnemyData data)
        {
            enemy.transform.GetChild(data.apperenceNum).gameObject.SetActive(true);
            enemy.GetComponent<CharacterAppearance>().EquipWeapon(data.weaponApparenceID, data.handleType);
        }


        public void ReturnEnemy(EnemyController enemy)
        {
            enemyControllerPool.Enqueue(enemy);

            enemy.transform.GetChild((enemy.battleStatus.status as EnemyStatus).apperenceNum).gameObject.SetActive(false);

            enemy.gameObject.SetActive(false);
        }

        #endregion

        #region BattleText

        // Pool
        Queue<BattleText> battleTextPool = new Queue<BattleText>();


        public BattleText CreateText()
        {
            GameObject obj = Instantiate(battleTextPrefab, battleTextParent.transform);
            BattleText text = obj.GetComponent<BattleText>();
            return text;
        }

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

            text.Init(textStr, position, type);
            text.gameObject.SetActive(true);

            return text;
        }

        public void ReturnText(BattleText text)
        {
            text.gameObject.SetActive(false);
            battleTextPool.Enqueue(text);
        }
        #endregion

        #region LootingItem

        //Pool
        Queue<LootingItem> LootingItemPool = new Queue<LootingItem>();

        public LootingItem CreateLooitngItem(Transform backpackTransform)
        {
            LootingItem item = Instantiate(lootingItem, LootingItemParent);
            item.SetUp(backpackTransform);
            return item;
        }

        public LootingItem GetLootingItem(Vector3 position, DropItemType type, Transform backpackTransform)
        {
            LootingItem lootingItem;

            if (LootingItemPool.Count > 0)
            {
                lootingItem = LootingItemPool.Dequeue();
            }
            else
            {
                lootingItem = CreateLooitngItem(backpackTransform);
            }

            lootingItem.Init(position, type);
            lootingItem.gameObject.SetActive(true);

            return lootingItem;
        }

        public void ReturnLootingItem(LootingItem item)
        {
            item.gameObject.SetActive(false);
            LootingItemPool.Enqueue(item);
        }

        #endregion

        #region Skill

        List<Ability.Ability> abilityPool = new List<Ability.Ability>();

        private Ability.Ability CreateAbility(int abilityID)
        {
            Ability.Ability prefab = GameManager.Instance.abilityPrefabDic[abilityID];
            var ability = Instantiate(prefab, abilityParent);
            abilityPool.Add(ability);  
            return ability;
        }

        public Ability.Ability GetAbility(int abilityID)
        {
            Ability.Ability getAbility;

            if (abilityPool.Count > 0)
            {
                if ((getAbility = abilityPool.Find(ability => (ability.abilityID == abilityID) && (!ability.gameObject.activeInHierarchy))) == null)
                {
                    getAbility = CreateAbility(abilityID);

                }
            }
            else
            {
                getAbility = CreateAbility(abilityID);
            }

            getAbility.gameObject.SetActive(true);
            return getAbility;
        }

        public Ability.Ability GetAbility(int abilityID, Transform starPos, UnityAction<BattleStatus> hitAction = null, UnityAction<BattleStatus> chainAction = null, Space space = Space.Self)
        {
            Ability.Ability getAbility;

            if (abilityPool.Count > 0)
            {
                if ((getAbility = abilityPool.Find(ability => (ability.abilityID == abilityID) && (!ability.gameObject.activeInHierarchy))) == null)
                {
                    getAbility = CreateAbility(abilityID);
                }
            }
            else
            {
                getAbility = CreateAbility(abilityID);
            }

            getAbility.InitAbility(starPos, hitAction, chainAction, space);
            getAbility.gameObject.SetActive(true);
            return getAbility;
        }

        public void ReturnAbility(Ability.Ability ability)
        {
            ability.transform.position = Vector3.zero;
            ability.gameObject.SetActive(false);
        }

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