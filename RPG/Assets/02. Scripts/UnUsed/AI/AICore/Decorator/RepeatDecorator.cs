using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public class RepeatDecorator : DecoratorNode
    {
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            child.Update();
            return Stats.UPDATE;
        }
    }

}