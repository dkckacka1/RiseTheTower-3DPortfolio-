using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UnUsed
{
    public class DebugAction : ActionNode
    {
        [SerializeReference] public string str;

        public DebugAction(string str)
        {
            this.str = str;
        }

        public override void OnStart()
        {
            Debug.Log($"Start Log => {str}");
        }

        public override void OnStop()
        {
            Debug.Log($"Stop Log => {str}");
        }

        public override Stats OnUpdate()
        {
            Debug.Log($"Update Log => {str}");
            return Stats.SUCCESS;
        }
    }
}