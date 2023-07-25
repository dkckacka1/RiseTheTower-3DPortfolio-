using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Character.Status;

/*
 * 전투 캐릭터 UI 클래스
 */

namespace RPG.Battle.UI
{
    public abstract class CharacterUI : MonoBehaviour
    {
        public Canvas battleCanvas;     // 전투 캔버스
        public BattleStatus status;     // 캐릭터의 스텟

        [Header("HPUI")]
        public HPBar hpBar; // 캐릭터의 체력바

        [Header("DebuffUI")]
        public DebuffUI debuffUI;   // 디버프 UI

        [Header("BattleText")]
        public Vector3 battleTextOffset;    // 전투 텍스트가 나타날 오프셋

        private void Awake()
        {
            // 초기화합니다.
            status = GetComponent<BattleStatus>();
            SetUp();
        }
        public virtual void SetUp()
        {
            battleCanvas = BattleManager.BattleUI.battleCanvas;
        }

        // UI를 세팅합니다.
        public virtual void Init()
        {
            if (hpBar != null)
            {
                hpBar.gameObject.SetActive(true);
                hpBar.InitHpSlider(status.status.MaxHp);
                debuffUI.ResetAllDebuff();
            }
        }

        // UI를 꺼줍니다.
        public virtual void ReleaseUI()
        {
            if (hpBar != null)
            {
                hpBar.gameObject.SetActive(false);
            }
        }

        // 체력바를 업데이트 합니다.
        public void UpdateHPUI(int currentHP)
        {
            if(hpBar != null)
            {
                hpBar.ChangeCurrentHP(currentHP);
            }
        }

        // 전투 텍스트를 표시합니다.
        public void TakeDamageText(string damage, DamagedType type = DamagedType.Normal)
        {
            // 현재 자신의 위치 + 오프셋 위치에 표기합니다.
            BattleManager.ObjectPool.GetText(damage.ToString(), this.transform.position + battleTextOffset, type);
        }
    }
}