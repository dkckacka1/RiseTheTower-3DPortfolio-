using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Battle.Core;

namespace RPG.Character.Equipment
{
    public class Stone_Weapon : WeaponIncant
    {
        public Stone_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            var ability = BattleManager.ObjectPool.GetAbility(1);
            ability.InitAbility(player.transform, HitStone );
        }

        public void HitStone(BattleStatus target)
        {
            target.TakeDamage(10);
        }
    }
}