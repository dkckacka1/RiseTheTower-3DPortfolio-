using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Battle.Control;
using RPG.Battle.Behaviour;

/*
 * 추적 상태 클래스
 */

namespace RPG.Battle.AI
{
    public class ChaseState : State,IState
    {
        private Movement movement;  // 이동 행동

        public ChaseState(Controller controller) : base(controller)
        {
            movement = controller.movement;
        }

        public void OnEnd()
        {
            // 이동이 종료되면 이동 애니메이션을 종료하고 네브메쉬를 초기화합니다.
            movement.isMove = false;
            controller.animator.ResetTrigger("Move");
            movement.ResetNav();
        }

        public void OnStart()
        {
            // 이동이 시작되면 AI상태를 추적상태로 변경해주며, 이동 애니메이션으로 변경합니다.
            controller.currentState = AIState.Chase;
            movement.isMove = true;
            controller.animator.SetTrigger("Move");
        }

        public void OnUpdate()
        {
            // 대상위치로 이동합니다.
            movement.MoveNav(controller.target.transform);
        }
    }
}
