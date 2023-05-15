using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public class SelectorComposite : CompositeNode
    {
        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            foreach (var node in children)
            {
                Stats stats = node.Update();

                switch (stats)
                {
                    case Stats.UPDATE:
                        return Stats.UPDATE;
                    case Stats.SUCCESS:
                        return Stats.SUCCESS;
                }
            }

            return Stats.FAILURE;
        }
    }
}
