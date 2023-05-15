using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Core;


namespace RPG.Character.Equipment
{
    public class Weapon : Equipment
    {
        public int weaponApparenceID;

        public int attackDamage;
        public float attackSpeed;
        public float attackRange;
        public float movementSpeed;
        public float criticalChance;
        public float criticalDamage;
        public float attackChance;

        public weaponHandleType handleType;

        // Encapsulation
        public int AttackDamage
        {
            get
            {
                int value = 0;
                value += attackDamage;

                if (prefix != null)
                {
                    value += (prefix as WeaponIncant).attackDamage;
                }

                if (suffix != null)
                {
                    value += (suffix as WeaponIncant).attackDamage;
                }

                value += (int)(attackDamage * reinforceCount * 0.1);

                return value;
            }

            set => attackDamage = value;
        }
        public float AttackSpeed 
        {
            get
            {
                float value = 0f;
                value += attackSpeed;

                if (prefix != null)
                {
                    value += (prefix as WeaponIncant).attackSpeed;
                }

                if (suffix != null)
                {
                    value += (suffix as WeaponIncant).attackSpeed;
                }

                return value;
            }

            set => attackSpeed = value; 
        }
        public float AttackRange 
        {
            get
            {
                float value = 0f;
                value += attackRange;

                if (prefix != null)
                {
                    value += (prefix as WeaponIncant).attackRange;
                }

                if (suffix != null)
                {
                    value += (suffix as WeaponIncant).attackRange;
                }

                return value;
            }

            set => attackRange = value; 
        }
        public float MovementSpeed 
        {
            get
            {
                float value = 0f;
                value += movementSpeed;

                if (prefix != null)
                {
                    value += (prefix as WeaponIncant).movementSpeed;
                }

                if (suffix != null)
                {
                    value += (suffix as WeaponIncant).movementSpeed;
                }

                return value;
            }
            set => movementSpeed = value; 
        }
        public float CriticalChance 
        {
            get
            {
                float value = 0f;
                value += criticalChance;

                if (prefix != null)
                {
                    value += (prefix as WeaponIncant).criticalChance;
                }

                if (suffix != null)
                {
                    value += (suffix as WeaponIncant).criticalChance;
                }

                return value;
            }

            set => criticalChance = value; 
        }
        public float CriticalDamage 
        {
            get
            {
                float value = 0f;
                value += criticalDamage;

                if (prefix != null)
                {
                    value += (prefix as WeaponIncant).criticalDamage;
                }

                if (suffix != null)
                {
                    value += (suffix as WeaponIncant).criticalDamage;
                }

                return value;
            }

            set => criticalDamage = value; 
        }
        public float AttackChance
        { 
            get
            {
                float value = 0f;
                value += attackChance;

                if (prefix != null)
                {
                    value += (prefix as WeaponIncant).attackChance;
                }

                if (suffix != null)
                {
                    value += (suffix as WeaponIncant).attackChance;
                }

                return value;
            }

            set => attackChance = value; 
        }

        public Weapon(Weapon weapon) : base(weapon)
        {
            weaponApparenceID = weapon.weaponApparenceID;
            attackDamage = weapon.attackDamage;
            attackSpeed = weapon.attackSpeed;
            attackRange = weapon.attackRange;
            movementSpeed = weapon.movementSpeed;
            criticalChance = weapon.criticalChance;
            criticalDamage = weapon.criticalDamage;
            attackChance = weapon.attackChance;

            prefix = weapon.prefix;
            suffix = weapon.suffix;

            reinforceCount = weapon.reinforceCount;

            handleType = weapon.handleType;
        }

        public Weapon(WeaponData data) : base(data)
        {
            weaponApparenceID = data.weaponApparenceID;
            AttackDamage = data.attackDamage;
            AttackSpeed = data.attackSpeed;
            AttackRange = data.attackRange;
            MovementSpeed = data.movementSpeed;
            CriticalChance = data.criticalChance;
            CriticalDamage = data.criticalDamage;
            AttackChance = data.attackChance;

            handleType = data.weaponHandleType;
        }

        public override void ChangeData(EquipmentData data)
        {
            if (!(data is WeaponData))
            {
                Debug.LogError("잘못된 데이타 형식입니다.");
            }

            base.ChangeData(data);
            weaponApparenceID = (data as WeaponData).weaponApparenceID;
            AttackDamage = (data as WeaponData).attackDamage;
            AttackSpeed = (data as WeaponData).attackSpeed;
            AttackRange = (data as WeaponData).attackRange;
            MovementSpeed = (data as WeaponData).movementSpeed;
            CriticalChance = (data as WeaponData).criticalChance;
            CriticalDamage = (data as WeaponData).criticalDamage;
            AttackChance = (data as WeaponData).attackChance;
        }
    }
}