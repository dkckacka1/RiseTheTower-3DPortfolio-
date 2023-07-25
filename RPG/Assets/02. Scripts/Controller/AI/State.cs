using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using RPG.Battle.Control;

/*
 * 상태의 기본 정의
 */

namespace RPG.Battle.AI
{
    public abstract class State
    {
        protected Controller controller;    // 현재 컨트롤러

        public State(Controller controller)
        {
            this.controller = controller;
        }
    }
}
