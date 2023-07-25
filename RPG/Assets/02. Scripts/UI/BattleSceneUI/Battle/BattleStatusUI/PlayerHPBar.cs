using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/*
 * 플레이어 전용 HP 체력바 UI 클래스
 */

namespace RPG.Battle.UI
{
    public class PlayerHPBar : HPBar
    {
        public TextMeshProUGUI hpText;  // 현재 체력 텍스트

        private int maxHp;  // 최대 체력 수치

        // 체력이 변화했을 떄 메서드
        public override void ChangeCurrentHP(int currentHp)
        {
            base.ChangeCurrentHP(currentHp);
            hpText.text = $"{currentHp}  /  {maxHp}";

        }

        // 체력바를 초기화합니다.
        public override void InitHpSlider(int maxHp)
        {
            base.InitHpSlider(maxHp);
            this.maxHp = maxHp;
            hpText.text = $"{maxHp}  /  {maxHp}";
        }
    } 
}
