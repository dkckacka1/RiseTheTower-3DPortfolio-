using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 장비에 붙는 인챈트 데이터 클래스
 */

namespace RPG.Character.Equipment
{
    [CreateAssetMenu(fileName = "NewIncant", menuName = "CreateScriptableObject/IncantData/DefaultData", order = 0)]
    public class IncantData : Data
    {
        public string className;                            // 데이터 클래스 이름
        public IncantType incantType;                       // 접두, 접미 인챈트 타입
        public EquipmentItemType itemType;                  // 어느 장비 타입의 인챈트인지
        public TierType incantTier = TierType.Normal;       // 인챈트의 등급
        public string incantName;                           // 인챈트의 이름
        [Header("Ability")]
        public bool isIncantAbility;                        // 인챈트에 스킬이 따로 붙어 있는지 여부
        [TextArea()]
        public string abilityDesc;                          // 인챈트의 설명
        public Sprite abilityIcon;                          // 인챈트의 아이콘
    }

}