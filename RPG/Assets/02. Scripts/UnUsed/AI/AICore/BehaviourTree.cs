using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

namespace RPG.UnUsed
{
    public abstract class BehaviourTree : MonoBehaviour
    {
        public Node rootNode;
        public Stats rootStats = Stats.UPDATE;

        public Context context;

        public void InitNode()
        {
            context = new Context(this.gameObject);
            rootStats = Stats.UPDATE;
            SetRootNode();
            if (rootNode != null)
            {
                rootNode.Init(this.context);
            }
        }

        public void Play()
        {
            if (rootNode != null && rootStats == Stats.UPDATE)
            {
                rootStats = rootNode.Update();
            }
        }

        public abstract void SetRootNode();
    }

}