using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Helmet : Equipment
    {
        private int defencePoint;
        private int hpPoint;
        private float decreseCriticalDamage;
        private float evasionCritical;

        // Encapsulation
        public int DefencePoint 
        { 
            get
            {
                int value = 0;
                value += defencePoint;

                if (prefix != null)
                {
                    value += (prefix as HelmetIncant).defencePoint;
                }

                if (suffix != null)
                {
                    value += (suffix as HelmetIncant).defencePoint;
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
                    value += (prefix as HelmetIncant).hpPoint;
                }

                if (suffix != null)
                {
                    value += (suffix as HelmetIncant).hpPoint;
                }
                value += (int)(hpPoint * 0.1 * reinforceCount);

                return value;
            }

            set => hpPoint = value; 
        }
        public float DecreseCriticalDamage 
        {
            get
            {
                float value = 0;
                value += decreseCriticalDamage;

                if (prefix != null)
                {
                    value += (prefix as HelmetIncant).decreseCriticalDamage;
                }

                if (suffix != null)
                {
                    value += (suffix as HelmetIncant).decreseCriticalDamage;
                }

                return value;
            }

            set => decreseCriticalDamage = value; 
        }
        public float EvasionCritical 
        {
            get
            {
                float value = 0;
                value += evasionCritical;

                if (prefix != null)
                {
                    value += (prefix as HelmetIncant).evasionCritical;
                }

                if (suffix != null)
                {
                    value += (suffix as HelmetIncant).evasionCritical;
                }

                return value;
            }

            set => evasionCritical = value; 
        }

        public Helmet(Helmet helmet) : base(helmet)
        {
            DefencePoint = helmet.DefencePoint;
            HpPoint = helmet.HpPoint;
            DecreseCriticalDamage = helmet.DecreseCriticalDamage;
            EvasionCritical = helmet.EvasionCritical;
        }

        public Helmet(HelmetData data) : base(data)
        {
            DefencePoint = data.defencePoint;
            HpPoint = data.hpPoint;
            DecreseCriticalDamage = data.decreseCriticalDamage;
            EvasionCritical = data.evasionCritical;
        }

        public override void ChangeData(EquipmentData data)
        {
            if (!(data is HelmetData))
            {
                Debug.LogError("잘못된 데이타 형식입니다.");
            }

            base.ChangeData(data);
            DefencePoint = (data as HelmetData).defencePoint;
            HpPoint = (data as HelmetData).hpPoint;
            DecreseCriticalDamage = (data as HelmetData).decreseCriticalDamage;
            EvasionCritical = (data as HelmetData).evasionCritical;
        }
    }
}