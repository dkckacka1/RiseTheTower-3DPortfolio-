using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 갑옷 인챈트의 인챈트 클래스
 */

namespace RPG.Character.Equipment
{
    public class ArmorIncant : Incant
    {
        public int hpPoint;         // 체력 수치
        public int defencePoint;    // 방어도 수치
        public float movementSpeed; // 이동속도
        public float evasionPoint;  // 회피율

        // 갑옷 데이터에 맞는 갑옷 인챈트 생성자
        public ArmorIncant(ArmorIncantData data) : base(data)
        {
            hpPoint = data.hpPoint;
            defencePoint = data.defencePoint;
            movementSpeed = data.movementSpeed;
            evasionPoint = data.evasionPoint;
        }

        // 초당 발휘될 효과
        public virtual void PerSecEvent(BattleStatus status)
        {
        }

        // 데미지 받을 시 효과
        public virtual void TakeDamageEvent(BattleStatus character, BattleStatus target)
        {
        }

        // 갑옷 인챈트의 증가 스텟 설명
        public override string GetAddDesc()
        {
            string returnStr = string.Empty;

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
                    returnStr = $"이동 속도(+{movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"이동 속도(+{movementSpeed})");
                }
            }

            if (evasionPoint > 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"회피율(+{evasionPoint * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"회피율(+{evasionPoint * 100}%)");
                }
            }

            return returnStr;
        }

        // 갑옷 인챈트의 감소 스텟 설명
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
                    returnStr = $"이동 속도({movementSpeed})";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"이동 속도({movementSpeed})");
                }
            }

            if (evasionPoint < 0)
            {
                if (returnStr == string.Empty)
                {
                    returnStr = $"회피율({evasionPoint * 100}%)";
                }
                else
                {
                    returnStr = string.Join("\n", returnStr, $"회피율({evasionPoint * 100}%)");
                }
            }


            return returnStr;

        }
    }
}