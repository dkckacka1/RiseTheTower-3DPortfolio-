using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Battle.Core;

/*
 * 투사체 효과 이펙트 클래스
 */

namespace RPG.Battle.Ability
{
    public class ProjectileAbility : Ability
    {
        [SerializeField] float speed;               // 투사체 속도
        [SerializeField] bool isPiercing = false;   // 투사체 관통 여부

        [SerializeField] int hitAbilityID = -1;      // 투사체가 적중했을 때 추가 효과 이펙트 ID

        public override void InitAbility(Transform startPos, UnityAction<BattleStatus> hitAction, UnityAction<BattleStatus> chainAction = null, Space space = Space.Self)
        {
            // 투사체의 방향을 투사체를 사출하는 대상이 바라보고있는 방향으로 조정합니다.
            this.transform.rotation = startPos.rotation;
            base.InitAbility(startPos, hitAction, chainAction, space);
        }

        // Update is called once per frame
        void Update()
        {
            // 정면으로 투사체를 이동시킵니다.
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
            // 투사체의 트리거 반경내에 진입한 콜라이더가 있다면
        {
            var enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                var enemyStatus = enemyController.battleStatus;
                if (enemyStatus != null)
                    // 해당 콜라이더가 적컨트롤러를 가지고 있다면
                {
                    // 적중 이벤트를 호출합니다.
                    hitAction.Invoke(enemyStatus);
                    if (hitAbilityID >= 0)
                        // 적중 효과 이펙트가 존재하면
                    {
                        // 오브젝트 풀에서 효과 이펙트를 가져옵니다.
                        BattleManager.ObjectPool.GetAbility(hitAbilityID, transform, chainAction);
                    }

                    if (isPiercing == false)
                        // 관통이 안되는 투사체라면
                    {
                        // 오브젝트 풀에 반환합니다.
                        BattleManager.ObjectPool.ReturnAbility(this);
                    }
                }
            }

        }
    }

}