using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : 여기서부터 주석 작성

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