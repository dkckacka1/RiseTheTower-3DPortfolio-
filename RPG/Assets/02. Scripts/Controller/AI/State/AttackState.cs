using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Behaviour;
using RPG.Battle.Control;

namespace RPG.Battle.AI
{
    public class AttackState : State, IState
    {
        Attack attack;
        public Coroutine attackDelayCheckCoroutine;

        public AttackState(Controller controller) : base(controller)
        {
            attack = controller.attack;
        }

        public void OnEnd()
        {
        }

        public void OnStart()
        {
            controller.currentAIState = AIState.Attack;
        }

        public void OnUpdate()
        {
            if (!attack.canAttack) return;

            controller.animator.SetTrigger("Attack");
            controller.transform.LookAt(controller.target.transform);
            attack.AttackTarget();
            attackDelayCheckCoroutine = controller.StartCoroutine(attack.WaitAttackDelay());
        }
    }
}
