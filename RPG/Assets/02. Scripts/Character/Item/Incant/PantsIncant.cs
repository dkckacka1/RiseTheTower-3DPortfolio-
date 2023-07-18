using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 바지 인챈트의 인챈트 클래스
 */

namespace RPG.Character.Equipment
{
    public class PantsIncant : Incant
    {
        public int hpPoint;              // 체력 수치
        public int defencePoint;         // 방어도
        public float movementSpeed;      // 이동 속도

        public float skillCoolTime; // 스킬 쿨타임

        // 바지 데이터에 맞는 바지 인챈트 생성자
        public PantsIncant(PantsIncantData data) : base(data)
        {
            hpPoint = data.hpPoint;
            defencePoint = data.defencePoint;
            movementSpeed = data.movementSpeed;

    }

        // 이동시 발휘할 효과
        public virtual void MoveEvent(BattleStatus player)
        {
            Debug.Log("MoveEvent is Nothing");
        }

        // 바지 액티브 스킬
        public virtual void ActiveSkill(BattleStatus player)
        {
            Debug.Log("Pants ActiveSkill is Nothing");
        }

        // 바지 인챈트의 증가 스텟 설명
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

            if (movementSpeed > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"이동속도(+{movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"이동속도(+{movementSpeed})");
                }
            }

            return returnStr;
        }

        // 바지 인챈트의 감소 스텟 설명
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

            if (movementSpeed < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"이동속도({movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"이동속도({movementSpeed})");
                }
            }

            return returnStr;
        }
    }
}