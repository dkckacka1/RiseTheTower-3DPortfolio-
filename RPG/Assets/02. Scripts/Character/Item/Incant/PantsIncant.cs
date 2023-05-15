using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class PantsIncant : Incant
    {
        public int hpPoint;
        public int defencePoint;
        public float movementSpeed;

        public float skillCoolTime;

        public PantsIncant(PantsIncantData data) : base(data)
        {
            hpPoint = data.hpPoint;
            defencePoint = data.defencePoint;
            movementSpeed = data.movementSpeed;

    }

        public virtual void MoveEvent(BattleStatus player)
        {
            Debug.Log("MoveEvent is Nothing");
        }

        public virtual void ActiveSkill(BattleStatus player)
        {
            Debug.Log("Pants ActiveSkill is Nothing");
        }

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