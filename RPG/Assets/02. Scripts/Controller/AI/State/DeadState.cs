using RPG.Battle.Control;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.AI
{
    public class DeadState : State, IState
    {
        float deadTime = 0f;
        float deadEventTiming = 2f;
        bool callDeadEvent = true;

        public DeadState(Controller controller) : base(controller)
        {
        }

        public void OnStart()
        {
            controller.currentAIState = AIState.Dead;
            deadTime = 0f;
            callDeadEvent = true;
            controller.StopAttack();
            controller.animator.SetTrigger("Dead");
            controller.nav.enabled = false;

            if (controller is EnemyController)
            {
                (controller as EnemyController).LootingItem();
            }
        }

        public void OnEnd()
        {
        }

        public void OnUpdate()
        {
            deadTime += Time.deltaTime;
            if (callDeadEvent && deadTime > deadEventTiming)
            {
                callDeadEvent = false;
                controller.DeadEvent();
            }
        }
    }
}

