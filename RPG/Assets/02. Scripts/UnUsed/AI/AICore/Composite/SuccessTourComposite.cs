using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public class SuccessTourComposite : CompositeNode
    {
        public Stats statsTour;

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            statsTour = Stats.SUCCESS;
            foreach (var node in children)
            {
                Stats stats = node.Update();

                if (stats == Stats.UPDATE)
                {
                    statsTour = Stats.UPDATE;
                }

                if (stats == Stats.FAILURE)
                {
                Debug.Log("¾øÀ½!");
                    return Stats.FAILURE;
                }
            }

            return statsTour;
        }
    }

}