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

namespace RPG.Main.UI
{
    public class MainSceneUIManager : MonoBehaviour
    {
        [Header("UI")]
        public CharacterAppearance appearance;

        [Space()]
        [SerializeField] TextMeshProUGUI reinforceText;
        [SerializeField] TextMeshProUGUI incantText;
        [SerializeField] TextMeshProUGUI itemGachaTicketText;
        [SerializeField] TextMeshProUGUI EnergyText;

        [Header("Canvas")]
        [SerializeField] Canvas statusCanvas;

        private void Start()
        {
            appearance.EquipWeapon(GameManager.Instance.Player.currentWeapon.weaponApparenceID, GameManager.Instance.Player.currentWeapon.handleType);
            UpdateTicketCount();
            AudioManager.Instance.PlayMusic("MainBackGroundMusic", true);
            GameSLManager.SaveToJSON(GameManager.Instance.UserInfo, Application.dataPath + @"\Userinfo.json");
        }

        #region ButtonPlugin

        public void ShowUI(Canvas canvas)
        {
            canvas.gameObject.SetActive(true);
        }

        public void ReleaseUI(Canvas canvas)
        {
            canvas.gameObject.SetActive(false);
        }

        public void LoadStageChoiceScene()
        {
            SceneLoader.LoadStageChoiceScene();
        }

        #endregion

        public void UpdateTicketCount()
        {
            this.itemGachaTicketText.text = $"{GameManager.Instance.UserInfo.itemGachaTicket}";
            this.reinforceText.text = $"{GameManager.Instance.UserInfo.itemReinforceTicket}";
            this.incantText.text = $"{GameManager.Instance.UserInfo.itemIncantTicket}";
            this.EnergyText.text = $"{GameManager.Instance.UserInfo.energy}";
        }

        // HACK : TEST
        private void OnGUI()
        {
            if (GUI.Button(new Rect(10, 190, 80, 80), "ÄíÆù Ãß°¡"))
            {
                GameManager.Instance.UserInfo.itemReinforceTicket += 100;
                GameManager.Instance.UserInfo.itemIncantTicket += 100;
                GameManager.Instance.UserInfo.itemGachaTicket += 100;
                UpdateTicketCount();
            }
        }
    }
}