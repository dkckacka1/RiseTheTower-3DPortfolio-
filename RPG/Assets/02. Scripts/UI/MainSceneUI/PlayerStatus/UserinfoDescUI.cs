using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;

/*
 * 스텟창에서 유저의 정보를 표시해주는 UI 클래스   
 */

namespace RPG.Main.UI.StatusUI
{
    public class UserinfoDescUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI riseTopCountText;  // 최대로 오른 층수 텍스트
        [SerializeField] ItemDescUI weaponDesc;             // 무기 설명 UI
        [SerializeField] ItemDescUI armorDesc;              // 갑옷 설명 UI
        [SerializeField] ItemDescUI helmetDesc;             // 헬멧 설명 UI
        [SerializeField] ItemDescUI pantsDesc;              // 바지 설명 UI

        // 창이 활성화 되면 유저 정보를 보여줍니다
        private void OnEnable()
        {
            ShowUserinfo();
        }

        // 유저 정보를 보여줍니다.
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