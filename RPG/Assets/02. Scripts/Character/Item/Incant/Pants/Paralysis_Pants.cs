using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;

/*
 * 바지 인챈트 접두 마비
 */

namespace RPG.Character.Equipment
{
    public class Paralysis_Pants : PantsIncant
    {
        public Paralysis_Pants(PantsIncantData data) : base(data)
        {
            // 스킬 쿨타임은 15초
            skillCoolTime = 15f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            // 모든 적을 5초간 마비시킵니다.
            foreach (var enemy in BattleManager.Instance.liveEnemies)
            {
                enemy.battleStatus.TakeDebuff(DebuffType.Paralysis, 5f);
            }
        }
    }

}