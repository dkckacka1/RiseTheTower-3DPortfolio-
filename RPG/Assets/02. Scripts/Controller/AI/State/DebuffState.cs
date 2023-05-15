using UnityEngine;
using RPG.Battle.Control;
using RPG.Battle.Behaviour;
using RPG.Character.Status;

namespace RPG.Battle.AI
{
    public class DebuffState : State, IState
    {
        delegate void DebuffAction(Controller character, Controller target);
        DebuffAction action;

        private Movement movement;

        public DebuffState(Controller controller) : base(controller)
        {
            movement = controller.movement;
        }

        public void OnEnd()
        {
            action = null;
            controller.SetTarget(out controller.target);
        }

        public void OnStart()
        {
            controller.currentAIState = AIState.Debuff;
            controller.animator.ResetTrigger("Attack");
            controller.animator.ResetTrigger("Move");
            controller.animator.ResetTrigger("Idle");
            controller.StopAttack();
            switch (controller.battleStatus.currentDebuff)
            {
                case DebuffType.Stern:
                    controller.animator.SetTrigger("Stern");
                    controller.movement.ResetNav();
                    action = SternAction;
                    break;
                case DebuffType.Temptation:
                    controller.animator.SetTrigger("Temptation");
                    controller.movement.ResetNav();
                    action = TemptationAction;
                    break;
                case DebuffType.Fear:
                    controller.animator.SetTrigger("Fear");
                    controller.movement.ResetNav();
                    action = FearAction;
                    break;
            }
        }

        public void OnUpdate()
        {
            action.Invoke(controller, controller.target);
        }

        public void SternAction(Controller character, Controller target)
        {
            // 그자리에서 가만히
        }

        public void TemptationAction(Controller character, Controller target)
        {
            // 타겟으로 이동
            movement.MoveNav(target.transform);
        }

        public void FearAction(Controller character, Controller target)
        {
            // 타겟에서 멀어짐
            Vector3 runDirection = (character.transform.position - target.transform.position).normalized * character.battleStatus.status.AttackRange * 2;
            Debug.DrawLine(controller.transform.position, character.transform.position + runDirection, Color.red);
            movement.MovePos(character.transform.position + runDirection);

        }
    }
}
