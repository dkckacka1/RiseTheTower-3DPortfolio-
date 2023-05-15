using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace RPG.Battle.UI
{
    public class PlayerHPBar : HPBar
    {
        public TextMeshProUGUI hpText;

        private int maxHp;

        public override void ChangeCurrentHP(int currentHp)
        {
            base.ChangeCurrentHP(currentHp);
            hpText.text = $"{currentHp}  /  {maxHp}";

        }

        public override void InitHpSlider(int maxHp)
        {
            base.InitHpSlider(maxHp);
            this.maxHp = maxHp;
            hpText.text = $"{maxHp}  /  {maxHp}";
        }
    } 
}
