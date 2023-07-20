using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;

/*
 * 헷멧 인챈트 접미 재생
 */

namespace RPG.Character.Equipment
{
    public class Regenerative_Helmet : HelmetIncant
    {
        public Regenerative_Helmet(HelmetIncantData data) : base(data)
        {
            // 스킬 쿨타임은 20초
            skillCoolTime = 20f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            // 액티브 스킬 : 사용 시 체력을 100 회복합니다.
            player.Heal(100);
        }
    }
}
