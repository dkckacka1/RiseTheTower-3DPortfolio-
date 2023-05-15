using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;

namespace RPG.UnUsed
{
    public class FindControllerAction : ActionNode 
    {
        Controller controller;

        public override void OnStart()
        {
            controller = context.controller.target;
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            if (controller == null)
            {
                // 현재 타겟이 없음
                //context.controller.SetChaseState();
                if (controller == null)
                {
                    // 새로 찾아도 없음
                    return Stats.FAILURE;
                }
            }

            // 타겟이 있음
            return Stats.SUCCESS;
        }
    }

}