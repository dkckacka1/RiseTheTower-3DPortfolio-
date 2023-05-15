using RPG.Character.Status;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Character.Equipment
{
    public class Regenerative_Armor : ArmorIncant
    {
        public Regenerative_Armor(ArmorIncantData data) : base(data)
        {
        }

        public override void PerSecEvent(BattleStatus status)
        {
            status.Heal(1);
        }
    }

}