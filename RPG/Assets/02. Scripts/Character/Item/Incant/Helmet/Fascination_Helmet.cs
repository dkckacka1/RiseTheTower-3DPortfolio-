using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RPG.Battle.Core;
using RPG.Character.Status;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Fascination_Helmet : HelmetIncant
    {
        public Fascination_Helmet(HelmetIncantData data) : base(data)
        {
            skillCoolTime = 10f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            var enemy = BattleManager.Instance.liveEnemies.Where(enemy => enemy).OrderByDescending(enemy => Vector3.Distance(player.transform.position, enemy.transform.position));

            enemy.First().battleStatus.TakeDebuff(DebuffType.Temptation, 5f);
        }
    }
}
