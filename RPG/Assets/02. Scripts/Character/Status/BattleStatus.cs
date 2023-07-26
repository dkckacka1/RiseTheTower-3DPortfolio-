using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using RPG.Character.Equipment;
using RPG.Battle.UI;
using RPG.Battle.Event;
using RPG.Battle.Control;

/*
 * 전투시 캐릭터들의 스텟 클래스
 */

namespace RPG.Character.Status
{
    public class BattleStatus : MonoBehaviour
    {
        // Component
        [Header("UI")]
        public CharacterUI characterUI;     // 전투 캐릭터 UI

        [Header("Battle")]
        public int currentHp = 0;       // 현재 체력
        public bool isDead = false;     // 죽음 여부

        [Header("Status")]
        public Status status;       // 캐릭터 스텟

        // Coroutine
        public IEnumerator perSecCoroutine; // 매초당 이벤트를 호출할 코루틴

        public CombatState currentState;    // 현재 상태
        public DebuffType currentDebuff;    // 현재 디버프 상황
        public bool isActunableDebuff; // 현재 행동 불가 디버프 상태인가?
        private bool isCursed;  // 현재 저주받음 상태인지 여부
        public List<IEnumerator> debuffList = new List<IEnumerator>(); // 현재 디버프 리스트

        // Event
        public PerSecondEvent perSecEvent;      // 매초당 나올 이벤트
        public TakeDamageEvent takeDamageEvent; // 피해받았을때 나올 이벤트

        // 현재 체력을 갱신합니다.
        public int CurrentHp
        {
            get => currentHp;
            set
            {
                currentHp = Mathf.Clamp(value, 0, status.MaxHp);
                if (characterUI != null)
                    // 현재 체력바를 세팅합니다.
                {
                    characterUI.UpdateHPUI(currentHp);
                }

                if (currentHp <= 0)
                    // 체력이 0 이하가 되면 사망합니다.
                {
                    Dead();
                }
            }
        }

        private void Awake()
        {
            perSecEvent = new PerSecondEvent();     
            takeDamageEvent = new TakeDamageEvent();
            perSecCoroutine = PerSecEvent();
        }

        #region Initialize
        // 활성화 되었을때 초기화합니다.
        public virtual void Init()
        {
            currentHp = status.MaxHp;
            isDead = false;
        }

        // 게임 오브젝트가 활성화 될때 행동
        public virtual void Release()
        {
        }
        #endregion

        #region BattleEvent

        // 피해입을때 이벤트를 추가합니다.
        public void AddTakeDamageAction(UnityAction<BattleStatus, BattleStatus> action)
        {
            takeDamageEvent.AddListener(action);
        }

        // 초당 이벤트를 추가합니다.
        public void AddPerSecAction(UnityAction<BattleStatus> action)
        {
            perSecEvent.AddListener(action);
        }

        // 매초당 이벤트를 호출할 코루틴
        public IEnumerator PerSecEvent()
        {
            while (!isDead)
            {
                yield return new WaitForSeconds(1f);
                perSecEvent.Invoke(this);
            }
        }

        #endregion

        // 피해 입습니다.
        public void TakeDamage(int damage, DamagedType type = DamagedType.Normal)
        {
            if (isDead) return;

            int totalDamage = 0;
            totalDamage += damage;
            if (isCursed)
            {
                totalDamage += (int)(damage * 0.5f);
            }

            // 받은 피해 타입에 따라 나올 전투 텍스트를 변경합니다.
            switch (type)
            {
                case DamagedType.Normal:
                    CurrentHp -= totalDamage;
                    characterUI.TakeDamageText(totalDamage.ToString(), type);
                    break;
                case DamagedType.Ciritical:
                    CurrentHp -= totalDamage;
                    characterUI.TakeDamageText(totalDamage.ToString() + "!!", type);
                    break;
                case DamagedType.MISS:
                    characterUI.TakeDamageText("MISS~", type);
                    break;
            }
        }

        // 캐릭터가 죽습니다.
        public void Dead()
        {
            StopAllDebuff();
            RemoveAllDebuff();
            currentState = CombatState.Dead;
            isDead = true;
        }

        // 체력을 회복합니다.
        public void Heal(int healPoint)
        {
            CurrentHp += healPoint;
        }

