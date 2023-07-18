using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * 적이 생성될 때 세팅할 전투 진형 클래스
 */

namespace RPG.Battle.Core
{
    public class StageFomation : ScriptableObject
    {
        public List<Fomation> FomationList = new List<Fomation>();
    }

    [System.Serializable]
    public class Fomation
    {
        public string fomationName;                             // 진형 이름
        public int fomationEnemyCount;                          // 진형에 세팅할 적 수
        public List<Vector3> positions = new List<Vector3>();   // 적이 위치할 위치값
    } 
}