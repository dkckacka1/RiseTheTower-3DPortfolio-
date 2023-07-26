using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/*
 * 스테이지의 적군 정보를 표시하는 UI 클래스
 */

namespace RPG.Stage.UI
{
    public class EnemyInfomationUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI enemyNameTxt;      // 적 이름

        [Header("DropItemlist")]
        [SerializeField] TextMeshProUGUI earnEnergyTxt;     // 드랍할 에너지양
        [SerializeField] TextMeshProUGUI earnGachaTxt;      // 드랍할 수 있는 가챠 티켓
        [SerializeField] TextMeshProUGUI earnIncantTxt;     // 드랍할 수 있는 인챈트 티켓
        [SerializeField] TextMeshProUGUI earnReinforceTxt;  // 드랍할 수 있는 강화 티켓

        // 적 정보를 표시합니다.
        public void ShowEnemyInfomation(EnemyData data, int enemyCount)
        {
            enemyNameTxt.text = data.enemyName;
            if (enemyCount > 1)
            {
                enemyNameTxt.text += $"X {enemyCount}";
            }

            earnEnergyTxt.text = data.dropEnergy.ToString();

            int dropGachaCount = 0;
            int dropIncantCount = 0;
            int dropReinforceCount = 0;

            foreach (var dropData in data.dropitems)
            {
                switch (dropData.itemType)
                {
                    case DropItemType.GachaItemScroll:
                        dropGachaCount++;
                        break;
                    case DropItemType.reinfoceScroll:
                        dropIncantCount++;
                        break;
                    case DropItemType.IncantScroll:
                        dropReinforceCount++;
                        break;
                }
            }

            if (dropGachaCount >= 1)
            {
                earnGachaTxt.text = $"최대 {dropGachaCount}개";
                earnGachaTxt.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                earnGachaTxt.transform.parent.gameObject.SetActive(false);
            }

            if (dropIncantCount >= 1)
            {
                earnIncantTxt.text = $"최대 {dropIncantCount}개";
                earnIncantTxt.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                earnIncantTxt.transform.parent.gameObject.SetActive(false);
            }

            if (dropReinforceCount >= 1)
            {
                earnReinforceTxt.text = $"최대 {dropReinforceCount}개";
                earnReinforceTxt.transform.parent.gameObject.SetActive(true);
            }
            else
            {
                earnReinforceTxt.transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
