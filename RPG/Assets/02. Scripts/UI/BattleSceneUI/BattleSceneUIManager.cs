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

/*
 * 전투 UI 매니저 클래스
 */

namespace RPG.Battle.UI
{
    public class BattleSceneUIManager : MonoBehaviour
    {
        [Header("Canvas")]
        public Canvas battleCanvas;             // 전투 UI 캔버스

        [Header("BattleUI")]
        [SerializeField] TextMeshProUGUI floorText; // 현재 층 수 텍스트

        [Header("looting")]
        public Image backpack;  // 가방 UI 이미지

        [Header("PlayerUI")]
        // PlayerUI
        public PlayerHPBar playerHPBarUI;   // 플레이어 체력바 UI
        public DebuffUI playerDebuffUI;     // 플레이어 디버프 UI

        [Header("WindodUI")]
        public Button PauseBtn;             // 멈춤 버튼
        public Canvas resultCanvas;         // 결과창 캔버스
        public BattleResultWindow resultUI; // 전투 결과 UI
        public PauseUI pauseUI;             // 멈춤 UI

        [Header("AbilityButton")]
        public AbilityButton helmetAbility; // 헬멧 액티브 스킬 UI
        public AbilityButton PantsAbility;  // 바지 액티브 스킬 UI

        public Animation endingAnimation;   // 엔딩 애니메이션

        private void Awake()
        {
            // 초기화합니다.
            StartCoroutine(SetEvent());
        }

        // 전투 매니저 클래스에 접근이 될때 까지 대기하는 코루틴
        IEnumerator SetEvent()
        {
            // 전투 매니저가 생성될 때 까지 대기합니다.
            while (true)
            {
                if (BattleManager.Instance != null)
                {
                    break;
                }
                yield return null;
            }
            
            // 각 전투 상태 이벤트 버스에 이벤트를 등록합니다.
            BattleManager.Instance.SubscribeEvent(BattleSceneState.Win, () =>
            // 전투 승리 시
            {
                // 멈춤 버튼을 비활성화합니다.
                InitResultBtn(false);
            });

            BattleManager.Instance.SubscribeEvent(BattleSceneState.Defeat, () =>
            // 전투 패배 시
            {
                // 멈춤 버튼을 비활성화합니다.
                InitResultBtn(false);
                // 결과창의 현재 층수를 업데이트 합니다.
                resultUI.InitUI(BattleManager.Instance.currentStageFloor);
                // 패배 결과 UI를 보여줍니다.
                ShowResultUI(BattleSceneState.Defeat);
            });

            BattleManager.Instance.SubscribeEvent(BattleSceneState.Battle, () =>
            // 전투 시
            {
                // 멈춤 버튼을 활성화 합니다.
                InitResultBtn(true);
            });

            BattleManager.Instance.SubscribeEvent(BattleSceneState.Pause, () =>
            // 전투 중단 시
            {
                // 멈춤 버튼을 비활성화 합니다.
                InitResultBtn(false);
                // 중단 UI를 보여줍니다.
                pauseUI.Init();
            });

            BattleManager.Instance.SubscribeEvent(BattleSceneState.Ending, () =>
            // 엔딩 시
            {
                // 엔딩 애니메이션을 보여줍니다.
                endingAnimation.Play();
            });
        }

        // ORDER : #7) 전투 시 플레이어 캐릭터의 바지와 헬멧의 인챈트를 확인하여 액티브 스킬을 장착
        // 액티브 스킬을 세팅합니다.
        public void InitAbility(Helmet helmet, Pants pants, BattleStatus status)
        {
            if (helmet.suffix != null && helmet.suffix.isIncantAbility)
                // 헬멧의 접미 인챈트가 있고 인챈트에 따로 효과가 있다면
            {
                HelmetIncant incant = helmet.suffix as HelmetIncant;

                // 인챈트에서 효과 정보를 가지고 와서 세팅합니다.
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
                // 없다면 스킬 UI를 숨겨줍니다.
                helmetAbility.gameObject.SetActive(false);
            }

            if (pants.suffix != null && pants.suffix.isIncantAbility)
                // 바지에 접미 인챈트가 있고 인챈트에 따로 효과가 있다면
            {
                PantsIncant incant = pants.suffix as PantsIncant;

                // 인챈트에서 효과 정보를 가지고 와서 세팅합니다.
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
                // 없다면 스킬 UI를 숨겨줍니다.
                PantsAbility.gameObject.SetActive(false);
            }
        }

        // 현재 층수를 표기합니다.
        public void ShowFloor(int floor)
        {
            floorText.text = $"현재 {floor}층 등반중!";
        }

        // 전투 결과 UI를 표시합니다.
        public void ShowResultUI(BattleSceneState state)
        {
            // 현재 전투 타입에 따라서 멈춤 UI를 표시할지, 패배 UI를 표시할지 결정합니다.
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

        // 결과 UI를 꺼줍니다.
        public void ReleaseResultUI()
        {
            resultCanvas.gameObject.SetActive(false);
        }

        // 멈춤버튼을 세팅합니다.
        public void InitResultBtn(bool isActive)
        {
            PauseBtn.interactable = isActive;
        }
    }
}
