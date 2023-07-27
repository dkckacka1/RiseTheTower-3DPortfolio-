using UnityEngine;
using RPG.Battle.Control;
using RPG.Battle.Behaviour;
using RPG.Character.Status;

/*
 * 디버프 상태 클래스
 */

namespace RPG.Battle.AI
{
    public class DebuffState : State, IState
    {
        delegate void DebuffAction(Controller character, Controller target);    // 디버프 액션 정의
        DebuffAction action;                                                    // 디버프 액션

        private Movement movement;  // 이동 행동

        public DebuffState(Controller controller) : base(controller)
        {
            movement = controller.movement;
        }

        public void OnEnd()
        {
            // 디버프 상태가 액션을 null 세팅하고 대상을 설정합니다.
            action = null;
            controller.SetTarget(out controller.target);
        }

        public void OnStart()
        {
            // 디버프 상태에 진입하면 AI상태를 디버프 상태로 변경합니다.
            controller.currentState = AIState.Debuff;
            // 공격, 이동, 유휴 애니메이션을취소합니다.
            controller.animator.ResetTrigger("Attack");
            controller.animator.ResetTrigger("Move");
            controller.animator.ResetTrigger("Idle");
            controller.StopAttack();
            switch (controller.battleStatus.currentDebuff)
                // 컨트롤러의 현재 디버프를 확인합니다.
            {
                case DebuffType.Stern:
                    // 각 디버프에 맞는 액션을 넣어주고 애니메이션을 세팅합니다.
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
            // 액션을 수행합니다.
            action.Invoke(controller, controller.target);
        }

        // 기절액션
        public void SternAction(Controller character, Controller target)
        {
            // 그자리에서 가만히
        }

        // 유혹 액션
        public void TemptationAction(Controller character, Controller target)
        {
            // 대상에게 이동합니다.
            movement.MoveNav(target.transform);
        }

        // 공포 액션
        public void FearAction(Controller character, Controller target)
        {
            // 타겟에서 멀어지도록 위치값을 세팅하고 이동시킵니다.
            Vector3 runDirection = (character.transform.position - target.transform.position).normalized * character.battleStatus.status.AttackRange * 2;
            movement.MovePos(character.transform.position + runDirection);

        }
    }
}
