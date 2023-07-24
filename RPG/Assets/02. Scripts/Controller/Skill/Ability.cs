using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Battle.Core;
using RPG.Character.Status;
using RPG.Main.Audio;

namespace RPG.Battle.Ability
{
    public abstract class Ability : MonoBehaviour
    {
        public int abilityID;
        public string abilityName;
        [Space()]
        [TextArea()]
        public string abilityDesc;
        public float abilityTime;
        public string AbilitySoundName;

        [HideInInspector] public ParticleSystem particle;
        protected UnityAction<BattleStatus> hitAction;
        protected UnityAction<BattleStatus> chainAction;

        private void Awake()
        {
            particle = GetComponent<ParticleSystem>();
        }

        protected virtual void OnEnable()
        {
            PlaySound();
            StartCoroutine(ReleaseTimer());
        }

        // ó���� ��ų�� ��� ��Ÿ�� ���ΰ�?
        public Vector3 abilityPositionOffset;
        public virtual void InitAbility(Transform startPos, UnityAction<BattleStatus> hitAction = null, UnityAction<BattleStatus> chainAction = null, Space space = Space.Self)
        {
            if (space == Space.Self)
            {
                this.transform.localPosition = startPos.localPosition;
            }
            else
            {
                this.transform.localPosition = startPos.position;
            }


            this.transform.Translate(abilityPositionOffset);
            this.hitAction = hitAction;
            this.chainAction = chainAction;
        }

        public virtual void ReleaseAbility()
        {
            BattleManager.ObjectPool.ReturnAbility(this);
        }

        public IEnumerator ReleaseTimer()
        {
            yield return new WaitForSeconds(abilityTime);
            ReleaseAbility();
        }

        public void PlaySound()
        {
            if (AbilitySoundName != string.Empty)
            {
                AudioManager.Instance.PlaySoundOneShot(AbilitySoundName);
            }
        }
    }
}