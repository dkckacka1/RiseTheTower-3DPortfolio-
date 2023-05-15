using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Battle.Control;

namespace RPG.Battle.AI
{
    public interface IState
    {
        public void OnUpdate();
        public void OnStart();
        public void OnEnd();
    }
}
