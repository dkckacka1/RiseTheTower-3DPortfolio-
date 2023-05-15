using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.AI;
using RPG.Battle.Behaviour;
using RPG.Character.Status;
using RPG.Character.Equipment;
using UnityEngine.Events;

namespace RPG.Battle.Control
{
    public class PlayerController : Controller
    {
        public override void SetUp()
        {
            base.SetUp();
            AddAttackEvent();
            BattleManager.Instance.livePlayer = this;
        }

        public void AddAttackEvent()
        {
            PlayerStatus status = (battleStatus.status as PlayerStatus);

            if (status.currentWeapon.prefix != null)
            {
                attack.AddAttackEvent((status.currentWeapon.prefix as WeaponIncant).AttackEvent);
            }

            if (status.currentWeapon.suffix != null)
            {
                attack.AddAttackEvent((status.currentWeapon.suffix as WeaponIncant).AttackEvent);
            }

            if (status.currentArmor.prefix != null)
            {
                battleStatus.AddPerSecAction((status.currentArmor.prefix as ArmorIncant).PerSecEvent);
            }

            if (status.currentArmor.suffix != null)
            {
                battleStatus.AddTakeDamageAction((status.currentArmor.suffix as ArmorIncant).TakeDamageEvent);
            }

            if (status.currentHelmet.prefix != null)
            {
                attack.AddCriticalAttackEvent((status.currentHelmet.prefix as HelmetIncant).criticalAttackEvent);
            }

            if (status.currentPants.prefix != null)
            {
                movement.AddMoveEvent((status.currentPants.prefix as PantsIncant).MoveEvent);
            }

            BattleManager.BattleUI.InitAbility(status.currentHelmet, status.currentPants, battleStatus);
        }

        public override void DeadEvent()
        {
            base.DeadEvent();
        }

        public override bool SetTarget(out Controller controller)
        {
            controller = BattleManager.Instance.ReturnNearDistanceController<EnemyController>(transform);
            if (controller != null)
            {
                this.target = controller;
                attack.SetTarget(controller.battleStatus);
            }

            return (controller != null);
        }

        protected override void Ending()
        {
            base.Ending();
            battleStatus.RemoveAllDebuff();
            animator.SetTrigger("Win");
        }
    }
}