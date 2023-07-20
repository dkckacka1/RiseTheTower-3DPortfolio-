using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

/*
 * 무기 인챈트 접두 둔탁한
 */

namespace RPG.Character.Equipment
{
    public class Dull_Weapon : WeaponIncant
    {
        public Dull_Weapon(WeaponIncantData data) : base(data)
        {
        }

        public override void AttackEvent(BattleStatus player, BattleStatus enemy)
        {
            // 공격시 30% 확률로 상대를 2초간 기절시킵니다.
            if (MyUtility.ProbailityCalc(70f, 0f, 100f))
            {
                enemy.TakeDebuff(DebuffType.Stern, 2f);
            }
        }
    }

}