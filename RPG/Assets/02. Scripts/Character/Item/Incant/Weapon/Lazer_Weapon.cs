using RPG.Battle.Core;
using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 무기 인챈트 접미 레이저
 */

namespace RPG.Character.Equipment
{
    public class Lazer_Weapon : WeaponIncant
    {
        public Lazer_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            // 공격 시 20 데미지의 관통 레이저를 발사합니다.
            var ability = BattleManager.ObjectPool.GetAbility(6);
            ability.InitAbility(player.transform, HitLazer);
        }

        public void HitLazer(BattleStatus target)
        {
            // 데미지 20
            target.TakeDamage(20);
        }
    }

}