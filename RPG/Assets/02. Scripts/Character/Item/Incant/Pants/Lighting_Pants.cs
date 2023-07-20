using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;
using RPG.Battle.Ability;
using RPG.Battle.Control;

/*
 * 바지 인챈트 접두 번개
 */

namespace RPG.Character.Equipment
{
    public class Lighting_Pants : PantsIncant
    {
        public Lighting_Pants(PantsIncantData data) : base(data)
        {
            // 스킬 쿨타임은 10초
            skillCoolTime = 10f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            // 가장 가까운 적을 포함하여 4명까지 번개를 내리칩니다.
            var nearlyTarget = BattleManager.Instance.ReturnNearDistanceController<EnemyController>(player.transform);
            // 번개 데미지는 10초
            var ability = BattleManager.ObjectPool.GetAbility(3, nearlyTarget.transform, (status) => { status.TakeDamage(10); });
            (ability as ChainAbility).SetTarget(nearlyTarget);
            player.StartCoroutine((ability as ChainAbility).delayCoroutine());
        }


    }
}
