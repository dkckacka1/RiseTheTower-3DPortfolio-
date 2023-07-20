using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Battle.Core;
using RPG.Character.Status;
using UnityEngine;

/*
 * 헷멧 인챈트 접미 매혹
 */

namespace RPG.Character.Equipment
{
    public class Fascination_Helmet : HelmetIncant
    {
        public Fascination_Helmet(HelmetIncantData data) : base(data)
        {
            // 스킬 쿨타임은 10초
            skillCoolTime = 10f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            // 가장 멀리있는 대상을 5초 동안 유혹합니다.
            var enemyList = BattleManager.Instance.liveEnemies.Where(enemy => enemy).OrderByDescending(enemy => Vector3.Distance(player.transform.position, enemy.transform.position));
            enemyList.First().battleStatus.TakeDebuff(DebuffType.Temptation, 5f);
        }
    }
}
