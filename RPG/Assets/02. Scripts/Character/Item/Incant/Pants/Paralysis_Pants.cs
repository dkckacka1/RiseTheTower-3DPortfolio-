using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;

/*
 * ���� ��æƮ ���� ����
 */

namespace RPG.Character.Equipment
{
    public class Paralysis_Pants : PantsIncant
    {
        public Paralysis_Pants(PantsIncantData data) : base(data)
        {
            // ��ų ��Ÿ���� 15��
            skillCoolTime = 15f;
        }

        public override void ActiveSkill(BattleStatus player)
        {
            // ��� ���� 5�ʰ� �����ŵ�ϴ�.
            foreach (var enemy in BattleManager.Instance.liveEnemies)
            {
                enemy.battleStatus.TakeDebuff(DebuffType.Paralysis, 5f);
            }
        }
    }

}