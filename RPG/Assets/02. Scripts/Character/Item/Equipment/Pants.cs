using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Pants : Equipment
    {
        private int defencePoint;
        private int hpPoint;
        private float movementSpeed;

        // Encapsulation
        public int DefencePoint 
        { 
            get
            {
                int value = 0;
                value += defencePoint;

                if (prefix != null)
                {
                    value += (prefix as PantsIncant).defencePoint;
                }

                if (suffix != null)
                {
                    value += (suffix as PantsIncant).defencePoint;
                }
                value += (int)(defencePoint * 0.1 * reinforceCount);

                return value;
            }

            set => defencePoint = value; 
        }
        public int HpPoint 
        { 
            get
            {
                int value = 0;
                value += hpPoint;

                if (prefix != null)
                {
                    value += (prefix as PantsIncant).hpPoint;
                }

                if (suffix != null)
                {
                    value += (suffix as PantsIncant).hpPoint;
                }
                value += (int)(hpPoint * 0.1 * reinforceCount);

                return value;
            }

            set => hpPoint = value; 
        }
        public float MovementSpeed 
        {
            get
            {
                float value = 0;
                value += movementSpeed;

                if (prefix != null)
                {
                    value += (prefix as PantsIncant).movementSpeed;
                }

                if (suffix != null)
                {
                    value += (suffix as PantsIncant).movementSpeed;
                }

                return value;
            }
            set => movementSpeed = value; 
        }

        public Pants(Pants pants) : base(pants)
        {
            DefencePoint = pants.DefencePoint;
            HpPoint = pants.HpPoint;
            MovementSpeed = pants.MovementSpeed;
        }

        public Pants(PantsData data) : base(data)
        {
            DefencePoint = data.defencePoint;
            HpPoint = data.hpPoint;
            MovementSpeed = data.movementSpeed;
        }

        public override void ChangeData(EquipmentData data)
        {
            if (!(data is PantsData))
            {
                Debug.LogError("잘못된 데이타 형식입니다.");
            }

            base.ChangeData(data);
            DefencePoint = (data as PantsData).defencePoint;
            HpPoint = (data as PantsData).hpPoint;
            MovementSpeed = (data as PantsData).movementSpeed;
        }



    }
}