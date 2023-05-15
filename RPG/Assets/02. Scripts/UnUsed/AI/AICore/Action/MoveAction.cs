using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Behaviour;

namespace RPG.UnUsed
{
    public class MoveAction : ActionNode
    {
        Transform target;
        public override void OnStart()
        {
            target = context.controller.target.transform;
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            context.movement.MovePos(target);
            return Stats.SUCCESS;
        }
    }
}