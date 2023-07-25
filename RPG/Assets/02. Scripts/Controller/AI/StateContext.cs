using System;
using System.Collections.Generic;
using RPG.Battle.Control;
using UnityEngine;

/*
 * 상태 변경을 관리하는 컨텍스트 클래스
 */

namespace RPG.Battle.AI
{
    public class StateContext
    {
        public IState CurrentState  // 현재 상태
        {
            get; set;
        }
        private readonly Controller controller; // 현재 컨트롤러

        // 기본 생성자
        public StateContext(Controller controller)
        {
            this.controller = controller;
        }

        public void Update()
        {
            // 현재 상태가 있다면 현재 상태를 진행합니다.
            if(CurrentState != null)
                CurrentState.OnUpdate();
        }

        // 새로운 상태를 등록한다.
        public void SetState(IState state)
        {
            if (CurrentState == state)
                // 현재 상태와 동일한 상태가 들어온다면 리턴
            {
                return;
            }

            if(CurrentState != null)
                // 현재상태와 다른 상태 혹은 null 값이 들어왔다면
                // 현재 상태의 종료 메서드를 호출합니다.
                CurrentState.OnEnd();

            // 상태를 변경해줍니다.
            CurrentState = state;

            // 변경된 상태의 시작 메서드를 호출합니다.
            CurrentState.OnStart();
        }

    }
}
