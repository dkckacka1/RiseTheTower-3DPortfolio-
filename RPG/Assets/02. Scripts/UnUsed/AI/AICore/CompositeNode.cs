using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

namespace RPG.UnUsed
{
    public abstract class CompositeNode : Node
    {
        [SerializeReference] public List<Node> children = new List<Node>();


        public List<Node> GetChilds()
        {
            return children;
        }

        public override void Init(Context context)
        {
            this.context = context;
            this.stats = Stats.UPDATE;
            foreach (var child in children)
            {
                child.Init(context);
                child.stats = Stats.UPDATE;
            }
        }
    }
}