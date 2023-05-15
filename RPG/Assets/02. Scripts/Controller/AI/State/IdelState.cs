
using RPG.Battle.Control;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.AI
{
    public class IdelState : State, IState
    {
        public IdelState(Controller controller) : base(controller)
        {
        }

        public void OnStart()
        {
            controller.currentAIState = AIState.Idle;
            controller.animator.SetTrigger("Idle");
        }

        public void OnEnd()
        {
        }

        public void OnUpdate()
        {
        }
    }
}
