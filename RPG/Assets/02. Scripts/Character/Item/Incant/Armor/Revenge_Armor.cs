using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Revenge_Armor : ArmorIncant
    {
        public Revenge_Armor(ArmorIncantData data) : base(data)
        {
        }

        public override void TakeDamageEvent(BattleStatus mine, BattleStatus whoHitMe)
        {
            mine.StartCoroutine(Revenge(mine, 2f));
        }

        public IEnumerator Revenge(BattleStatus battleStatus, float time)
        {
            battleStatus.status.AttackDamage += 5;
            yield return new WaitForSeconds(time);
            battleStatus.status.AttackDamage -= 5;
        }
    }
}