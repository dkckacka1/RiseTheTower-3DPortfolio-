using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Ability;
using RPG.Battle.Control;
using RPG.Battle.Core;
using RPG.Character.Status;

namespace RPG.Character.Equipment
{
    public class Electronic_Weapon : WeaponIncant
    {
        public Electronic_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            var nearlyTarget = BattleManager.Instance.ReturnNearDistanceController<EnemyController>(player.transform);
            var ability = BattleManager.ObjectPool.GetAbility(4, player.transform, (status) => 
            {
                BattleManager.ObjectPool.GetAbility(5, status.transform, (status) =>
                {
                }, null, Space.World);
                status.TakeDamage(15); 
            });

            (ability as ChainProjectileAbility).SetTarget(nearlyTarget);
        }
    }
}
