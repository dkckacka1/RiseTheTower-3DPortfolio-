using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Battle.Control;

/*
 * 스테이지 데이터 클래스
 */

namespace RPG.Battle.Core
{
    [CreateAssetMenu(fileName = "NewStage", menuName = "CreateScriptableObject/CreateStage", order = 0)]
    public class StageData : Data
    {
        public Vector3 playerSpawnPosition = new Vector3(8.0f, 0f); // 유저 캐릭터 생성 위치
        public int[] enemyDatas;                                    // 스테이지의 적 ID
        public int ConsumEnergy;                                    // 에너지 소비양
    }
}