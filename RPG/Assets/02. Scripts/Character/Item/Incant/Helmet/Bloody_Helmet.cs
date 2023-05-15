using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Bloody_Helmet : HelmetIncant
    {
        public Bloody_Helmet(HelmetIncantData data) : base(data)
        {
        }

        public override void criticalAttackEvent(BattleStatus player, BattleStatus enemy)
        {
            enemy.TakeDebuff(DebuffType.Bloody, 10f);
        }
    }
}