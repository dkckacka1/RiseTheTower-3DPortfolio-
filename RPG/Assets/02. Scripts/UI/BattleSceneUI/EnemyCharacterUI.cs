using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.UI;
using RPG.Character.Status;

public class EnemyCharacterUI : CharacterUI
{
    public GameObject battleStatusUIPrefab;
    public GameObject battleStatusUI;
    public Vector3 battleStatusOffset = new Vector3(0, 1.5f, 0);

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

    public override void ReleaseUI()
    {
        if (battleStatusUI != null)
        {
            battleStatusUI.gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        UpdateBattleStatusUI(transform.position + battleStatusOffset);
    }

    public void UpdateBattleStatusUI(Vector3 position)
    {
        battleStatusUI.transform.transform.position = Camera.main.WorldToScreenPoint(position);
    }

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
