using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Behaviour;
using RPG.Battle.Control;

/*
 * 공격 상태 클래스
 */ 

namespace RPG.Battle.AI
{
    public class AttackState : State, IState
    {
        Attack attack;      // 공격 행동 정의
        public Coroutine attackDelayCheckCoroutine; // 공격 시간 체크용 코루틴

        public AttackState(Controller controller) : base(controller)
        {
            attack = controller.attack;
        }

        public void OnEnd()
        {
            // 공격 종료시의 행동 없음
        }

        public void OnStart()
        {
            // 공격 상태에 진입했다면 현재 AI상태를 공격상태로 변경해줍니다.
            controller.currentAIState = AIState.Attack;
        }

        public void OnUpdate()
        {
            // 현재 공격할 수 없다면 리턴
            if (!attack.canAttack) return;

            // 대상을 바라본뒤 공격합니다.
            controller.animator.SetTrigger("Attack");
            controller.transform.LookAt(controller.target.transform);
            attack.AttackTarget();
            // 공격 딜레이를 계산하고 대기합니다.
            attackDelayCheckCoroutine = controller.StartCoroutine(attack.WaitAttackDelay());
        }
    }
}
