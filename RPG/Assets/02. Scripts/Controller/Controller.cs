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

/*
 *  전투 시 캐릭터의 동작을 수행하는 컨트롤러
 */

namespace RPG.Battle.Control
{
    public abstract class Controller : MonoBehaviour
    {
        // Component
        public CharacterUI ui;              // 캐릭터 UI
        public Animator animator;           // 캐릭터 애니메이터
        public NavMeshAgent nav;            // 캐릭터 네브메쉬에이전트
        public BattleStatus battleStatus;   // 캐릭터 스탯

        // AI State
        public StateContext stateContext;   // 상태 관리를 담당하는 컨택스트
        public IdelState idleState;         // 유휴 상태 동작
        public ChaseState chaseState;       // 추적 상태 동작
        public AttackState attackState;     // 공격 상태 동작
        public DeadState deadState;         // 죽음 상태 동작
        public DebuffState debuffState;     // 디버프 상태 동작

        // Behaviour
        public Movement movement;   // 이동 행동
        public Attack attack;       // 공격 행동

        // Battle
        public Controller target;       // 추적 타겟
        public AIState currentState;  // 현재 상태

        private void Awake()
        {
            // 컨트롤러 생성시 각 동작과 상태를 생성합니다.
            SetUp();
        }

        private void OnEnable()
        {
            // 컨트롤러가 활성화되면 스탯, UI, 이벤트를 세팅합니다.
            battleStatus.Init();
            ui.Init();
            Init();
            if (BattleManager.Instance == null)
            {
                return;
            }
            // 이벤트 버스에 전투 상태에 따른 메서드를 구독합니다.
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Win, Win);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Defeat, Defeat);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Ready, Ready);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Battle, Battle);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Pause, Pause);
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Ending, Ending);
        }


        private void OnDisable()
        {
            // 컨트롤러가 비활성화되면 스탯, UI 이벤트를 꺼줍니다.
            battleStatus.Release();
            ui.ReleaseUI();
            Release();
            if (BattleManager.Instance == null)
            {
                return;
            }
            // 이벤트 버스에 등록한 메서드를 구독해제합니다.
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

            // 현재 상태를 판단합니다.
            stateContext.SetState(CheckState());
            // 현재 상태의 동작을 수행합니다.
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

        // 활성화 될 때 초기화합니다.
        public virtual void Init()
        {
            attack.canAttack = true;
            nav.enabled = true;
            animator.Rebind();

            RuntimeAnimatorController rc = animator.runtimeAnimatorController;
            // 현재 공격 애니메이션의 길이를 체크합니다.
            foreach (var item in rc.animationClips)
            {
                if (item.name == "Attack")
                {
                    attack.defaultAttackAnimLength = item.length;
                    break;
                }
            }

            // 행동의 수치를 업데이트합니다.
            battleStatus.UpdateBehaviour();
            battleStatus.currentState = CombatState.Default;
        }

        // 비활성화 될 때 행동입니다.
        public virtual void Release()
        {
        }
        #endregion

        #region BattleSceneState EventMethod
        // 전투에서 승리합니다.
        public void Win()
        {
            // 대상지정과 이동행동을 취소합니다.
            target = null;
            movement.ResetNav();
            // 모든 디버프를 제거하고 이벤트를 중단합니다.
            battleStatus.StopAllDebuff();
            battleStatus.RemoveAllDebuff();
            StopCoroutine(battleStatus.perSecCoroutine);
        }

        // 전투에서 패배합니다.
        public void Defeat()
        {
            // 대상지정과 이동행동을 취소합니다.
            target = null;
            movement.ResetNav();
            // 모든 디버프를 제거하고 이벤트를 중단합니다.
            battleStatus.StopAllDebuff();
            battleStatus.RemoveAllDebuff();
            StopCoroutine(battleStatus.perSecCoroutine);
        }

        // 전투를 준비합니다.
        public void Ready()
        {
            // 대상지정과 이동행동을 취소합니다.
            target = null;
            movement.ResetNav();
        }

        // 전투를 수행합니다.
        public void Battle()
        {
            // 스킬 이벤트를 활성화하고, 중단했던 디버프를 다시 활성화합니다.
            animator.speed = 1;
            StartCoroutine(battleStatus.perSecCoroutine);
            StartCoroutine(movement.moveEventCorotine);
            battleStatus.ReStartAllDebuff();
        }

        // 전투를 잠시 중단합니다.
        public void Pause()
        {
            // 동작중이었던 애니메이터를 멈추게합니다.
            animator.speed = 0;
            // 스킬 이벤트를 활성화하고 디버프를 중단합니다.
            StopCoroutine(battleStatus.perSecCoroutine);
            StopCoroutine(movement.moveEventCorotine);
            battleStatus.StopAllDebuff();
            // 이동중이었다면 이동을 취소합니다.
            movement.ResetNav();
        }

        // 엔딩 시 행동
        protected virtual void Ending()
        {
        }


        #endregion

        #region CheckState
        // ORDER : #3) 현재 상태에 따라 컨트롤러의 동작을 변경하는 상태패턴 구현
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

        // 공격을 중단합니다.
        public void StopAttack()
        {
            if (attack.isAttack)
            {
                attack.isAttack = false;
            }
        }

        // 컨트롤러의 죽음을 전투 매니저에 알려줍니다.
        public virtual void DeadController()
        {
            BattleManager.Instance.CharacterDead(this);
        }

        /// <summary>
        /// 찾으면 true 못찾으면 false
        /// </summary>
        /// <param name="controller"></param>
        /// <returns></returns>
        // 대상을 지정합니다.
        public abstract bool SetTarget(out Controller controller);

        // 공격 애니메이션 이벤트 메서드입니다.
        public void AttackEvent()
        {
            if (target == null) return;
            if (target.battleStatus.isDead) return;

            AudioManager.Instance.PlaySoundOneShot("AttackSound");
            attack.TargetTakeDamage();
        }
    }
}