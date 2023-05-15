using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public class SequenceComposite : CompositeNode
    {
        int current;

        public override void OnStart()
        {
            current = 0;
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            if (children.Count == 0)
                return Stats.SUCCESS;

            var Node = children[current];

            switch (Node.Update())
            {
                case Stats.UPDATE:
                    return Stats.UPDATE;
                case Stats.FAILURE:
                    return Stats.FAILURE;
                case Stats.SUCCESS:
                    break;
            }

            current++;
            return children.Count == current ? Stats.SUCCESS : Stats.UPDATE;
        }
    }
}