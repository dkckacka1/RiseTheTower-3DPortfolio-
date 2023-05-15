using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;

namespace RPG.UnUsed
{
    public class IfDecorator : DecoratorNode
    {
        public delegate bool Function();

        Function function;

        public IfDecorator(Function funcion)
        {
            this.function = funcion;
        }

        public override void OnStart()
        {
        }

        public override void OnStop()
        {
        }

        public override Stats OnUpdate()
        {
            if (function.Invoke())
            {
                return child.Update();
            }

            return Stats.FAILURE;
        }
    }
}
