using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;

namespace RPG.Character.Equipment
{
    public class Regenerative_Helmet : HelmetIncant
    {
        public Regenerative_Helmet(HelmetIncantData data) : base(data)
        {
            skillCoolTime = 20f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            player.Heal(100);
        }
    }
}
