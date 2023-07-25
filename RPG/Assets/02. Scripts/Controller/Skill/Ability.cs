using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Core;
using RPG.Character.Status;
using RPG.Main.Audio;

/*
 * 효과 이펙트의 기본 추상 클래스
 */

namespace RPG.Battle.Ability
{
    public abstract class Ability : MonoBehaviour
    {
        public int abilityID;               // 효과 이펙트 ID
        public string abilityName;          // 효과 이펙트 이름
        [Space()]
        [TextArea()]
        public string abilityDesc;          // 효과 이펙트 설명
        public float abilityTime;           // 효과 이펙트 시간
        public string AbilitySoundName;     // 효과 이펙트 오디오 이름

        [HideInInspector] public ParticleSystem particle;   // 파티클 시스템
        protected UnityAction<BattleStatus> hitAction;      // 이펙트가 대상에게 적중시 액션
        protected UnityAction<BattleStatus> chainAction;    // 이펙트의 연결 액션

        // 처음에 스킬이 어디서 나타날 것인지 
        public Vector3 abilityPositionOffset;

        private void Awake()
        {
            particle = GetComponent<ParticleSystem>();
        }

        // 효과 이펙트가 활성화 되면 사운드를 출력하고 반환 코루틴을 시작합니다.
        protected virtual void OnEnable()
        {
            PlaySound();
            StartCoroutine(ReleaseTimer());
        }

        // 효과 이펙트를 초기화 합니다.
        public virtual void InitAbility(Transform startPos,
            UnityAction<BattleStatus> hitAction = null,
            UnityAction<BattleStatus> chainAction = null,
            Space space = Space.Self)
        {
            // 이펙트 생성위치를 대상의 위치로 할지 로컬위치로할지 결정합니다.
            if (space == Space.Self)
            {
                this.transform.localPosition = startPos.localPosition;
            }
            else
            {
                this.transform.localPosition = startPos.position;
            }


            // 대상위치로 이동후 offset만큼 추가 이동합니다.
            this.transform.Translate(abilityPositionOffset);
            this.hitAction = hitAction;
            this.chainAction = chainAction;
        }

        // 효가 이펙트를 오브젝트 풀에 반환합니다.
        public virtual void ReleaseAbility()
        {
            BattleManager.ObjectPool.ReturnAbility(this);
        }

        // 반환 시간만큼 대기 후 오브젝트 풀에 반환합니다.
        public IEnumerator ReleaseTimer()
        {
            yield return new WaitForSeconds(abilityTime);
            ReleaseAbility();
        }

        // 소리를 재생합니다.
        public void PlaySound()
        {
            if (AbilitySoundName != string.Empty)
            {
                AudioManager.Instance.PlaySoundOneShot(AbilitySoundName);
            }
        }
    }
}