using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

namespace RPG.UnUsed
{
    public class UntillFailureRepeatDecorator : DecoratorNode
    {
        public UntillFailureRepeatDecorator(Node child = null)
        {
            this.child = child;
        }



        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            return child.Update() == Stats.FAILURE ? Stats.SUCCESS : Stats.UPDATE;
        }
    }
}