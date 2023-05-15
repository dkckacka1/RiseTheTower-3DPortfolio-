using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Status 
{
    public class Status : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] int maxHp = 0;

        [Header("Attack")]
        [SerializeField] float attackRange = 0f;
        [SerializeField] int attackDamage = 0;
        [SerializeField] float attackSpeed = 0f;
        [SerializeField] float criticalChance = 0f;
        [SerializeField] float criticalDamage = 0f;
        [SerializeField] float attackChance = 0f;

        [Header("Defence")]
        [SerializeField] int defencePoint = 0;
        [SerializeField] float evasionPoint = 0f;
        [SerializeField] float decreseCriticalDamage = 0f;
        [SerializeField] float evasionCritical = 0f;

        [Header("Movement")]
        [SerializeField] float movementSpeed = 0f;

        // Encapsulation
        public int MaxHp 
        {
            get => maxHp;
            set => maxHp = value; 
        }
        public float AttackRange { get => attackRange; set => attackRange = value; }
        public int AttackDamage { get => attackDamage; set => attackDamage = value; }
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

