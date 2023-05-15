using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Battle.Core;
using RPG.Battle.Ability;
using RPG.Character.Equipment;
using RPG.Character.Status;

namespace RPG.Battle.UI
{
    public class BattleSceneUIManager : MonoBehaviour
    {
        [Header("Canvas")]
        public Canvas battleCanvas; 

        [Header("BattleUI")]
        [SerializeField] TextMeshProUGUI floorText;

        [Header("looting")]
        public Image backpack;

        [Header("PlayerUI")]
        // PlayerUI
        public PlayerHPBar playerHPBarUI;
        public DebuffUI playerDebuffUI;

        [Header("WindodUI")]
        public Button PauseBtn;
        public Canvas resultCanvas;
        public BattleResultWindow resultUI;
        public PauseUI pauseUI;

        [Header("AbilityButton")]
        public AbilityButton helmetAbility;
        public AbilityButton PantsAbility;

        public Animation endingAnimation;

        private void Awake()
        {
            StartCoroutine(SetEvent());
        }

        IEnumerator SetEvent()
        {
            while (true)
            {
                if (BattleManager.Instance != null)
                {
                    break;
                }
                yield return null;
            }

            BattleManager.Instance.SubscribeEvent(BattleSceneState.Win, () =>
            {
                InitResultBtn(false);
            });

            BattleManager.Instance.SubscribeEvent(BattleSceneState.Defeat, () =>
            {
                InitResultBtn(false);
                resultUI.InitUI(BattleManager.Instance.currentStageFloor);
                ShowResultUI(BattleSceneState.Defeat);
            });

            BattleManager.Instance.SubscribeEvent(BattleSceneState.Battle, () =>
            {
                InitResultBtn(true);
            });

            BattleManager.Instance.SubscribeEvent(BattleSceneState.Pause, () =>
            {
                InitResultBtn(false);
                pauseUI.Init();
            });

            BattleManager.Instance.SubscribeEvent(BattleSceneState.Ending, () =>
            {
                endingAnimation.Play();
            });
        }

        public void InitAbility(Helmet helmet, Pants pants, BattleStatus status)
        {
            if (helmet.suffix != null && helmet.suffix.isIncantAbility)
            {
                HelmetIncant incant = helmet.suffix as HelmetIncant;

                helmetAbility.gameObject.SetActive(true);
                helmetAbility.Init(helmet.suffix.abilityIcon, incant.skillCoolTime);
                helmetAbility.AbilityBtn.onClick.AddListener(() => 
                {
                    if (BattleManager.Instance.currentBattleState != BattleSceneState.Battle) return;

                    incant.ActiveSkill(status);
                });
            }
            else
            {
                helmetAbility.gameObject.SetActive(false);
            }

            if (pants.suffix != null && pants.suffix.isIncantAbility)
            {
                PantsIncant incant = pants.suffix as PantsIncant;

                PantsAbility.gameObject.SetActive(true);
                PantsAbility.Init(pants.suffix.abilityIcon, incant.skillCoolTime);
                PantsAbility.AbilityBtn.onClick.AddListener(() => 
                {
                    if (BattleManager.Instance.currentBattleState != BattleSceneState.Battle) return;

                    incant.ActiveSkill(status);
                });
            }
            else
            {
                PantsAbility.gameObject.SetActive(false);
            }
        }

        public void ShowFloor(int floor)
        {
            floorText.text = $"현재 {floor}층 등반중!";
        }

        public void ShowResultUI(BattleSceneState state)
        {
            switch (state)
            {
                case BattleSceneState.Pause:
                    {
                        resultUI.ShowPauseUI();
                        break;
                    }

                case BattleSceneState.Defeat:
                    {
                        resultUI.ShowDefeatUI();
                        break;
                    }

                case BattleSceneState.Win:
                    {
                        break;
                    }
            }

            resultCanvas.gameObject.SetActive(true);
        }

        public void ReleaseResultUI()
        {
            resultCanvas.gameObject.SetActive(false);
        }

        public void InitResultBtn(bool isActive)
        {
            PauseBtn.interactable = isActive;
        }
    }
}
