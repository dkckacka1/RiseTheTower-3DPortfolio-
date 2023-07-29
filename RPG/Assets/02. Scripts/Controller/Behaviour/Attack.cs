using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Control;
using RPG.Character.Status;
using RPG.Battle.Event;

/*
 * 공격 행동 정의 클래스
 */

namespace RPG.Battle.Behaviour
{
    public class Attack
    {
        public bool canAttack = true;           // 공격할 수 있느지 여부
        public bool isAttack = false;           // 공격중인지 여부
        public float attackDelay = 1;           // 공격 딜레이 
        public float defaultAttackAnimLength;   // 기본 공격 애니메이션 길이

        // AttackEvent
        AttackEvent attackEvent;                    // 공격 시 이벤트
        CriticalAttackEvent criticalAttackEvent;    // 치명타 공격 시 이벤트

        // Component
        BattleStatus character;     // 현재 캐릭터
        BattleStatus target;        // 공격할 대상 캐릭터

        // 공격 행동을 수행할 캐릭터를 넣어줍니다.
        public Attack(BattleStatus character)
        {
            this.character = character; 
            attackEvent = new AttackEvent();
            criticalAttackEvent = new CriticalAttackEvent();
        }

        // 공격 이벤트를 넣어줍니다.
        public void AddAttackEvent(UnityAction<BattleStatus, BattleStatus> action)
        {
            attackEvent.AddListener(action);
        }

        // 치명타 공격 이벤트를 넣어줍니다.
        public void AddCriticalAttackEvent(UnityAction<BattleStatus, BattleStatus> action)
        {
            criticalAttackEvent.AddListener(action);
        }

        // 대상을 설정합니다
        public void SetTarget(BattleStatus target)
        {
            this.target = target;
        }

        // 대상을 공격합니다.
        public void AttackTarget()
        {
            canAttack = false;
        }

        // 대상에게 데미지를 입힙니다.
        public void TargetTakeDamage()
        {
            // 대상이 죽었거나 없다면 리턴
            if (target.isDead) return;
            if (target == null)
            {
                Debug.Log($"타겟이 없지만 AttackAnimEvent가 호출되었습니다.");
                return;
            }

            // 방어도 계산식을 적용합니다.
            float defenceAverage = (target.status.DefencePoint / 300);

            // 최소 피해량은 피해량의 10%
            defenceAverage = 1 - Mathf.Clamp(defenceAverage, 0, 0.9f);

            // 공격합니다.
            if (AttackChangeCalc(character, target))
            // 공격이 적중했다면
            {
                // 공격 이벤트를 호출합니다.
                attackEvent.Invoke(character, target);
                // 대상 피격 이벤트를 호출합니다.
                target.takeDamageEvent.Invoke(target, character);
                if (AttackCriticalCalc(character, target))
                // 치명타가 발생헀다면
                {
                    // 치명타 공격 이벤트를 호출합니다.
                    criticalAttackEvent.Invoke(character, target);
                    // 치명타 데미지를 계산하고 대상에게 피해를 입힙니다.
                    // 전투 텍스트를 치명타 타입으로 변경합니다.
                    int criticalDamage = (int)(character.status.AttackDamage * (1 + character.status.CriticalDamage));
                    target.TakeDamage(DamageCalc(criticalDamage, defenceAverage), DamagedType.Ciritical);
                }
                else
                // 치명타가 발생하지 않았다면
                {
                    // 대상에게 피해를 입힙니다.
                    // 전투 텍스트를 일반 데미지 타입으로 변경합니다.
                    target.TakeDamage(DamageCalc(character.status.AttackDamage, defenceAverage), DamagedType.Normal);
                }
            }
            else
            // 공격이 적중하지 않았다면
            {
                // 전투 텍스트를 MISS 타입으로 변경합니다.
                target.TakeDamage(character.status.AttackDamage, DamagedType.MISS);
            }
        }

        // 데미지 공식을 계산합니다.
        private int DamageCalc(int damage, float defenceAverage)
        {
            //Debug.Log($"공격력 : {damage}\n" +
            //    $"방어율 : {defenceAverage * 100}%" +
            //    $"실제 데미지 수치 : {(int)(damage * defenceAverage)}");

            Debug.Log((int)(damage * defenceAverage));
            return (int)(damage * defenceAverage);
        }

        // 공격 명중을 계산합니다.
        private bool AttackChangeCalc(BattleStatus character, BattleStatus target)
        {
            // 최종 명중률은 명중률 * (1 - 대상 회피율)
            float chance = character.status.AttackChance * (1 - target.status.EvasionPoint);

            float random = Random.Range(0, 1f);

            //Debug.Log($"{character.name}가 공격하여 {target.name}을 타격했습니다.\n" +
            //    $"{character.name}의 적중률 : {character.status.attackChance * 100}%\n" +
            //    $"{target.name}의 회피율 : {target.status.evasionPoint * 100}%\n" +
            //    $"공격이 적중할 확률은 {chance * 100}% 입니다.\n" +
            //    $"주사위는 {random * 100}이 나왔습니다.");

            if (chance > random)
            {
                // 적중 성공
                return true;
            }

            // 적중 실패
            return false;
        }

        // 공격 치명타를 계산합니다.
        private bool AttackCriticalCalc(BattleStatus character, BattleStatus target)
        {
            // 최종 치명타 확률은 치명타 확률 * (1 - 대상 치명타 회피율)
            float criticalChance = character.status.CriticalChance * (1 - target.status.EvasionCritical);

            float random = Random.Range(0, 1f);

            //        Debug.Log($"{character.name}가 공격하여 {target.name}을 타격했습니다.\n" +
            //$"{character.name}의 치명타 적중률 : {character.status.attackChance * 100}%\n" +
            //$"{target.name}의 치명타 회피율 : {target.status.evasionPoint * 100}%\n" +
            //$"공격이 치명타가 발생할 확률은 {criticalChance * 100}% 입니다.\n" +
            //$"치명타 데미지는 {(int)(character.status.attackDamage * (1 + character.status.criticalDamage))} 입니다.\n" +
            //$"주사위는 {random * 100}이 나왔습니다.");

            if (criticalChance > random)
            {
                // 치명타 적중 성공
                return true;
            }

            // 치명타 실패
            return false;
        }

        // 공격 딜레이만큼 대기합니다.
        public IEnumerator WaitAttackDelay()
        {
            isAttack = true;
            yield return new WaitForSeconds(attackDelay);
            canAttack = true;
            isAttack = false;
        }
    }
}