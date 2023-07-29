using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Equipment;
using RPG.Character.Status;

/*
 * 무기 인챈트의 인챈트 클래스
 */

public class WeaponIncant : Incant
{
    public int attackDamage;        // 공격력
    public float attackSpeed;       // 공격 속도
    public float attackRange;       // 공격 범위
    public float movementSpeed;     // 이동 속도
    public float criticalChance;    // 치명타 확률
    public float criticalDamage;    // 치명타 피해율
    public float attackChance;      // 명중률

        // 무기 데이터에 맞는 무기 인챈트 생성자
    public WeaponIncant(WeaponIncantData data) : base(data)
    {
        attackDamage = data.attackDamage;
        attackSpeed = data.attackSpeed;
        attackRange = data.attackRange;
        movementSpeed = data.movementSpeed;
        criticalChance = data.criticalChance;
        criticalDamage = data.criticalDamage;
        attackChance = data.movementSpeed;
    }

    // 공격 시 나올 스킬 효과
    public virtual void AttackEvent(BattleStatus player, BattleStatus enemy)
    {
    }

        // 무기 인챈트의 증가 스텟 설명
    public override string GetAddDesc()
    {
        string returnStr = "";
        if (attackDamage > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"공격력(+{attackDamage})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"공격력(+{attackDamage})");
            }
        }

        if (attackSpeed > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"공격속도(+{attackSpeed})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"공격속도(+{attackSpeed})");
            }
        }

        if (attackRange > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"공격범위(+{attackRange})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"공격범위(+{attackRange})");
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

        if (criticalChance > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"치명타 적중률(+{criticalChance * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"치명타 적중률(+{criticalChance * 100}%)");
            }
        }

        if (criticalDamage > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"치명타 데미지(+{criticalDamage * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"치명타 데미지(+{criticalDamage * 100}%)");
            }
        }

        if (attackChance > 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"적중률(+{attackChance * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"적중률(+{attackChance * 100}%)");
            }
        }

        return returnStr;
    }

        // 무기 인챈트의 감소 스텟 설명
    public override string GetMinusDesc()
    {
        string returnStr = "";
        if (attackDamage < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"공격력({attackDamage})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"공격력({attackDamage})");
            }
        }

        if (attackSpeed < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"공격속도({attackSpeed})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"공격속도({attackSpeed})");
            }
        }

        if (attackRange < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"공격범위({attackRange})";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"공격범위({attackRange})");
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

        if (criticalChance < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"치명타 적중률({criticalChance * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"치명타 적중률({criticalChance * 100}%)");
            }
        }

        if (criticalDamage < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"치명타 데미지({criticalDamage * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"치명타 데미지({criticalDamage * 100}%)");
            }
        }

        if (attackChance < 0)
        {
            if (returnStr == string.Empty)
            {
                returnStr = $"적중률({attackChance * 100}%)";
            }
            else
            {
                returnStr = string.Join("\n", returnStr, $"적중률({attackChance * 100}%)");
            }
        }

        return returnStr;
    }
}
