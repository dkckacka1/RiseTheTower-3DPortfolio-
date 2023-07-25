using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 헬멧에 붙은 인챈트 클래스
 */

namespace RPG.Character.Equipment
{
    public class HelmetIncant : Incant
    {
        public int hpPoint;                     // 체력 수치
        public int defencePoint;                // 방어도 수치
        public float decreseCriticalDamage;     // 치명타 데미지 감소율
        public float evasionCritical;           // 회피율

        public float skillCoolTime;             // 스킬 쿨타입

        // 헬멧 데이터에 따른 생성자
        public HelmetIncant(HelmetIncantData data) : base(data)
        {
            hpPoint = data.hpPoint;
            defencePoint = data.defencePoint;
            decreseCriticalDamage = data.decreseCriticalDamage;
            evasionCritical = data.evasionCritical;
        }

        // 치명타 공격시 효과
        public virtual void criticalAttackEvent(BattleStatus player, BattleStatus enemy)
        {
        }

        // 헬멧 액티브 스킬
        public virtual void ActiveSkill(BattleStatus player)
        {
        }

        // 헬멧 인챈트의 증가 스텟 설명
        public override string GetAddDesc()
        {
            string returnStr = "";
            if (hpPoint > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"체력(+{hpPoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"체력(+{hpPoint})");
                }
            }

            if (defencePoint > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"방어력(+{defencePoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"방어력(+{defencePoint})");
                }
            }

            if (decreseCriticalDamage > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"치명타데미지감소(+{decreseCriticalDamage * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"치명타데미지감소(+{decreseCriticalDamage * 100}%)");
                }
            }

            if (evasionCritical > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"치명타회피율(+{evasionCritical * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"치명타회피율(+{evasionCritical * 100}%)");
                }
            }

            return returnStr;
        }

        // 헬멧 인챈트의 감소 스텟 설명
        public override string GetMinusDesc()
        {
            string returnStr = "";
            if (hpPoint < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"체력({hpPoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"체력({hpPoint})");
                }
            }

            if (defencePoint < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"방어력({defencePoint})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"방어력({defencePoint})");
                }
            }

            if (decreseCriticalDamage < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"치명타데미지감소({decreseCriticalDamage * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"치명타데미지감소({decreseCriticalDamage * 100}%)");
                }
            }

            if (evasionCritical < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"치명타회피율({evasionCritical * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"치명타회피율({evasionCritical * 100}%)");
                }
            }

            return returnStr;
        }
    }
}