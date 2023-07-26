using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Battle.Core;

/*
 * 전투 플레이어의 액티브 스킬 버튼 UI
 */

namespace RPG.Battle.UI
{
    public class AbilityButton : MonoBehaviour
    {
        public float abilityCoolTime = 10f;     // 액티브 스킬의 기본 쿨타입
        public float currentCoolTime;           // 현재 스킬 쿨타임

        public Button AbilityBtn;                               // 현재 스킬 버튼
        [SerializeField] Image AbilityIconImage;                // 스킬 이미지
        [SerializeField] TextMeshProUGUI AbilityCoolTimeText;   // 스킬 쿨타임 텍스트
        [SerializeField] Image abilityCoolTimeImage;            // 스킬 쿨타임 이미지

        [SerializeField] bool canUse;   // 스킬을 사용가능한지 여부

        // UI를 초기화합니다.
        public void Init(Sprite sprite, float coolTime)
        {
            AbilityIconImage.sprite = sprite;
            abilityCoolTime = coolTime;
        }

        // 액티브 스킬을 사용합니다.
        public void UseAbility()
        {
            if (BattleManager.Instance.currentBattleState != BattleSceneState.Battle)
                // 전투중이 아니라면 리턴
                return;

            if (canUse)
                // 스킬을 사용할 수 있는 상태라면
            {
                // 스킬을 사용하고 쿨타임체크를 시작한다.
                StartCoroutine(CheckCoolTime());
            }
        }

        // ORDER : #9) 스킬 쿨타임을 보여주는 UI
        // 스킬 쿨타임 체크를 시작합니다.
        public IEnumerator CheckCoolTime()
        {
            // 스킬 쿨타임을 세팅합니다.
            SetCool();
            while (true)
            {
                if (BattleManager.Instance.currentBattleState == BattleSceneState.Battle)
                    // 현재 전투 중이라면 
                {
                    // 쿨타임이미지의 fillAmount를 감소시킵니다.
                    abilityCoolTimeImage.fillAmount -= Time.deltaTime / abilityCoolTime;
                    // 현재 쿨타임을 감소시킵니다.
                    currentCoolTime -= Time.deltaTime;
                    // 쿨타임 텍스트를 업데이트 합니다.
                    AbilityCoolTimeText.text = currentCoolTime.ToString("N1");
                    if (abilityCoolTimeImage.fillAmount <= 0)
                        // 쿨타임이 끝났다면 반복문 종료
                    {
                        break;
                    }
                }
                yield return null;
            }
            // 스킬 사용할 수 있도록 세팅
            CanSkill();
        }

        // 현재 스킬을 사용할 수 있도록 세팅합니다.
        public void CanSkill()
        {
            canUse = true;
            abilityCoolTimeImage.gameObject.SetActive(false);
            AbilityCoolTimeText.gameObject.SetActive(false);
            AbilityBtn.interactable = true;
        }

        // 쿨타임체크를 세팅합니다.
        private void SetCool()
        {
            canUse = false;
            abilityCoolTimeImage.fillAmount = 1;
            currentCoolTime = abilityCoolTime;
            abilityCoolTimeImage.gameObject.SetActive(true);
            AbilityCoolTimeText.gameObject.SetActive(true);
            AbilityBtn.interactable = false;
        }
    }
}