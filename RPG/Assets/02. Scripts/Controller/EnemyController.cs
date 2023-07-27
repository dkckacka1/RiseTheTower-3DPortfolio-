
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.Behaviour;
using RPG.Battle.AI;

/*
 * 적 캐릭터의 컨트롤러 클래스
 */

namespace RPG.Battle.Control
{
    public class EnemyController : Controller
    {
        // 대상을 지정합니다.
        public override bool SetTarget(out Controller controller)
        {
            // 가까운 플레이어 컨트롤러를 지정합니다.
            controller = BattleManager.Instance.ReturnNearDistanceController<PlayerController>(transform);
            if(controller != null)
            {
                this.target = controller;
                attack.SetTarget(controller.battleStatus);
            }

            return (controller != null);
        }

        // 아이템을 루팅합니다.
        public void LootingItem()
        {
            BattleManager.Instance.LootingItem(this);
        }
    }
}
