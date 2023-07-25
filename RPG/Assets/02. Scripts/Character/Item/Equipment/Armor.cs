using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 갑옷 장비 클래스
 */

namespace RPG.Character.Equipment
{
    public class Armor : Equipment
    {
        private int hpPoint;            // 체력 수치
        private int defencePoint;       // 방어 수치
        private float movementSpeed;    // 이독속도
        private float evasionPoint;     // 회피율

        // 장비의 인챈트가 부여됬다면 인챈트 속성치를 더해서 반환합니다.
        public int HpPoint 
        { 
            get
            {
                int value = 0;
                value += hpPoint;

                if (prefix != null)
                {
                    value += (prefix as ArmorIncant).hpPoint;
                }

                if (suffix != null)
                {
                    value += (suffix as ArmorIncant).hpPoint;
                }

                value += (int)(hpPoint * 0.1 * reinforceCount);
                return value;
            }

            set => hpPoint = value; 
        }
        public int DefencePoint 
        {
            get
            {
                int value = 0;
                value += defencePoint;

                if (prefix != null)
                {
                    value += (prefix as ArmorIncant).defencePoint;
                }

                if (suffix != null)
                {
                    value += (suffix as ArmorIncant).defencePoint;
                }

                value += (int)(defencePoint * 0.1 * reinforceCount);
                return value;
            }

            set => defencePoint = value; 
        }
        public float MovementSpeed 
        {
            get
            {
                float value = 0;
                value += movementSpeed;

                if (prefix != null)
                {
                    value += (prefix as ArmorIncant).movementSpeed;
                }

                if (suffix != null)
                {
                    value += (suffix as ArmorIncant).movementSpeed;
                }

                return value;
            }

            set => movementSpeed = value; 
        }
        public float EvasionPoint 
        {
            get
            {
                float value = 0;
                value += evasionPoint;

                if (prefix != null)
                {
                    value += (prefix as ArmorIncant).evasionPoint;
                }

                if (suffix != null)
                {
                    value += (suffix as ArmorIncant).evasionPoint;
                }

                return value;
            }

            set => evasionPoint = value; 
        }

        public Armor(Armor armor) : base(armor)
        {
            DefencePoint = armor.DefencePoint;
            HpPoint = armor.HpPoint;
            MovementSpeed = armor.MovementSpeed;
            EvasionPoint = armor.EvasionPoint;
        }

        public Armor(ArmorData data) : base(data)
        {
            DefencePoint = data.defencePoint;
            HpPoint = data.hpPoint;
            MovementSpeed = data.movementSpeed;
            EvasionPoint = data.evasionPoint;
        }

        // 갑옷의 데이터를 변경합니다.
        public override void ChangeData(EquipmentData data)
        {
            if (!(data is ArmorData))
            {
                Debug.LogError("잘못된 데이타 형식입니다.");
            }

            base.ChangeData(data);
            DefencePoint = (data as ArmorData).defencePoint;
            HpPoint = (data as ArmorData).hpPoint;
            MovementSpeed = (data as ArmorData).movementSpeed;
            EvasionPoint = (data as ArmorData).evasionPoint;
        }
    } 
}
