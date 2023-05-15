using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Core;
using RPG.Battle.UI;
using RPG.Character.Status;

public class PlayerCharacterUI : CharacterUI
{
    public override void SetUp()
    {
        base.SetUp();
        hpBar = BattleManager.BattleUI.playerHPBarUI;
        debuffUI = BattleManager.BattleUI.playerDebuffUI;
    }

    public override void Init()
    {
        base.Init();
    }
}
