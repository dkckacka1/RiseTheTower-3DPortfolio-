using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;

namespace RPG.Main.UI.StatusUI
{
    public class UserinfoDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI riseTopCountText;
        [SerializeField] ItemDescUI weaponDesc;
        [SerializeField] ItemDescUI armorDesc;
        [SerializeField] ItemDescUI helmetDesc;
        [SerializeField] ItemDescUI pantsDesc;

        private void OnEnable()
        {
            ShowUserinfo();
        }

        public void ShowUserinfo()
        {
            riseTopCountText.text = MyUtility.returnSideText("최대로 오른 층 수 :", GameManager.Instance.UserInfo.risingTopCount.ToString());

            weaponDesc.ShowEquipment(GameManager.Instance.Player.currentWeapon);
            armorDesc.ShowEquipment(GameManager.Instance.Player.currentArmor);
            helmetDesc.ShowEquipment(GameManager.Instance.Player.currentHelmet);
            pantsDesc.ShowEquipment(GameManager.Instance.Player.currentPants);
        }
    }
}