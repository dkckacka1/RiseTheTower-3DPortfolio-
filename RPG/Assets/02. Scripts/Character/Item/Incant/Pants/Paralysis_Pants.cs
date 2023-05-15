using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;

namespace RPG.Character.Equipment
{
    public class Paralysis_Pants : PantsIncant
    {
        public Paralysis_Pants(PantsIncantData data) : base(data)
        {
            skillCoolTime = 40f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            foreach (var enemy in BattleManager.Instance.liveEnemies)
            {
                enemy.battleStatus.TakeDebuff(DebuffType.Paralysis, 5f);
            }
        }
    }

}