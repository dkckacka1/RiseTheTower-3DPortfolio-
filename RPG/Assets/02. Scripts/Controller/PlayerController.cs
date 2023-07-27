using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.AI;
using RPG.Battle.Behaviour;
using RPG.Character.Status;
using RPG.Character.Equipment;
using UnityEngine.Events;

/*
 * 플레이어 캐릭터의 컨트롤러
 */

namespace RPG.Battle.Control
{
    public class PlayerController : Controller
    {
        public override void SetUp()
        {
            base.SetUp();
            // 세팅 시 장비 인챈트에 따른 이벤트를 구독해줍니다.
            AddBattleEvent();
            BattleManager.Instance.livePlayer = this;
        }

        // 장비 인챈트에 효과가 있는 경우 이벤트를 구독해줍니다.
        public void AddBattleEvent()
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

        // 가장 가까운 적을 지정합니다.
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

        // 엔딩시 행동입니다.
        protected override void Ending()
        {
            base.Ending();
            // 모든 디버프를 제거하고 승리 애니메이션을 취합니다.
            battleStatus.RemoveAllDebuff();
            animator.SetTrigger("Win");
        }
    }
}