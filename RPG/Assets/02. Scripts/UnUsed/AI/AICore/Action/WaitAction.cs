using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public class WaitAction : ActionNode
    {
        public float duration;
        float startTime;

        public WaitAction(float duration = 1f)
        {
            this.duration = duration;
        }

        public override void OnStart()
        {
            startTime = Time.time;
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            if (Time.time - startTime > duration)
            {
                return Stats.SUCCESS;
            }

            return Stats.UPDATE;
        }
    }

}