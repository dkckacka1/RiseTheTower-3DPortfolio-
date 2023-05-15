using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using RPG.Character.Equipment;

namespace RPG.Core
{
    public static class RandomSystem
    {
        public static bool TryGachaRandomData<T>(Dictionary<int, EquipmentData> dic ,EquipmentItemType type, out T data, int lowerTier = 0) where T : EquipmentData
        {
            var tier = GetRandomTier(Random.Range(lowerTier, 101));

            var list = dic
                .Where(data => (data.Value.equipmentType == type && data.Value.equipmentTier == tier))
                .ToList();

            int getRandomIndex = Random.Range(0, list.Count);
            if (list.Count == 0)
            {
                Debug.Log($"{tier}등급의 뽑을 {type}이 없습니다.");
                data = null;
                return false;
            }

            data = list[getRandomIndex].Value as T;
            return true;
        }

        public static bool TryGachaIncant(EquipmentItemType type, Dictionary<int, Incant> dic, out Incant incant, int lowerTier = 0)
        {
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

        private static TierType GetRandomTier(int tex)
        {
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