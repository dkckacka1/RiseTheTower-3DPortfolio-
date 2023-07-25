using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;
using RPG.Character.Status;

/*
 * 적 캐릭터 UI 클래스
 */

public class EnemyCharacterUI : CharacterUI
{
    public GameObject battleStatusUIPrefab;                     // 캐릭터의 UI 프리팹
    public GameObject battleStatusUI;                           // 캐릭터 UI 오브젝트
    public Vector3 battleStatusOffset = new Vector3(0, 1.5f, 0);// 캐릭터 UI가 생성될 위치 오프셋

    // 초기화 합니다
    public override void SetUp()
    {
        base.SetUp();
        SetUpBattleStatusUI();
    }

    public override void Init()
    {
        base.Init();
        this.battleStatusUI.SetActive(true);
    }

    // UI를 꺼줍니다.
    public override void ReleaseUI()
    {
        if (battleStatusUI != null)
        {
            battleStatusUI.gameObject.SetActive(false);
        }
    }

    // UI위치를 현재 캐릭터 위치와 동기화합니다.
    private void LateUpdate()
    {
        UpdateBattleStatusUI(transform.position + battleStatusOffset);
    }

    // 들어온 월드 포지션 값을 스크린 포인터로 변경합니다.
    public void UpdateBattleStatusUI(Vector3 position)
    {
        battleStatusUI.transform.transform.position = Camera.main.WorldToScreenPoint(position);
    }

    // 캐릭터 UI를 생성합니다.
    public void SetUpBattleStatusUI()
    {
        if (battleCanvas == null)
        {
            return;
        }
        battleStatusUI = Instantiate(battleStatusUIPrefab, battleCanvas.transform);
        hpBar = battleStatusUI.GetComponentInChildren<HPBar>();
        debuffUI = battleStatusUI.GetComponentInChildren<DebuffUI>();
    }


}
