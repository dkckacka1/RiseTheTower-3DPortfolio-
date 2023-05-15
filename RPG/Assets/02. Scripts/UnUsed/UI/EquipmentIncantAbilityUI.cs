using RPG.Character.Equipment;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RPG.UnUsed
{
    public class EquipmentIncantAbilityUI : MonoBehaviour
    {
        [SerializeField] Image AbilityIcon;
        [SerializeField] Button AbilityDescButton;

        Incant incant;

        public void InitAbility(Incant incant, UnityAction action)
        {
            this.incant = incant;
            AbilityIcon.sprite = incant.abilityIcon;
            AbilityDescButton.onClick.RemoveAllListeners();
            AbilityDescButton.onClick.AddListener(action);
        }
    }
}