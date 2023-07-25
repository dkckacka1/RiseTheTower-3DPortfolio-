using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RPG.Character.Equipment;

/*
 * 장비 아이템의 설명을 보여주는 UI 클래스
 */

namespace RPG.Main.UI.StatusUI
{
    public class ItemDescUI : MonoBehaviour
    {
        [SerializeField] Image itemImage;                   // 아이템 이미지
        [SerializeField] TextMeshProUGUI itemNameText;      // 아이템 설명 텍스트

        // 장비아이템의 설명을 보여줍니다.
        public void ShowEquipment(Equipment equipment)
        {
            itemImage.sprite = equipment.data.equipmentSprite;

            // 인챈트 여부를 고려해서 이름을 보여줍니다.
            string text = "";

            if (equipment.prefix != null)
            {
                text += equipment.prefix.incantName + " ";
            }

            if (equipment.suffix != null)
            {
                text += equipment.suffix.incantName + " ";
            }

            text += equipment.itemName;
            itemNameText.text = text;
        }
    }
}