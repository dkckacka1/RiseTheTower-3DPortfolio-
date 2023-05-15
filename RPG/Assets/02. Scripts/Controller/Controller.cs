using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using RPG.Battle.Core;
using RPG.Battle.Behaviour;
using RPG.Battle.AI;
using RPG.Battle.UI;
using RPG.Character.Status;
using RPG.Main.Audio;

namespace RPG.Battle.Control
{
    public abstract class Controller : MonoBehaviour
    {
        // Component
        public CharacterUI ui;
        public Animator animator;
        public NavMeshAgent nav;
        public BattleStatus battleStatus;

        // AI State
        public StateContext stateContext;
        public IdelState idleState;
        public ChaseState chaseState;
        public AttackState attackState;
        public DeadState deadState;
        public DebuffState debuffState;

        // Behaviour
        public Movement movement;
        public Attack attack;

        // Battle
        public Controller target;
        public AIState currentAIState;

        private void Awake()
        {
            SetUp();
        }

        private void OnEnable()
        {
            battleStatus.Init();
            ui.Init();
            Init();
            if (BattleManager.Instance == null)
            {
                return;
            }
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Win, Win);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Defeat, Defeat);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Ready, Ready);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Battle, Battle);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Pause, Pause);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Ending, Ending);
        }


        private void OnDisable()
        {
            battleStatus.Release();
            ui.ReleaseUI();
            Release();
            if (BattleManager.Instance == null)
            {
                return;
            }
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Win, Win);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Defeat, Defeat);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Ready, Ready);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Battle, Battle);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Pause, Pause);
            BattleManager.Instance.UnsubscribeEvent(BattleSceneState.Ending, Ending);
        }


        private void Update()
        {
            // 전투중인가?
            if (BattleManager.Instance == null) return;
            if (BattleManager.Instance.currentBattleState != BattleSceneState.Battle) return;

            stateContext.SetState(CheckState());
            stateContext.Update();
        }


        #region Initialize

        // 생성 시 초기화 단계
        public virtual void SetUp()
        {
            movement = new Movement(battleStatus, nav);
            attack = new Attack(battleStatus);

            stateContext = new StateContext(this);
            idleState = new IdelState(this);
            chaseState = new ChaseState(this);
            attackState = new AttackState(this);
            deadState = new DeadState(this);
            debuffState = new DebuffState(this);
        }

        public virtual void Init()
        {
            attack.canAttack = true;
            nav.enabled = true;
            animator.Rebind();

            RuntimeAnimatorController rc = animator.runtimeAnimatorController;
            foreach (var item in rc.animationClips)
            {
                if (item.name == "Attack")
                {
                    attack.defaultAttackAnimLength = item.length;
                    break;
                }
            }

            battleStatus.UpdateBehaviour();
            battleStatus.currentState = CombatState.Default;
        }

        public virtual void Release()
        {
        }
        #endregion

        #region BattleSceneState EventMethod
        public void Win()
        {
            target = null;
            movement.ResetNav();
            battleStatus.StopAllDebuff();
            battleStatus.RemoveAllDebuff();
            StopCoroutine(battleStatus.perSecCoroutine);
        }

        public void Defeat()
        {
            target = null;
            movement.ResetNav();
            battleStatus.StopAllDebuff();
            battleStatus.RemoveAllDebuff();
            StopCoroutine(battleStatus.perSecCoroutine);
        }

        public void Ready()
        {
            target = null;
            movement.ResetNav();
        }

        public void Battle()
        {
            animator.speed = 1;
            StartCoroutine(battleStatus.perSecCoroutine);
            StartCoroutine(movement.moveEventCorotine);
            battleStatus.ReStartAllDebuff();
        }

        public void Pause()
        {
            animator.speed = 0;
            StopCoroutine(battleStatus.perSecCoroutine);
            StopCoroutine(movement.moveEventCorotine);
            battleStatus.StopAllDebuff();
            movement.ResetNav();
        }

        protected virtual void Ending()
        {
        }


        #endregion

        #region CheckState
        private IState CheckState()
        {
            if (battleStatus.isDead)
            // 나는 죽어있는가?
            {
                return deadState;
            }

            if (battleStatus.currentState == CombatState.Actunable)
            // 행동 불가 상태인가?
            {
                return debuffState;
            }

            if (!SetTarget(out target))
            // 다른 적이 있는가?
            {
                return idleState;
            }

            if (attack.isAttack)
                // 공격중인가?
            {
                return attackState;
            }

            if (movement.MoveDistanceResult(target.transform))
            // 적과의 거리가 나의 공격 사거리보다 먼가?
            {
                return chaseState;
            }
            else
            {
                //타겟이 살아있는가?
                if (!target.battleStatus.isDead)
                {
                    if (attack.canAttack)
                        // 공격할 수 있는가?
                        return attackState;
                }
            }

            return idleState;
        }

        #endregion

        public void StopAttack()
        {
            if (attack.isAttack)
            {
                attack.isAttack = false;
            }
        }

        public virtual void DeadEvent()
        {
            BattleManager.Instance.CharacterDead(this);
        }

        /// <summary>
        /// 찾으면 true 못찾으면 false
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        public abstract bool SetTarget(out Controller controller);

        public void AttackEvent()
        {
            if (target.battleStatus.isDead) return;

            AudioManager.Instance.PlaySoundOneShot("AttackSound");
            attack.TargetTakeDamage();
        }
    }
}