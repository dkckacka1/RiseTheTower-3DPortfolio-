using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using RPG.Core;
using RPG.Character.Equipment;

namespace RPG.Main.UI.BlackSmith
{
    public class BlackSmithUI : MonoBehaviour
    {
        [SerializeField] ItemPopupUI popupUI;
        [SerializeField] ItemChoiceUI choiceUI;

        [SerializeField] TextMeshProUGUI remainIncantText;
        [SerializeField] TextMeshProUGUI remainReinfoceText;
        [SerializeField] TextMeshProUGUI remainGachaText;

        private void OnEnable()
        {
            choiceUI.InitButtonImage();
            choiceUI.ChoiceWeapon(popupUI);
            popupUI.InitIncant();
            InitRemainText();
        }

        public void InitRemainText()
        {
            remainIncantText.text = GameManager.Instance.UserInfo.itemIncantTicket.ToString();
            remainReinfoceText.text = GameManager.Instance.UserInfo.itemReinforceTicket.ToString();
            remainGachaText.text = GameManager.Instance.UserInfo.itemGachaTicket.ToString();
        }
    } 
}
