using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;
using RPG.Battle.Core;

/*
 * 바지 인챈트 접두 공포
 */

namespace RPG.Character.Equipment
{
    public class Fear_Pants : PantsIncant
    {
        public Fear_Pants(PantsIncantData data) : base(data)
        {
            // 스킬 쿨타임은 15초
            skillCoolTime = 15f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            // 공포 이펙트 출력
            var ability = BattleManager.ObjectPool.GetAbility(2, player.transform, Fear);
        }

        public void Fear(BattleStatus character)
        {
            // 주변의 적을 4초 동안 공포에 질리게 합니다.
            if (character.status is EnemyStatus)
            {
                character.TakeDebuff(DebuffType.Fear, 4f);
            }
        }
    }
}