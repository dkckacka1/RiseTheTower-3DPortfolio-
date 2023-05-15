using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RPG.Battle.Control;

namespace RPG.Battle.AI
{
    public abstract class State
    {
        protected Controller controller;

        public State(Controller controller)
        {
            this.controller = controller;
        }
    }
}
