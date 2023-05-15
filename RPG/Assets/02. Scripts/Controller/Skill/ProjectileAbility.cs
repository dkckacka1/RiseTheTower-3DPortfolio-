using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Battle.Core;

namespace RPG.Battle.Ability
{
    public class ProjectileAbility : Ability
    {
        [SerializeField] float speed;
        [SerializeField] bool isPiercing = false;

        [SerializeField] int hitEffectID = -1;

        public override void InitAbility(Transform startPos, UnityAction<BattleStatus> hitAction, UnityAction<BattleStatus> chainAction = null, Space space = Space.Self)
        {
            this.transform.rotation = startPos.rotation;
            base.InitAbility(startPos, hitAction, chainAction, space);
        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            var enemyController = other.GetComponent<EnemyController>();
            if (enemyController != null)
            {
                var enemyStatus = enemyController.battleStatus;
                if (enemyStatus != null)
                {
                    hitAction.Invoke(enemyStatus);
                    if (hitEffectID >= 0)
                    {
                        BattleManager.ObjectPool.GetAbility(hitEffectID, transform, chainAction);
                    }

                    if (isPiercing == false)
                    {
                        BattleManager.ObjectPool.ReturnAbility(this);
                    }
                }
            }

        }
    }

}