using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 갑옷 인챈트 접미 저주
 */

namespace RPG.Character.Equipment
{
    public class Cursed_Armor : ArmorIncant
    {
        public Cursed_Armor(ArmorIncantData data) : base(data)
        {
        }

        public override void TakeDamageEvent(BattleStatus mine, BattleStatus whoHitMe)
        {
            // 피격시 타격한 대상을 5초간 저주에 빠트립니다.
            whoHitMe.TakeDebuff(DebuffType.Curse, 5f);
        }
    }

}