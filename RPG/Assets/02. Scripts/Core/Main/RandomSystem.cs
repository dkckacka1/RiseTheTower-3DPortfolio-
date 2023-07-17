using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Character.Equipment;

/*
 * 게임 내에서 랜덤한 결과를 내는 클래스를 모아놓은 static 클래스 입니다.
 */

namespace RPG.Core
{
    public static class RandomSystem
    {
        // 랜덤한 아이템을 뽑습니다.
        public static bool TryGachaRandomData<T>(Dictionary<int, EquipmentData> dic ,EquipmentItemType type, out T data, int lowerTier = 0) where T : EquipmentData
        {
            // 랜덤한 등급을 추출합니다.
            var tier = GetRandomTier(Random.Range(lowerTier, 101));
            var list = dic
                .Where(data => (data.Value.equipmentType == type && data.Value.equipmentTier == tier))
                .ToList();

            // 해당 등급의 랜덤한 아이템 인덱스를 가져옵니다
            int getRandomIndex = Random.Range(0, list.Count);
            if (list.Count == 0)
            {
                Debug.Log($"{tier}등급의 뽑을 {type}이 없습니다.");
                data = null;
                return false;
            }

            // 아이템을 가져오되 T 클래스로 변환합니다.
            data = list[getRandomIndex].Value as T;
            return true;
        }

        // 아이템에 랜덤한 인챈트를 부여합니다.
        public static bool TryGachaIncant(EquipmentItemType type, Dictionary<int, Incant> dic, out Incant incant, int lowerTier = 0)
        {
            // 랜덤한 등급을 추출합니다.
            var tier = GetRandomTier(Random.Range(lowerTier, 101));
            var IncantList = dic
                            .Where(item => item.Value.itemType == type && item.Value.incantTier == tier)
                            .ToList();

            if (IncantList.Count == 0)
            {
                Debug.Log($"{tier} 등급의 알맞는 인챈트가 없습니다.");
                incant = null;
                return false;
            }

            // 해당 등급의 랜덤한 인챈트 인덱스를 가져옵니다.
            int randomIndex = Random.Range(0, IncantList.Count);
            incant = IncantList[randomIndex].Value;

            if (incant.itemType != type)
            {
                Debug.LogError($"잘못된 인챈트 형식 : {incant.incantName}은 {type}에 인챈트할 수 없습니다!");
                incant = null;
                return false;
            }

            return true;
        }

        // 강화 활률을 계산합니다.
        public static float ReinforceCalc(Equipment equipment)
            // 강화 확률 계산기
            // 100f = 100%, 0 = 0%
        {
            // 현재 강화 수치
            int currentReinforceCount = equipment.reinforceCount;
            // 강화 성공확률
            float reinforcementSuccessProbability = 100f - ((float)currentReinforceCount / Constant.maxReinforceCount * 100);

            return reinforcementSuccessProbability;
        }

        // 랜덤한 등급을 추출해줍니다.
        private static TierType GetRandomTier(int tex)
        {
            // 각 등급 확률에 맞게 등급을 리턴합니다.
            if (tex <= Constant.getNormalPercent)
            {
                return TierType.Normal;
            }
            else if (tex <= Constant.getNormalPercent + Constant.getRarelPercent)
            {
                return TierType.Rare;
            }
            else if (tex <= Constant.getNormalPercent + Constant.getRarelPercent + Constant.getUniquePercent)
            {
                return TierType.Unique;
            }
            else if (tex <= Constant.getNormalPercent + Constant.getRarelPercent + Constant.getUniquePercent + Constant.getLegendaryPercent)
            {
                return TierType.Legendary;
            }

            return TierType.Normal;
        }
    }

}