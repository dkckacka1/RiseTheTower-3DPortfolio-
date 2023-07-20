using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Ability;
using RPG.Battle.Control;
using RPG.Battle.Core;
using RPG.Character.Status;

/*
 * 무기 인챈트 접두 전류의
 */

namespace RPG.Character.Equipment
{
    public class Electronic_Weapon : WeaponIncant
    {
        public Electronic_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            // 공격시 최대 4명까지 전류를 흐르게하여 데미지를 입힙니다.
            var nearlyTarget = BattleManager.Instance.ReturnNearDistanceController<EnemyController>(player.transform);
            var ability = BattleManager.ObjectPool.GetAbility(4, player.transform, (status) => 
            {
                // 대상이 이펙트에 맞는다면 바로 폭발 이펙트 보여줍니다.
                BattleManager.ObjectPool.GetAbility(5, status.transform, (status) =>
                {
                }, null, Space.World);
                status.TakeDamage(15); 
            });

            // 다음 가까운 적으로 세팅합니다.
            (ability as ChainProjectileAbility).SetTarget(nearlyTarget);
        }
    }
}
