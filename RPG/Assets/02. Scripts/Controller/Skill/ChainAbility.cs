using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Core;
using RPG.Battle.Control;
using RPG.Character.Status;

/*
 * 체인 효과 이펙트 클래스
 */

namespace RPG.Battle.Ability
{
    public class ChainAbility : Ability
    {
        [SerializeField] protected float chainDelay = 0.1f; // 체인 딜레이
        [SerializeField] protected float chainRange = 1f; // 체인 반경
        [SerializeField] protected int chainCount = 3; // 타겟의 갯수

        protected List<EnemyController> targetList = new List<EnemyController>();   // 현재 타겟된 리스트

        public override void ReleaseAbility()
        {
            base.ReleaseAbility();
        }

        // 대상을 설정합니다.
        public virtual void SetTarget(EnemyController target)
        {
            // 타겟된 대상
            var currentTarget = target;
            // 또 타겟되지 않도록 미리 리스트에 넣어준다.
            targetList.Add(currentTarget);
            for (int i = 0; i < chainCount; i++)
                // 체인 횟수 만큼 대상을 추가 선정합니다.
            {
                // 가장 가까운 대상을 선정하여 타겟 리스트에 넣어줍니다.
                EnemyController nextTarget;
                if (TryCheckNearlyTarget(currentTarget, out nextTarget))
                {
                    targetList.Add(nextTarget);
                    // 현재 대상을 다음대상으로 설정
                    currentTarget = nextTarget;
                }
                else
                // 더이상 대상이 없다면 리턴
                {
                    return;
                }

            }
        }

        // 가장 가까운 대상을 찾아 반환합니다.
        public bool TryCheckNearlyTarget(EnemyController target, out EnemyController nextTarget)
        {
            // 가장 가까운 적 순으로 정렬합니다.
            BattleManager.Instance.liveEnemies.Sort((enemy1, enemy2) =>
            {
                float distance1 = Vector3.Distance(enemy1.transform.position, target.transform.position);
                float distance2 = Vector3.Distance(enemy2.transform.position, target.transform.position);

                if (distance1 > distance2)
                    return 1;
                else
                    return -1;
            });

            foreach (var enemycontroller in BattleManager.Instance.liveEnemies)
            {
                // 가장 가까운 순으로 정렬한뒤 타겟리스트에 없는 대상이고 죽지 않았다면
                // 타겟 리스트에 넣어줍니다.
                if (targetList.Find(enemy => enemycontroller == enemy) == null)
                {
                    if(!enemycontroller.battleStatus.isDead)
                    {
                        nextTarget = enemycontroller;
                        return true;
                    }
                }
            }

            nextTarget = null;
            return false;
        }

        public IEnumerator delayCoroutine()
        {
            foreach (var target in targetList)
            {
                var newEffect = BattleManager.ObjectPool.GetAbility(this.abilityID, target.transform, hitAction);
                newEffect.transform.position = target.transform.position;
                newEffect.particle.Play();
                hitAction.Invoke(target.battleStatus);
                yield return new WaitForSeconds(chainDelay);
            }

            targetList.Clear();
        }
    }
}
