using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;
using RPG.Battle.Core;

/*
 * 체인 투사체 효과 이펙트 클래스
 */

namespace RPG.Battle.Ability
{
    public class ChainProjectileAbility : ChainAbility
    {
        [SerializeField] float speed;                   // 투사체 속도
        [SerializeField] float distanceCheck;           // 거리 체크 
        [SerializeField] int hitChainAbilityID = -1;      // 투사체가 적중할 경우 추가 효과 이펙트 ID

        EnemyController currentTarget;  // 현재 대상

        int chainHitCount = 0; // 현재 체인 횟수

        // 대상을 설정합니다.
        public override void SetTarget(EnemyController target)
        {
            base.SetTarget(target);
            currentTarget = targetList[0];
        }

        public override void ReleaseAbility()
        {
            base.ReleaseAbility();

        }

        void Update()
        {
            // 투사체가 대상을 바라보고 대상위치로 이동합니다.
            transform.LookAt(currentTarget.transform.position + abilityPositionOffset);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, currentTarget.transform.position + abilityPositionOffset) < distanceCheck)
                // 대상이 투사체 거리에 맞을위치에 있다면
            {
                if (hitChainAbilityID != -1)
                    // 추가 체인 효과 이펙트가 있다면
                {
                    // 오브젝트 풀에서 꺼내옵니다.
                    BattleManager.ObjectPool.GetAbility(hitChainAbilityID, this.transform, chainAction, null, Space.World);
                }
                // 적중 이벤트를 호출합니다.
                hitAction.Invoke(currentTarget.battleStatus);
                chainHitCount++;
                // 체인 적중 카운트가 타겟 리스트 카운트와 동일하다면
                if (targetList.Count == chainHitCount)
                {
                    chainHitCount = 0;
                    targetList.Clear();
                    // 효과 이펙트를 반환합니다.
                    BattleManager.ObjectPool.ReturnAbility(this);
                    return;
                }

                // 현재 대상을 타겟리스트의 다음대상으로 변경합니다.
                currentTarget = targetList[chainHitCount];
            }
        }
    }
}