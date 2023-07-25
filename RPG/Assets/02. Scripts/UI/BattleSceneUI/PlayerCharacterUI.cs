using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.UI;
using RPG.Character.Status;

/*
 * 플레이어 캐릭터 UI 클래스
 */

public class PlayerCharacterUI : CharacterUI
{
    // UI를 세팅합니다.
    public override void SetUp()
    {
        base.SetUp();
        hpBar = BattleManager.BattleUI.playerHPBarUI;
        debuffUI = BattleManager.BattleUI.playerDebuffUI;
    }
}
