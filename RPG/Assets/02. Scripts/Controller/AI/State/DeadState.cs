using RPG.Battle.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 사망 상태 클래스
 */

namespace RPG.Battle.AI
{
    public class DeadState : State, IState
    {
        float deadTimer = 0f;            // 죽음 타이머
        float deadTime = 2f;     // 죽음 애니메이션이 나올때 까지 기다릴 시간
        bool callDeadEvent = true;      // 죽음 이벤트가 출력된 상태인지 확인

        public DeadState(Controller controller) : base(controller)
        {
        }

        public void OnStart()
        {
            // 죽음 상태에 진입하면 현재 AI 상태를 죽음상태로 변경해줍니다.
            controller.currentState = AIState.Dead;
            deadTimer = 0f;
            callDeadEvent = true;
            // 공격하는 도중이었다면 공격을 취소하고 죽음 애니메이션을 보여줍니다.
            controller.StopAttack();
            controller.animator.SetTrigger("Dead");
            // 네브메쉬를 꺼줍니다.
            controller.nav.enabled = false;

            // TODO : 죽음 상태가 할 행동은 아님
            if (controller is EnemyController)
            {
                // 만약 적 컨트롤러였다면 아이템을 루팅합니다.
                (controller as EnemyController).LootingItem();
            }
        }

        public void OnEnd()
        {
            // 사망 시의 행동 없음
        }

        public void OnUpdate()
        {
            // 죽음 타이머를 계산합니다.
            deadTimer += Time.deltaTime;
            if (callDeadEvent && deadTimer > deadTime)
                // 일정 시간이 지났다면
            {
                callDeadEvent = false;
                // 컨트롤러의 죽음 메서드를 호출합니다.
                controller.DeadController();
            }
        }
    }
}

