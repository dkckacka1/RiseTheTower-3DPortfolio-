using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 갑옷 인챈트 접미 복수
 */

namespace RPG.Character.Equipment
{
    public class Revenge_Armor : ArmorIncant
    {
        public Revenge_Armor(ArmorIncantData data) : base(data)
        {
        }

        public override void TakeDamageEvent(BattleStatus mine, BattleStatus whoHitMe)
        {
            // 피격 시 2초간 공격력이 5 상승합니다.
            mine.StartCoroutine(Revenge(mine, 2f));
        }

        // 5초동안 공격력 상승이 유지되도록 합니다.
        public IEnumerator Revenge(BattleStatus battleStatus, float time)
        {
            battleStatus.status.AttackDamage += 5;
            yield return new WaitForSeconds(time);
            battleStatus.status.AttackDamage -= 5;
        }
    }
}