using RPG.Battle.Core;
using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Lazer_Weapon : WeaponIncant
    {
        public Lazer_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            var ability = BattleManager.ObjectPool.GetAbility(6);
            ability.InitAbility(player.transform, HitLazer);
        }

        public void HitLazer(BattleStatus target)
        {
            Debug.Log("히트레이저");
            target.TakeDamage(20);
        }
    }

}