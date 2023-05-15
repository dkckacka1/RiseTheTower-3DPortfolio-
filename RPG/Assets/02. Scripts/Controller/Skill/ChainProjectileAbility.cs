using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;
using RPG.Battle.Core;

namespace RPG.Battle.Ability
{
    public class ChainProjectileAbility : ChainAbility
    {
        [SerializeField] float speed;
        [SerializeField] float distanceCheck;
        [SerializeField] int actionEffectNum = -1;

        EnemyController currentTarget;

        int index = 0;

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
            transform.LookAt(currentTarget.transform.position + abilityPositionOffset);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);

            if (Vector3.Distance(this.transform.position, currentTarget.transform.position + abilityPositionOffset) < distanceCheck)
            {
                if (actionEffectNum != -1)
                {
                    BattleManager.ObjectPool.GetAbility(actionEffectNum, this.transform, chainAction, null, Space.World);
                }
                hitAction.Invoke(currentTarget.battleStatus);
                index++;
                if (targetList.Count == index)
                {
                    index = 0;
                    targetList.Clear();
                    BattleManager.ObjectPool.ReturnAbility(this);
                    return;
                }
                currentTarget = targetList[index];
            }
        }
    }
}