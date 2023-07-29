using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using RPG.Character.Status;
using RPG.Character.Equipment;
using UnityEngine.EventSystems;
using RPG.Main.Audio;

/*
 * 메인씬의 UI 매니저 클래스
 */

namespace RPG.Main.UI
{
    public class MainSceneUIManager : MonoBehaviour
    {
        [Header("UI")]
        public CharacterAppearance appearance;      // 로비 캐릭터 외형

        [Space()]
        [SerializeField] TextMeshProUGUI reinforceText;             // 강화 티켓 텍스트
        [SerializeField] TextMeshProUGUI incantText;                // 인챈트 티켓 텍스트
        [SerializeField] TextMeshProUGUI itemGachaTicketText;       // 아이템 가챠 티켓 텍스트
        [SerializeField] TextMeshProUGUI EnergyText;                // 에너지 티켓 텍스트

        [Header("Canvas")]
        [SerializeField] Canvas statusCanvas;   // 스테이터스 캔버스

        private void Start()
        {
            // 메인씬에 진입시 로비 캐릭터를 세팅하고 티켓 텍스트를 보여줍니다.
            appearance.EquipWeapon(GameManager.Instance.Player.currentWeapon.weaponApparenceID, GameManager.Instance.Player.currentWeapon.handleType);
            UpdateTicketCount();
            AudioManager.Instance.PlayMusic("MainBackGroundMusic", true);
            GameSLManager.SaveToJSON(GameManager.Instance.UserInfo, Application.dataPath + @"\Userinfo.json");
        }

        #region ButtonPlugin
        // 해당 캔버스를 보여줍니다.
        public void ShowUI(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
        }

        // 해당 캔버스를 꺼줍니다
        public void ReleaseUI(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }

        // 스테이지 선택씬으로 이동합니다.
        public void LoadStageChoiceScene()
        {
            SceneLoader.LoadStageChoiceScene();
        }

        #endregion

        // 티켓 텍스트를 업데이트합니다.
        public void UpdateTicketCount()
        {
            this.itemGachaTicketText.text = $"{GameManager.Instance.UserInfo.itemGachaTicket}";
            this.reinforceText.text = $"{GameManager.Instance.UserInfo.itemReinforceTicket}";
            this.incantText.text = $"{GameManager.Instance.UserInfo.itemIncantTicket}";
            this.EnergyText.text = $"{GameManager.Instance.UserInfo.energy}";
        }

        private void OnGUI()
        {
            // 치트용 버튼을 추가합니다.
            if (GUI.Button(new Rect(10, 100, 80, 80), "최강 장비로 변경"))
            {
                {
                    {
                        var weapon = GameManager.Instance.Player.currentWeapon;
                        GameManager.Instance.GetEquipmentData(199, out WeaponData weaponData);
                        weapon.ChangeData(weaponData);
                        weapon.Incant(GameManager.Instance.incantDic[5]);
                        weapon.Incant(GameManager.Instance.incantDic[6]);
                        for (int i = 0; i < 10; i++)
                        {
                            weapon.ReinforceItem();
                        }
                        appearance.EquipWeapon(GameManager.Instance.Player.currentWeapon.weaponApparenceID, GameManager.Instance.Player.currentWeapon.handleType);
                    }

                    {
                        var armor = GameManager.Instance.Player.currentArmor;
                        GameManager.Instance.GetEquipmentData(210, out ArmorData armorData);
                        armor.ChangeData(armorData);
                        armor.Incant(GameManager.Instance.incantDic[101]);
                        armor.Incant(GameManager.Instance.incantDic[104]);
                        for (int i = 0; i < 30; i++)
                        {
                            armor.ReinforceItem();
                        }
                    }

                    {
                        var helmet = GameManager.Instance.Player.currentHelmet;
                        GameManager.Instance.GetEquipmentData(309, out HelmetData helmetData);
                        helmet.ChangeData(helmetData);
                        helmet.Incant(GameManager.Instance.incantDic[203]);
                        helmet.Incant(GameManager.Instance.incantDic[204]);
                        for (int i = 0; i < 30; i++)
                        {
                            helmet.ReinforceItem();
                        }
                    }

                    {
                        var pants = GameManager.Instance.Player.currentPants;
                        GameManager.Instance.GetEquipmentData(405, out PantsData pantsData);
                        pants.ChangeData(pantsData);
                        pants.Incant(GameManager.Instance.incantDic[301]);
                        pants.Incant(GameManager.Instance.incantDic[304]);
                        for (int i = 0; i < 30; i++)
                        {
                            pants.ReinforceItem();
                        }
                    }

                    GameManager.Instance.UserInfo.lastedArmorID = 210;
                    GameManager.Instance.UserInfo.armorPrefixIncantID = 101;
                    GameManager.Instance.UserInfo.armorSuffixIncantID = 104;
                    GameManager.Instance.UserInfo.armorReinforceCount = 30;

                    GameManager.Instance.UserInfo.lastedHelmetID = 309;
                    GameManager.Instance.UserInfo.helmetPrefixIncantID = 204;
                    GameManager.Instance.UserInfo.helmetSuffixIncantID = 203;
                    GameManager.Instance.UserInfo.helmetReinforceCount = 30;

                    GameManager.Instance.UserInfo.lastedPantsID = 405;
                    GameManager.Instance.UserInfo.pantsPrefixIncantID = 301;
                    GameManager.Instance.UserInfo.pantsSuffixIncantID = 304;
                    GameManager.Instance.UserInfo.pantsReinforceCount = 30;
                }


                GameManager.Instance.Player.SetEquipment();
                GameManager.Instance.UserInfo.UpdateUserinfoFromStatus(GameManager.Instance.Player);
            }

            if (GUI.Button(new Rect(10, 190, 80, 80), "쿠폰 추가"))
            {
                GameManager.Instance.UserInfo.itemReinforceTicket += 100;
                GameManager.Instance.UserInfo.itemIncantTicket += 100;
                GameManager.Instance.UserInfo.itemGachaTicket += 100;
                UpdateTicketCount();
            }
        }
    }
}