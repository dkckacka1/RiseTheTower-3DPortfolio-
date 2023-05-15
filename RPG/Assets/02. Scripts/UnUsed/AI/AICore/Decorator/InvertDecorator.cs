using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;

namespace RPG.UnUsed
{
    /// <summary>
    /// 하위 노드가 SUCCESS 일시 FAILURE
    /// 하위 노드가 FAILURE 일시 SUCCESS
    /// 하위 노드가 UPDATE 일시 UPDATE
    /// </summary>
    public class InvertDecorator : DecoratorNode
    {
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            switch (child.Update())
            {
                case Stats.FAILURE:
                    return Stats.SUCCESS;
                case Stats.SUCCESS:
                    return Stats.FAILURE;
            }

            return Stats.UPDATE;
        }
    }
}
