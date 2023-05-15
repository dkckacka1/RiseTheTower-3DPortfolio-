using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RPG.Core;
using RPG.Battle.Core;


namespace RPG.UnUsed
{
    public class StageWindowUI : MonoBehaviour
    {
        StageData data;

        [SerializeField] TextMeshProUGUI floorText;
        [SerializeField] TextMeshProUGUI isClearText;
        [SerializeField] TextMeshProUGUI isLastText;
        [SerializeField] TextMeshProUGUI ConsumeEnergyText;
        [SerializeField] RawImage unClickedImage;

        [SerializeField] Button stageBaltteButton;

        //Encapsulration
        public StageData Data
        {
            get
            {
                return data;
            }
        }

        public void Setup(StageData data)
        {
            this.data = data;
            floorText.text = $"{this.data.ID}층";

            ConsumeEnergyText.text = $"-{data.ConsumEnergy}";
        }

        public void Init(bool isClear, bool isLast)
        {
            if (isLast == true)
            {
                isClearText.gameObject.SetActive(false);
                isLastText.gameObject.SetActive(true);
                stageBaltteButton.interactable = true;
                unClickedImage.gameObject.SetActive(false);
            }


            if (isClear == true)
            {
                isClearText.gameObject.SetActive(true);
                isLastText.gameObject.SetActive(false);
                stageBaltteButton.interactable = true;
                unClickedImage.gameObject.SetActive(false);
            }


            if (isClear == false && isLast == false)
            {
                isClearText.gameObject.SetActive(false);
                isLastText.gameObject.SetActive(false);
                stageBaltteButton.interactable = false;
                unClickedImage.gameObject.SetActive(true);
            }
        }

        #region ButtonPlugin
        public void StageBattleStart()
        {
            if (data == null)
            {
                Debug.LogError("스테이지 데이터가 없습니다.");
            }
            SceneLoader.LoadBattleScene(this.data.ID);
        }


        #endregion
    }

}