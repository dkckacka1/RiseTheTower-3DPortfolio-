using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;
using RPG.Battle.Core;

/*
 * 폭발 효과 이펙트 클래스
 */

namespace RPG.Battle.Ability
{
    public class ExplosionAbility : Ability
    {
        public float explosionRange = 1f; // 폭발 반경

        // 이펙트가 활성화 되면
        protected override void OnEnable()
        {
            base.OnEnable();
            // 폭발 반경 내에 있는 컨트롤러 리스트를 만듭니다.
            var list = CheckInsideExplosionController();

            if (hitAction != null)
                // 적중 이벤트가 있다면
            {
                foreach (var controller in list)
                {
                    // 적중 이벤트를 호출합니다.
                    hitAction.Invoke(controller.battleStatus);
                }
            }

            if (chainAction != null)
                // 체인 이벤트가 있다면
            {
                foreach (var controller in list)
                {
                    // 체인 이벤트를 호출합니다.
                    chainAction.Invoke(controller.battleStatus);
                }
            }
        }

        // 반경 내의 컨트롤러 리스트 리턴
        public List<Controller> CheckInsideExplosionController()
        {
            List<Controller> controllerList = new List<Controller>();

            if (BattleManager.Instance.livePlayer != null 
                && Vector3.Distance(this.transform.position, BattleManager.Instance.livePlayer.transform.position) < explosionRange)
                // 플레이어가 살아있고 폭발 반경내에 있다면 리스트에 추가합니다.
            {
                controllerList.Add(BattleManager.Instance.livePlayer);
            }

            foreach (var enemy in BattleManager.Instance.liveEnemies)
                // 모든 살아있는 적을 순회합니다.
            {
                if (BattleManager.Instance.liveEnemies != null
                    && Vector3.Distance(this.transform.position, enemy.transform.position) < explosionRange)
                    // 적이 폭발 반경 내에 있다면 리스트에 추가합니다.
                {
                    controllerList.Add(enemy);
                }
            }

            return controllerList;
        }
    }
}
