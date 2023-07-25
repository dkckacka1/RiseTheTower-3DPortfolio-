using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Battle.Control;

/*
 * 상태 패턴의 상태 인터페이스
 */

namespace RPG.Battle.AI
{
    public interface IState
    {
        public void OnUpdate(); // 상태가 지속중일 때 메서드
        public void OnStart();  // 상태가 시작할 때 메서드
        public void OnEnd();    // 상태가 종료될 때 메서드
    }
}
