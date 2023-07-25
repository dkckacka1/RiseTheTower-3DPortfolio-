using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using RPG.Character.Status;
using RPG.Battle.Control;
using RPG.Battle.Event;
using UnityEngine.Events;

/*
 * 이동 행동 정의 클래스
 */

namespace RPG.Battle.Behaviour
{
    public class Movement
    {
        public bool canMove = true; // 이동 가능한지 여부
        public bool isMove;                     // 현재 이동중인지 여부

        public MoveEvent moveEvent;             // 이동중 호출할 이벤트
        public IEnumerator moveEventCorotine;   // 이동 코루틴

        BattleStatus character; // 현재 캐릭터
        NavMeshAgent nav;       // 현재 오브젝트의 네브에이전트
        float attackRange;  // 공격 범위

        // 현재 캐릭터와 네브를 세팅합니다.
        public Movement(BattleStatus character, NavMeshAgent nav)
        {
            this.character = character;
            this.nav = nav;
            moveEvent = new MoveEvent();
            moveEventCorotine = MoveEvent();
        }

        // 이동 이벤트를 호출합니다.
        public void AddMoveEvent(UnityAction<BattleStatus> action)
        {
            moveEvent.AddListener(action);
        }

        // 네브를 초기화합니다. 만약 이동중이였다면 이동 취소
        public void ResetNav()
        {
            if (nav.enabled == true)
                nav.ResetPath();
        }

        // 대상 트랜스폼의 위치로 이동합니다.
        public void MoveNav(Transform target)
        {
            nav.SetDestination(target.position);
        }

        // 타겟 벡터로 이동합니다.
        public void MoveNav(Vector3 target)
        {
            nav.SetDestination(target);
        }

        //대상 트랜스폼위치로 이동합니다. 네브매쉬 사용 X
        public void MovePos(Transform target)
        {
            Vector3 movementVector = new Vector3(target.position.x, 0, target.position.z);
            character.transform.LookAt(movementVector);
            //transform.Translate(Vector3.forward * status.movementSpeed * Time.deltaTime);
        }

        //대상 위치로 이동합니다. 네브매쉬 사용 X
        public void MovePos(Vector3 target)
        {
            character.transform.LookAt(target);
            character.transform.Translate(Vector3.forward * character.status.MovementSpeed * Time.deltaTime);
        }

        // 공격범위이상으로 다가가지 않도록
        // 대상 트랜스폼과의 위치 값을 계산합니다.
        public bool MoveDistanceResult(Transform target)
        {
            return Vector3.Distance(target.transform.position, this.character.transform.position) > character.status.AttackRange;
        }

        // 이동 시 호출할 이벤트 코루틴
        IEnumerator MoveEvent()
        {
            while (true)
            {
                if (isMove)
                {
                    moveEvent.Invoke(character);
                }
                yield return new WaitForSeconds(1f);
            }
        }
    }
}