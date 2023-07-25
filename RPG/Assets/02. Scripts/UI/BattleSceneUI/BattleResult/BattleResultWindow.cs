using RPG.Battle.Core;
using RPG.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * 전투 결과 창 UI 클래스
 */

namespace RPG.Battle.UI
{
    public class BattleResultWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI floorText;     // 현재 층 텍스트
        [SerializeField] GetItemText energyTxt;         // 얻은 에너지 텍스트
        [SerializeField] GetItemText gachaTxt;          // 얻은 가챠 티켓 텍스트
        [SerializeField] GetItemText reinforceTxt;      // 얻은 강화 티켓 텍스트
        [SerializeField] GetItemText incantTxt;         // 얻은 인챈트 티켓 텍스트

        [Header("ChangeBattleState")]
        [SerializeField] TextMeshProUGUI titleText; // 결과 텍스트
        [SerializeField] Button reStartBtn;         // 재도전 버튼
        [SerializeField] TextMeshProUGUI btnText;   // 버튼 텍스트

        // UI를 초기화합니다.
        public void InitUI(int floor)
        {
            UpdateUI(floor);
        }

        // 현재 층 수를 표기합니다.
        public void UpdateUI(int floor)
        {
            floorText.text = $"현재 층 수 : \t{floor}층";
        }

        // 패배 UI를 표시합니다.
        public void ShowDefeatUI()
        {
            // 재도전 여부를 확인하기위해 현재 층수의 소비 에너지를 가져옵니다.
            int consumeEnergy = GameManager.Instance.stageDataDic[BattleManager.Instance.currentStageFloor].ConsumEnergy;
            titleText.text = "전투 결과";
            btnText.text = $"현재 층\n재도전 (-{consumeEnergy})";
            reStartBtn.onClick.RemoveAllListeners();
            // 재도전 버튼에 이벤트를 구독합니다.
            reStartBtn.onClick.AddListener(() =>
            {
                GameManager.Instance.UserInfo.energy -= consumeEnergy;
                BattleManager.Instance.ReStartBattle();
            });

            // 현재 유저의 에너지양에 따라 재도전 버튼을 활성화합니다.
            if (GameManager.Instance.UserInfo.energy < consumeEnergy)
            {
                reStartBtn.interactable = false;
            }
            else
            {
                reStartBtn.interactable = true;
            }
        }

        // 멈춤 UI를 표시합니다.
        public void ShowPauseUI()
        {
            titleText.text = "전투 중지";
            btnText.text = "전투로\n돌아가기";
            reStartBtn.onClick.RemoveAllListeners();
            reStartBtn.onClick.AddListener(() => { BattleManager.Instance.ReturnBattle(); });
        }

        // 얻은 에너지를 표기합니다.
        public void UpdateEnergy()
        {
            if (BattleManager.Instance.gainEnergy == 0)
            {
                return;
            }

            energyTxt.GainText(BattleManager.Instance.gainEnergy, 0.5f);
        }

        // 얻은 가챠 티켓을 표기합니다.
        public void UpdateGacha()
        {
            if (BattleManager.Instance.gainGacha == 0)
            {
                return;
            }

            gachaTxt.GainText(BattleManager.Instance.gainGacha, 0.5f);
        }

        // 얻은 인챈트 티켓을 표기합니다.
        public void UpdateIncant()
        {
            if (BattleManager.Instance.gainIncant == 0)
            {
                return;
            }

            incantTxt.GainText(BattleManager.Instance.gainIncant, 0.5f);
        }

        // 얻은 강화 티켓을 표기합니다.
        public void UpdateReinforce()
        {
            if (BattleManager.Instance.gainReinforce == 0)
            {
                return;
            }

            reinforceTxt.GainText(BattleManager.Instance.gainReinforce, 0.5f);
        }

    }

}