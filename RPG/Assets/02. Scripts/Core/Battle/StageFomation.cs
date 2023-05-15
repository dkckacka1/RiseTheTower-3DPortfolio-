using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Battle.Core
{
    public class StageFomation : ScriptableObject
    {
        public List<Fomation> FomationList = new List<Fomation>();
    }

    [System.Serializable]
    public class Fomation
    {
        public string fomationName;
        public int fomationEnemyCount;
        public List<Vector3> positions = new List<Vector3>();
    } 
}