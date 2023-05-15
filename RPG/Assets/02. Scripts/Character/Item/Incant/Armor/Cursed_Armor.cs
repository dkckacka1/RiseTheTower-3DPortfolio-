using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Cursed_Armor : ArmorIncant
    {
        public Cursed_Armor(ArmorIncantData data) : base(data)
        {
        }

        public override void TakeDamageEvent(BattleStatus mine, BattleStatus whoHitMe)
        {
            whoHitMe.TakeDebuff(DebuffType.Curse, 5f);
        }
    }

}