
using RPG.Battle.Control;
using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * 유휴 상태 클래스
 */

namespace RPG.Battle.AI
{
    public class IdelState : State, IState
    {
        public IdelState(Controller controller) : base(controller)
        {
        }

        // 유휴 상태에 진입하면 유휴 애니메이션을 세팅합니다.
        public void OnStart()
        {
            controller.currentAIState = AIState.Idle;
            controller.animator.SetTrigger("Idle");
        }

        public void OnEnd()
        {
            // 유휴상태 종료시 행동 없음
        }

        public void OnUpdate()
        {
            // 유휴상태 진행 시 행동 없음
        }
    }
}