        #region Debuff
        // 디버프를 받습니다.
        public void TakeDebuff(DebuffType type, float duration)
        {
            // 얻은 디버프 종류에 맞춰서 활성화 합니다.
            switch (type)
            {
                case DebuffType.Stern:
                    if (isActunableDebuff) return;
                    {
                        // 스턴 디버프를 활성화합니다.
                        IEnumerator debuff = TakeStern(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Fear:
                    if (isActunableDebuff) return;
                    {
                        // 공포 디버프를 활성화합니다.
                        IEnumerator debuff = TakeFear(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Temptation:
                    if (isActunableDebuff) return;
                    {
                        // 유혹 디버프를 활성화합니다.
                        IEnumerator debuff = TakeTemptation(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Paralysis:
                    {
                        // 마비 디버프를 활성화합니다.
                        IEnumerator debuff = TakeParalysis(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Bloody:
                    {
                        // 출혈 디버프를 활성화합니다.
                        IEnumerator debuff = TakeBloody(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
                case DebuffType.Curse:
                    {
                        // 저주 디버프를 활성화합니다.
                        IEnumerator debuff = TakeCurse(duration);
                        StartCoroutine(debuff);
                        debuffList.Add(debuff);
                    }

                    break;
            }
        }

        // 모든 디버프를 활성화합니다.
        public void ReStartAllDebuff()
        {
            foreach (var debuff in debuffList)
            {
                StartCoroutine(debuff);
            }
        }

        // 모든 디버프를 중단시킵니다.
        public void StopAllDebuff()
        {
            foreach (var debuff in debuffList)
            {
                StopCoroutine(debuff);
            }
        }

        // 모든 디버프를 제거합니다.
        public void RemoveAllDebuff()
        {
            debuffList.Clear();
            characterUI.debuffUI.ResetAllDebuff();
        }

        // 기절할때의 행동입니다.
        private IEnumerator TakeStern(float duration)
        {
            // 현재 상태를 행동불가 상태로 변경합니다.
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Stern;
            // 기절 UI를 표시합니다.
            characterUI.debuffUI.InitDebuff(DebuffType.Stern);
            // 기절당한 동안에는 아무런 행동도 하지 않습니다.

            float time = 0;
            while (true)
            {
                // 기절 UI의 시간을 표시합니다.
                characterUI.debuffUI.ShowDebuff(DebuffType.Stern, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // 기절이 끝나면 UI를 업데이트하고 행동을 개시합니다.
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Stern);
        }


        // 공포시 행동입니다.
        private IEnumerator TakeFear(float duration)
        {
            // 현재 상태를 행동불가 상태로 변경합니다.
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Fear;
            // 공포 UI를 표시합니다.
            characterUI.debuffUI.InitDebuff(DebuffType.Fear);

            // 공포는 현재 이동속도의 70% 속도로 대상에게서 멀어집니다.
            float defaultMovementSpeed = status.MovementSpeed;
            UpdateMovementSpeed(status.MovementSpeed * 0.7f);

            float time = 0;
            while (true)
            {
                // 공포 UI의 시간을 표시합니다.
                characterUI.debuffUI.ShowDebuff(DebuffType.Fear, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // 공포가 끝나면 UI를 업데이트하고 이동속도를 되돌린뒤 행동을 개시합니다.
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
            UpdateMovementSpeed(defaultMovementSpeed);
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Fear);
        }

        // 유혹시 행동입니다.
        private IEnumerator TakeTemptation(float duration)
        {
            // 현재 상태를 행동불가 상태로 변경합니다.
            currentState = CombatState.Actunable;
            isActunableDebuff = true;
            currentDebuff = DebuffType.Temptation;
            // 유혹 UI를 표시합니다.
            characterUI.debuffUI.InitDebuff(DebuffType.Temptation);

            // 유혹은 현재 이동속도의 30% 속도로 대상에게 다가갑니다.
            float defaultMovementSpeed = status.MovementSpeed;
            UpdateMovementSpeed(status.MovementSpeed * 0.3f);

            float time = 0;
            while (true)
            {
                // 유혹 UI의 시간을 표시합니다.
                characterUI.debuffUI.ShowDebuff(DebuffType.Temptation, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // 유혹가 끝나면 UI를 업데이트하고 이동속도를 되돌린뒤 행동을 개시합니다.
            currentState = CombatState.Actable;
            isActunableDebuff = false;
            currentDebuff = DebuffType.Defualt;
            UpdateMovementSpeed(defaultMovementSpeed);
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Temptation);
        }

        // 마비 시 행동입니다.
        private IEnumerator TakeParalysis(float duration)
        {
            // 마비 UI를 표시합니다.
            characterUI.debuffUI.InitDebuff(DebuffType.Paralysis);

            // 마비는 현재 이동속도를 0으로 만듭니다.
            float defaultMovementSpeed = status.MovementSpeed;
            UpdateMovementSpeed(0);

            float time = 0;
            while (true)
            {
                // 마비 UI의 시간을 표시합니다.
                characterUI.debuffUI.ShowDebuff(DebuffType.Paralysis, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // 마비가 끝나면 UI를 업데이트 하고 이동속도를 되돌립니다.
            UpdateMovementSpeed(defaultMovementSpeed);
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Paralysis);
        }

        // 출혈 시 행동입니다.
        private IEnumerator TakeBloody(float duration)
        {
            // 출혈 UI를 표시합니다.
            characterUI.debuffUI.InitDebuff(DebuffType.Bloody);

            // 출혈은 매 초마다 최대체력이 2%씩 감소합니다.
            int bloodyDamage = status.MaxHp / 50;

            float time = 0;
            while (true)
            {
                if (time % 1 == 0)
                {
                    TakeDamage(bloodyDamage);
                }
                // 출혈 UI의 시간을 표시합니다.
                characterUI.debuffUI.ShowDebuff(DebuffType.Bloody, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // 출혈가 끝나면 UI를 업데이트합니다.
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Bloody);
        }


        // 저주 시 행동입니다.
        private IEnumerator TakeCurse(float duration)
        {
            // 저주 UI를 표시합니다.
            characterUI.debuffUI.InitDebuff(DebuffType.Curse);

            // 저주는 추가 데미지를 입습니다.
            isCursed = true;

            float time = 0;
            while (true)
            {
                // 저주 UI의 시간을 표시합니다.
                characterUI.debuffUI.ShowDebuff(DebuffType.Curse, duration - time);
                yield return new WaitForSeconds(0.1f);
                time += 0.1f;
                if (time >= duration)
                {
                    break;
                }
            }

            // UI를업데이트하고 저주상태에서 되돌립니다.
            characterUI.debuffUI.ReleaseDebuff(DebuffType.Curse);
            isCursed = false;
        }
        #endregion

        // 행동들의 수치를 스텟에 맞게 업데이트합니다.
        public void UpdateBehaviour()
        {
            // 이동행동에 공격범위와 이동속도를 전달합니다.
            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            nav.speed = status.MovementSpeed;
            nav.stoppingDistance = status.AttackRange;

            // 공격행동에 공격속도를 전달합니다.
            Controller controller = GetComponent<Controller>();
            controller.animator.SetFloat("AttackSpeed", status.AttackSpeed);
            controller.attack.attackDelay = controller.attack.defaultAttackAnimLength / status.AttackSpeed;
        }

        // 이동속도가 변경되었을때 이동행동에게 전달합니다.
        public void UpdateMovementSpeed(float speed)
        {
            status.MovementSpeed = speed;

            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            nav.speed = status.MovementSpeed;
        }

        // 공격범위가 변경되었을때 공격행동에게 전달합니다.
        public void UpdateAttackRange(float range)
        {
            status.AttackRange = range;

            NavMeshAgent nav = GetComponent<NavMeshAgent>();
            nav.stoppingDistance = status.AttackRange;
        }

        // 공격속도가 변경되었을때 공격행동에게 전달합니다.
        public void UpdateAttackSpeed(float speed)
        {
            status.AttackSpeed = speed;

            Controller controller = GetComponent<Controller>();
            controller.animator.SetFloat("AttackSpeed", status.AttackSpeed);
            controller.attack.attackDelay = controller.attack.defaultAttackAnimLength / status.AttackSpeed;
        }
    }
}