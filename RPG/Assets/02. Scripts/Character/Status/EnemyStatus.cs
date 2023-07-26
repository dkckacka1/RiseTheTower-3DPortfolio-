using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;

/*
 * 적군의 스탯 클래스
 */

namespace RPG.Character.Status
{
    public class EnemyStatus : Status
    {
        public int enemyID;         // 적 데이터 ID입니다.
        public int apperenceNum;    // 외형정보입니다.

        // 적데이터를 통해 스텟을 세팅합니다.
        public void ChangeEnemyData(EnemyData data)
        {
            enemyID = data.ID;
            apperenceNum = data.apperenceNum;

            MaxHp = data.maxHp;
            AttackDamage = data.attackDamage;
            AttackRange = data.attackRange;
            AttackSpeed = data.attackSpeed;
            CriticalChance = data.criticalChance;
            CriticalDamage = data.criticalDamage;
            AttackChance = data.attackChance;

            DefencePoint = data.defencePoint;
            EvasionPoint = data.evasionPoint;
            DecreseCriticalDamage = data.decreseCriticalDamage;
            EvasionCritical = data.evasionCritical;

            MovementSpeed = data.movementSpeed;
        }
    }
}