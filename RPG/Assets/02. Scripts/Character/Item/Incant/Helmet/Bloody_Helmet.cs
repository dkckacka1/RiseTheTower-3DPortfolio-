using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 헷멧 인챈트 접두 피나는
 */

namespace RPG.Character.Equipment
{
    public class Bloody_Helmet : HelmetIncant
    {
        public Bloody_Helmet(HelmetIncantData data) : base(data)
        {
        }

        public override void criticalAttackEvent(BattleStatus player, BattleStatus enemy)
        {
            // 치명타 타격 시 대상을 10초간 출혈시킵니다.
            enemy.TakeDebuff(DebuffType.Bloody, 10f);
        }
    }
}