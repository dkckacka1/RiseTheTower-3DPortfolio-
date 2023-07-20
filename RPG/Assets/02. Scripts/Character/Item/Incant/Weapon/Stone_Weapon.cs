using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Battle.Core;

/*
 * 무기 인챈트 접미 돌덩이 
 */

namespace RPG.Character.Equipment
{
    public class Stone_Weapon : WeaponIncant
    {
        public Stone_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            // 공격 시 10 데미지의 돌덩이 화살을 발사합니다.
            var ability = BattleManager.ObjectPool.GetAbility(1);
            ability.InitAbility(player.transform, HitStone );
        }

        public void HitStone(BattleStatus target)
        {
            // 데미지 10
            target.TakeDamage(10);
        }
    }
}