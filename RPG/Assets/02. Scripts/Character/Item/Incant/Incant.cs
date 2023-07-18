using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Character.Status;

/*
 * 장비에 붙는 인챈트의 추상 클래스
 */

namespace RPG.Character.Equipment
{
    public abstract class Incant
    {
        public int incantID;    // 인챈트 아이디

        // 어느장비에 붙을 수 있는지 타입
        public EquipmentItemType itemType;
        // 인챈트 등급
        public TierType incantTier;
        // 접두 접미 타입
        public IncantType incantType;

        // 인챈트의 이름
        public string incantName;

        public bool isIncantAbility;    // 인챈트에 따로 스킬이 붙는지 여부
        public string abilityDesc;      // 인챈트 스킬 설명
        public Sprite abilityIcon;      // 인챈트 스킬 아이콘

        // 인챈트 데이터에 맞게 인챈트를 생성합니다.
        public Incant(IncantData data)
        {
            incantID = data.ID;
            incantType = data.incantType;
            itemType = data.itemType;
            incantTier = data.incantTier;
            incantName = data.incantName;
            isIncantAbility = data.isIncantAbility;
            abilityDesc = data.abilityDesc;
            abilityIcon = data.abilityIcon;
        }


        // 인챈트의 증가 스탯 설명
        public abstract string GetAddDesc();
        // 인챈트의 감소 스탯 설명
        public abstract string GetMinusDesc();
    }

}