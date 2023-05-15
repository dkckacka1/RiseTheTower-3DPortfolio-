using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public class DistanceCheckDecorator : DecoratorNode
    {
        public float minimumDistance = 0f;
        public Transform myTransform ,targetTransform;

        public override void OnStart()
        {
            //minimumDistance = context.stats.attackRange;
            myTransform = context.transform;
            targetTransform = context.controller.target.transform;
            context.animator.SetBool("isMove", true);
        }

        public override void OnStop()
        {
            Debug.Log($"{myTransform.name}와 {targetTransform.name}의 거리가 가깝습니다.");
            context.animator.SetTrigger("Dead");
            //context.stats.IsDead = true;
        }

        public override Stats OnUpdate()
        {
            float distance = Vector3.Distance(myTransform.position, targetTransform.position);

            if (distance <= minimumDistance)
            {
                return Stats.SUCCESS;
            }

            child.Update();
            return Stats.UPDATE;
        }
    }
}