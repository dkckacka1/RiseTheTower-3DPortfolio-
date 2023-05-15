using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using RPG.Character.Status;
using RPG.Character.Equipment;
using RPG.Battle.Core;
using RPG.Core;
using RPG.Main.UI;
using RPG.Battle.UI;
using RPG.Battle.Control;
using Newtonsoft.Json;



namespace RPG.UnUsed
{
    public class TestScene1 : MonoBehaviour
    {
        UserInfo userinfo = new UserInfo();


        private void Start()
        {
            var armor = GameManager.Instance.equipmentDataDic[200];
            ArmorData data = armor as ArmorData;
            var json = JsonConvert.SerializeObject(data);
            Debug.Log(json);
        }
    }

    public class Test
    {
        public string ExcelTester()
        {




            return "";
        }
    }
}
