using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;
using RPG.Battle.Ability;
using RPG.Battle.Control;

namespace RPG.Character.Equipment
{
    public class Lighting_Pants : PantsIncant
    {
        public Lighting_Pants(PantsIncantData data) : base(data)
        {
            skillCoolTime = 2f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            var nearlyTarget = BattleManager.Instance.ReturnNearDistanceController<EnemyController>(player.transform);
            var ability = BattleManager.ObjectPool.GetAbility(3, nearlyTarget.transform, (status) => { status.TakeDamage(10); });
            (ability as ChainAbility).SetTarget(nearlyTarget);
            player.StartCoroutine((ability as ChainAbility).delayCoroutine());
        }


    }
}
