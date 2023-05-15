using RPG.Character.Status;
using UnityEngine.Events;

namespace RPG.Battle.Event
{
    public class AttackEvent : UnityEvent<BattleStatus, BattleStatus> { }
    public class TakeDamageEvent : UnityEvent<BattleStatus, BattleStatus> { }
    public class PerSecondEvent : UnityEvent<BattleStatus> { }
    public class CriticalAttackEvent : UnityEvent<BattleStatus, BattleStatus> { }
    public class MoveEvent : UnityEvent<BattleStatus> { }
}