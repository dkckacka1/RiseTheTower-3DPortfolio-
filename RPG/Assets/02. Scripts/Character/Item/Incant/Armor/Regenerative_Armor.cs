using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 갑옷 인챈트 접두 재생의
 */

namespace RPG.Character.Equipment
{
    public class Regenerative_Armor : ArmorIncant
    {
        public Regenerative_Armor(ArmorIncantData data) : base(data)
        {
        }

        public override void PerSecEvent(BattleStatus status)
        {
            // 매 초마다 체력이 1회복됩니다.
            status.Heal(1);
        }
    }

}