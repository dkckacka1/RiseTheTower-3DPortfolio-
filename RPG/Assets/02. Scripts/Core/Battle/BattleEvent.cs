using RPG.Character.Status;
using UnityEngine.Events;

/*
 * 전투시 사용될 이벤트들을 모아놓은 클래스
 */

namespace RPG.Battle.Event
{
    // 공격시 이벤트
    public class AttackEvent : UnityEvent<BattleStatus, BattleStatus> { }
    // 피격받을 시 이벤트
    public class TakeDamageEvent : UnityEvent<BattleStatus, BattleStatus> { }
    // 초당 이벤트
    public class PerSecondEvent : UnityEvent<BattleStatus> { }
    // 치명타 공격 시 이벤트
    public class CriticalAttackEvent : UnityEvent<BattleStatus, BattleStatus> { }
    // 이동 시 이벤트
    public class MoveEvent : UnityEvent<BattleStatus> { }
}