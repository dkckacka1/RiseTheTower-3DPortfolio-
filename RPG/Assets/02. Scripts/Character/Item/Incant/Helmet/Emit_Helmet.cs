using RPG.Battle.Core;
using RPG.Character.Status;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 헷멧 인챈트 접미 발산
 */


namespace RPG.Character.Equipment
{
    public class Emit_Helmet : HelmetIncant
    {
        public Emit_Helmet(HelmetIncantData data) : base(data)
        {
            skillCoolTime = 1f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            // 바라보고 있는 방향으로 폭발성 구체를 던집니다.
            var ability = BattleManager.ObjectPool.GetAbility(7);
            ability.InitAbility(player.transform, hitAction, chainAction);
        }

        // 주위 대상에게 데미지 20
        private void chainAction(BattleStatus target)
        {
            target.TakeDamage(20);
        }

        // 맞은 대상만 데미지 30
        private void hitAction(BattleStatus target)
        {
            target.TakeDamage(30);
        }
    }

}