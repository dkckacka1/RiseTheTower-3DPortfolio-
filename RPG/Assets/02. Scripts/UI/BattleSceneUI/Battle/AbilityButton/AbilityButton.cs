using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Battle.Core;

namespace RPG.Battle.UI
{
    public class AbilityButton : MonoBehaviour
    {
        public float abilityCoolTime = 10f;
        public float currentCoolTime;

        public Button AbilityBtn;
        [SerializeField] Image AbilityIconImage;
        [SerializeField] TextMeshProUGUI AbilityCoolTimeText;
        [SerializeField] Image abilityCoolTimeImage;

        [SerializeField] bool canUse;

        public void Init(Sprite sprite, float coolTime)
        {
            AbilityIconImage.sprite = sprite;
            abilityCoolTime = coolTime;
        }

        public void UseAbility()
        {
            if (BattleManager.Instance.currentBattleState != BattleSceneState.Battle)
                return;

            if (canUse)
            {
                StartCoroutine(CheckCoolTime());
            }
        }

        public IEnumerator CheckCoolTime()
        {
            SetCool();
            while (true)
            {
                if (BattleManager.Instance.currentBattleState == BattleSceneState.Battle)
                {
                    abilityCoolTimeImage.fillAmount -= Time.deltaTime / abilityCoolTime;
                    currentCoolTime -= Time.deltaTime;
                    AbilityCoolTimeText.text = currentCoolTime.ToString("N1");
                    if (abilityCoolTimeImage.fillAmount <= 0)
                    {
                        break;
                    }
                }
                yield return null;
            }
            CanSkill();
        }

        public void CanSkill()
        {
            canUse = true;
            abilityCoolTimeImage.gameObject.SetActive(false);
            AbilityCoolTimeText.gameObject.SetActive(false);
            AbilityBtn.interactable = true;
        }

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