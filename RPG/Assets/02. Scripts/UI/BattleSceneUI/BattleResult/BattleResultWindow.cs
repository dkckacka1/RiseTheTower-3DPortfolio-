using RPG.Battle.Core;
using RPG.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Battle.UI
{
    public class BattleResultWindow : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI floorText;
        [SerializeField] GetItemText energyTxt;
        [SerializeField] GetItemText gachaTxt;
        [SerializeField] GetItemText reinforceTxt;
        [SerializeField] GetItemText incantTxt;

        [Header("ChangeBattleState")]
        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] Button reStartBtn;
        [SerializeField] TextMeshProUGUI btnText;

        public void InitUI(int floor)
        {
            UpdateUI(floor);
        }

        public void UpdateUI(int floor)
        {
            floorText.text = $"현재 층 수 : \t{floor}층";
        }

        public void ShowDefeatUI()
        {
            int consumeEnergy = GameManager.Instance.stageDataDic[BattleManager.Instance.currentStageFloor].ConsumEnergy;
            titleText.text = "전투 결과";
            btnText.text = $"현재 층\n재도전 (-{consumeEnergy})";
            reStartBtn.onClick.RemoveAllListeners();
            reStartBtn.onClick.AddListener(() =>
            {
                GameManager.Instance.UserInfo.energy -= consumeEnergy;
                BattleManager.Instance.ReStartBattle();
            });

            if (GameManager.Instance.UserInfo.energy < consumeEnergy)
            {
                reStartBtn.interactable = false;
            }
            else
            {
                reStartBtn.interactable = true;
            }
        }

        public void ShowPauseUI()
        {
            titleText.text = "전투 중지";
            btnText.text = "전투로\n돌아가기";
            reStartBtn.onClick.RemoveAllListeners();
            reStartBtn.onClick.AddListener(() => { BattleManager.Instance.ReturnBattle(); });
        }

        public void UpdateEnergy()
        {
            if (BattleManager.Instance.gainEnergy == 0)
            {
                return;
            }

            energyTxt.GainText(BattleManager.Instance.gainEnergy, 0.5f, () => { Debug.Log("energyTxt end"); });
        }

        public void UpdateGacha()
        {
            if (BattleManager.Instance.gainGacha == 0)
            {
                return;
            }

            gachaTxt.GainText(BattleManager.Instance.gainGacha, 0.5f, () => { Debug.Log("gachaTxt end"); });
        }

        public void UpdateIncant()
        {
            if (BattleManager.Instance.gainIncant == 0)
            {
                return;
            }

            incantTxt.GainText(BattleManager.Instance.gainIncant, 0.5f, () => { Debug.Log("incantTxt end"); });
        }

        public void UpdateReinforce()
        {
            if (BattleManager.Instance.gainReinforce == 0)
            {
                return;
            }

            reinforceTxt.GainText(BattleManager.Instance.gainReinforce, 0.5f, () => { Debug.Log("reinforceTxt end"); });
        }

    }

}