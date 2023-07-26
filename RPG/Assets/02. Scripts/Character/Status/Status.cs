using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 모든 캐릭터의 기본 스탯 클래스
 */

namespace RPG.Character.Status 
{
    public class Status : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] int maxHp = 0;                         // 최대 체력

        [Header("Attack")]
        [SerializeField] float attackRange = 0f;                // 공격 범위
        [SerializeField] int attackDamage = 0;                  // 공격력
        [SerializeField] float attackSpeed = 0f;                // 공격 속도
        [SerializeField] float criticalChance = 0f;             // 치명타 확률
        [SerializeField] float criticalDamage = 0f;             // 치명타 피해량
        [SerializeField] float attackChance = 0f;               // 명중률

        [Header("Defence")]
        [SerializeField] int defencePoint = 0;                  // 방어 수치
        [SerializeField] float evasionPoint = 0f;               // 회피율
        [SerializeField] float decreseCriticalDamage = 0f;      // 치명타 데미지 감소율
        [SerializeField] float evasionCritical = 0f;            // 치명타 회피율

        [Header("Movement")]
        [SerializeField] float movementSpeed = 0f;              // 이동 속도

        // Encapsulation
        public int MaxHp 
        {
            get => maxHp;
            set => maxHp = value; 
        }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }
        // 공격속도는 최소 공격속도가 있습니다.
        public float AttackSpeed 
        {
            get
            {
                if (attackSpeed < Constant.minimumAttackSpeed)
                {
                    return Constant.minimumAttackSpeed;
                }
                else
                {
                    return attackSpeed;
                }
            }
            set => attackSpeed = value;
        }

        public float CriticalChance { get => criticalChance; set => criticalChance = value; }
        public float CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
        public float AttackChance { get => attackChance; set => attackChance = value; }
        public int DefencePoint { get => defencePoint; set => defencePoint = value; }
        public float EvasionPoint { get => evasionPoint; set => evasionPoint = value; }
        public float DecreseCriticalDamage { get => decreseCriticalDamage; set => decreseCriticalDamage = value; }
        public float EvasionCritical { get => evasionCritical; set => evasionCritical = value; }
        // 이동속도는 최소 이동속도가 있습니다.
        public float MovementSpeed 
        { 
            get
            {
                if (movementSpeed < Constant.minimumMovementSpeed)
                {
                    return Constant.minimumMovementSpeed;
                }
                else
                {
                    return movementSpeed;
                }
            }
            set => movementSpeed = value;
        }
    }
}

